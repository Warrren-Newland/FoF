using UnityEngine;
using UnityEngine.UI;
using System;
using System.Collections.Generic;
using System.Linq;

public class FloorGenerator : MonoBehaviour {

    public int width;
    public int length;
    public GameObject floorTile;
    public GameObject player;
    public GameObject unitFrame;
    public Canvas canvas;
    public int players;
    public TurnManager turnManager;
    public FloorTileSummary[,] floorTileSummaries;
    public PowerupGenerator powerupGenerator;
    public List<string> selectedMoves;

    GameObject floorTilePowerup;
    FloorController floorTileController;
    Material material;
    private GameObject clone;
    private SummaryController summaryController;

    private void Awake()
    {
        summaryController = GetComponentInParent<SummaryController>();
        selectedMoves = new List<string>();
        SetUpSummary();
        BuildFloor();
        GenerateInitialPowerUps();
        BuildPlayers();
        BuildUnitFrames();
        summaryController.buildSummary(floorTileSummaries);
        StartTurns();
    }

    private void GenerateInitialPowerUps()
    {
        int powerUpsToSpawn = (width - 1) * length / 2;
        List<string> listOfSpawns = new List<string>();
        int widthX;
        int lengthY;
        string name;
        System.Random random = new System.Random();
        int x = powerUpsToSpawn;
        while (x >0)
        {
            widthX = random.Next(1, width);
            lengthY = random.Next(0, length);
            name = widthX + "-" + lengthY;

            var match = listOfSpawns.FirstOrDefault(stringToCheck => stringToCheck.Contains(name));
            if(match == null)
            {
                AssignPowerupToTile(name);
                listOfSpawns.Add(name);
                x--;
            }
        }
    }

    private void AssignPowerupToTile(string name)
    {
        floorTilePowerup = GameObject.Find(name);
        floorTileController = floorTilePowerup.GetComponent<FloorController>();
        floorTileController.hasPowerUp = true;
        PowerUp power = powerupGenerator.generateRandomPowerUp();
        Debug.Log("making power: " + power + " at " + floorTilePowerup.name);
        setPowerupColorOfTile(power.color);
        floorTileController.powerUp = power;
    }

    private void setPowerupColorOfTile(string color)
    {
        Color instatiatedColor;
        switch (color)
        {
            case "purple":
                instatiatedColor = Color.magenta;
                break;
            case "red":
                instatiatedColor = Color.red;
                break;
            case "green":
                instatiatedColor = Color.green;
                break;
            default:
                instatiatedColor = Color.black;
                break;
        }
        material = floorTilePowerup.GetComponent<Renderer>().material;
        material.color = instatiatedColor;
    }

    private void SetUpSummary()
    {
        floorTileSummaries = new FloorTileSummary[width, length];
    }

    private void addToFloorTileSummary(int width, int length)
    {
        floorTileSummaries[width, length] = new FloorTileSummary();
    }

    private void BuildUnitFrames()
    {

        Transform unitframeTransform = unitFrame.transform;
        double width = System.Math.Round(canvas.pixelRect.width);
        int framesOnALine = (int) System.Math.Round(width / players,0);
        RectTransform unitFrameRectTransform = unitFrame.GetComponent<RectTransform>();
        
        for(int x = 1; x <= players; x++)
        {
            string id = "unitframe player " + x;
            CreateUnitFrame(id, unitFrameRectTransform);
            width = unitFrameRectTransform.sizeDelta.x * unitFrameRectTransform.localScale.x;
            unitFrameRectTransform.position = new Vector3(unitFrameRectTransform.position.x + 120, unitFrameRectTransform.position.y, unitFrameRectTransform.position.z);
        }

        Destroy(unitFrame);
    }

    private void CreateUnitFrame(string id, RectTransform rectTrasform)
    { 
        formatText(id);
        clone = Instantiate(unitFrame, rectTrasform.position, rectTrasform.rotation, canvas.transform);
        clone.name = id;
    }

    private void formatText(string id)
    {
        Text text = unitFrame.GetComponentInChildren<Text>();
        id = id.Replace("unitframe p", "P");
        text.text = id;
    }

    private void StartTurns()
    {
        turnManager.StartTurns();
    }

    private void BuildFloor()
    {
        Transform floorTile1Transform = floorTile.transform;
        for (int i = 0; i < width; i++)
        {
            for (int j = 0; j < length; j++)
            {
                CreateFloorTile(i, j, floorTile1Transform);
                addToFloorTileSummary(i, j);
            }
        }
        Destroy(floorTile);
    }

    private void BuildPlayers()
    {
        Transform player1Transform = player.transform;
        int spacesBetween = (length)/players;
        int currentBlockX = 0;
        for (int x = 1; x <= players; x++)
        {
            if(x == 1)
            {
                currentBlockX = 0;
            }
            else
            { 
                currentBlockX = currentBlockX + spacesBetween;
            }
            string findName ="0-" + currentBlockX;
            string id = "player " + x;
            Transform tileTransform = GameObject.Find(findName).transform;
            CreatePlayer(id, player1Transform, tileTransform);
        }
        Destroy(player);
    }

    private void CreatePlayer(string id, Transform playerTransform, Transform tileTransform)
    {
        Vector3 position = tileTransform.position;
        Vector3 scale = playerTransform.localScale;
        Quaternion rotation = playerTransform.rotation;
        Vector3 newPosition = new Vector3(tileTransform.position.x, tileTransform.position.y +1.5f, tileTransform.position.z);
        clone = Instantiate(player, newPosition, rotation);
        clone.name = id;
    }

    private void CreateFloorTile(int width, int length, Transform Transform)
    {
        Vector3 position = Transform.position;
        Vector3 scale = Transform.localScale;
        Quaternion rotation = Transform.rotation;
        Vector3 newPosition = new Vector3(position.x + scale.x * length, position.y, position.z + scale.z * width);
        clone = Instantiate(floorTile, newPosition, rotation);
        clone.name = width + "-" + length;
    }

    private void turnOnSelectable(int x, int y)
    {
        string toFind = x + "-" + y;
        GameObject tileWithPlayer = GameObject.Find(toFind);
        if(tileWithPlayer != null)
        {
            tileWithPlayer.GetComponent<FloorController>().turnOnSelectable();
        }
    }

    public void TurnOnSelected(int x, int y, int span)
    {
        TurnOffAllSelected();
        if (span > 1)
        {
            for(int i =0; i <= span; i++)
            {
                for (int j = 0; j <= span; j++)
                {
                    if (i + j <= span)
                    {
                        turnOnSelectable(x + i, y + j);
                        turnOnSelectable(x + i, y - j);
                        turnOnSelectable(x - i, y - j);
                        turnOnSelectable(x - i, y + j);
                    }
                }
            }
        }
        else
        {
            turnOnSelectable(x, y);
        }
    }

    public void TurnOffAllSelected()
    {
        GameObject[] selectables = GameObject.FindGameObjectsWithTag("Floor");
        foreach(GameObject selectable in selectables)
        {
            selectable.GetComponent<FloorController>().turnOffSelectable();
        }
    }

    public void TurnOffSelected(int x, int y, int span)
    {
        string toFind = x + "-" + y;
        GameObject tileWithPlayer = GameObject.Find(toFind);
        tileWithPlayer.GetComponent<FloorController>().turnOffSelectable();
    }
}
