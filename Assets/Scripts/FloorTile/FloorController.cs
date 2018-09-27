using UnityEngine;
using System.Linq;

public class FloorController : MonoBehaviour {

    public bool playerOnTile=false;
    public bool selectable = false;
    public bool hasPowerUp = false;
    public PowerUp powerUp;
    public Material myMaterial;
    GameObject player;
    PlayerMovement playerMovement;
    UtilityFunctions utilityFunctions;
    GameObject floor;
    GameObject manager;
    ScoreKeeper scoreKeeper;
    string currentPlayerName;
    string floorName;
    TurnManager turnManager;
    FloorGenerator floorGenerator;

    private void Awake()
    {
        manager = GameObject.FindGameObjectWithTag("Manager");
        myMaterial = GetComponent<Renderer>().material;
        turnManager = manager.GetComponentInParent<TurnManager>();
        floorGenerator= manager.GetComponentInParent<FloorGenerator>();
        scoreKeeper = manager.GetComponentInParent<ScoreKeeper>();
        utilityFunctions = new UtilityFunctions();
        floor = gameObject;
    }

    private void getPlayerMovement()
    {
        currentPlayerName = "player " + turnManager.currentPlayerTurn;
        player = GameObject.Find(currentPlayerName);
        playerMovement = player.GetComponent<PlayerMovement>();
    }

    private void updateFloorSummary(bool hasPlayer, string playerName)
    {
        floorName = this.name;
        if(floorName != "FloorTile")
        {
            int x = System.Convert.ToInt32(floorName.Substring(0, 1));
            int y = System.Convert.ToInt32(floorName.Substring(2, 1));
            floorGenerator.floorTileSummaries[x, y].hasPlayer = hasPlayer;
            floorGenerator.floorTileSummaries[x, y].playerName = playerName;
        }
    }

    void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Player")
        {
            var match = floorGenerator.selectedMoves.FirstOrDefault(stringToCheck => stringToCheck.Contains(this.name));
            if(match != null)
            {
                player = GameObject.Find(other.name);
                Animator animator = player.GetComponentInChildren<Animator>();
                animator.ResetTrigger("Jump");
                PlayerController playerController = player.GetComponent<PlayerController>();
                playerController.activateWeapon();
                activateFloorTile(other.name);
            }
             updateFloorSummary(true, other.name);
             playerOnTile = true;
        }
    }

    void OnTriggerExit(Collider other)
    {
        if (other.tag == "Player")
        {
            updateFloorSummary(false, other.name);
            playerOnTile = false;
        }
    }

    private void activateFloorTile(string playerName)
    {
        GameObject player = GameObject.Find(playerName);
        PlayerController playerController = player.GetComponent<PlayerController>();
        if (this.hasPowerUp)
        {
            switch (powerUp.grouping)
            {
                case "Danger":
                    Debug.Log("DANGER POWER UP " + playerName);
                    Danger danger = (Danger) powerUp;
                    scoreKeeper.updatePlayerHealth(playerName, -danger.damage);
                    resetFloorTilePowerup();
                    break;
                case "Heal":
                    Debug.Log("HEAL POWER UP " + playerName);
                    Heal heal = (Heal)powerUp;
                    scoreKeeper.updatePlayerHealth(playerName, heal.health);
                    resetFloorTilePowerup();
                    break;
                case "Weapon":
                    Debug.Log("WEAPON POWER UP " + playerName);
                    Weapon weapon = (Weapon) powerUp;
                    playerController.weapon = weapon;
                    resetFloorTilePowerup();
                    break;
                default:
                    break;
            }
        }
    }

    private void resetFloorTilePowerup()
    {
        powerUp = null;
        hasPowerUp = false;
        myMaterial.color = Color.black;
    }

    private void OnMouseDown()
    {
        floorName = this.name;
        floorGenerator.selectedMoves.Add(floorName);
        getPlayerMovement();
        if(this.selectable == true)
        {
            playerMovement.SetDestination(floorName, 1f);
            turnManager.NextTurn();
        }
    }

    public void turnOnSelectable()
    {
        GameObject selectable = utilityFunctions.FindObjectwithTag(floor.transform, "Selectable");
        this.selectable = true;
        selectable.GetComponent<MeshRenderer>().enabled = true;
    }

    public void turnOffSelectable()
    {
        GameObject selectable = utilityFunctions.FindObjectwithTag(floor.transform, "Selectable");
        this.selectable = false;
        selectable.GetComponent<MeshRenderer>().enabled = false;
    }

}
