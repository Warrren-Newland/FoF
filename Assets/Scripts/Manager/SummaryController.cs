using UnityEngine;
using UnityEngine.UI;

public class SummaryController : MonoBehaviour {

    public GameObject initialSummaryFloorTile;
    public Canvas canvas;

    private GameObject clone;
    GameObject floorSummaryTile;
    Text buttonText;

    RectTransform initialTransform;

    public void buildSummary(FloorTileSummary[,] floorTileSummaries)
    {
        initialTransform = initialSummaryFloorTile.GetComponent<RectTransform>();
        int summaryLength = floorTileSummaries.GetLength(1);
        int summaryWidth = floorTileSummaries.GetLength(0);
        string id;
        for(int x=0;x < summaryWidth; x++)
        {
            for (int y = 0; y < summaryLength; y++)
            {
                id = "summary tile " + y + "-" + x;
                Vector3 newPosition = new Vector3(initialTransform.position.x + initialTransform.rect.width* x, initialTransform.position.y + initialTransform.rect.height * y, initialTransform.position.z);
                clone = Instantiate(initialSummaryFloorTile, newPosition, initialTransform.rotation,canvas.transform);
                clone.name = id;
                clone.GetComponent<SummaryTileController>().name = y+"-" + x;
            }
        }



        Destroy(initialSummaryFloorTile);
    }

    public void AssignPlayerToSummaryTile(string name, string playerName, string lastFloorTile)
    {
        playerName = playerName.Replace("player", "");
        removePlayerFromTile(lastFloorTile);
        name = "summary tile " + name;
        floorSummaryTile = GameObject.Find(name);
        buttonText = floorSummaryTile.GetComponentInChildren<Text>();
        buttonText.text = playerName;
    }

    private void removePlayerFromTile(string name)
    {
        name = "summary tile " + name;
        floorSummaryTile = GameObject.Find(name);
        buttonText = floorSummaryTile.GetComponentInChildren<Text>();
        buttonText.text = "";
    }
}
