using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SummaryController : MonoBehaviour {

    public GameObject initialSummaryFloorTile;
    public Canvas canvas;

    private GameObject clone;

    RectTransform initialTransform;
    
    private void Awake()
    {
        initialTransform = initialSummaryFloorTile.GetComponent<RectTransform>();
    }

    public void buildSummary(FloorTileSummary[,] floorTileSummaries)
    {
        int summaryLength = floorTileSummaries.GetLength(1);
        int summaryWidth = floorTileSummaries.GetLength(0);
        string id;
        Debug.Log("Length: " + summaryWidth);
        for(int x=0;x < summaryWidth; x++)
        {
            for (int y = 0; y < summaryLength; y++)
            {
                id = "summary tile " + y + "-" + x;
                Debug.Log("GENERATING>>> " + id);
                Vector3 newPosition = new Vector3(initialTransform.position.x + initialTransform.rect.width* x, initialTransform.position.y + initialTransform.rect.height * y, initialTransform.position.z);
                clone = Instantiate(initialSummaryFloorTile, newPosition, initialTransform.rotation,canvas.transform);
                clone.name = id;
                clone.GetComponent<SummaryTileController>().name = y+"-" + x;
            }
        }



        Destroy(initialSummaryFloorTile);
    }
}
