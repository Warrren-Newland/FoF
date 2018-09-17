using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TurnManager : MonoBehaviour
{

    public int currentPlayerTurn;
    public FloorGenerator floorGenerator;

    private int players;
    private GameObject currentPlayer;

    private void Awake()
    {
        players = floorGenerator.players;
    }


    public void StartTurns()
    {
        string playerToGo = "player " + currentPlayerTurn;
    }

    public void NextTurn()
    {
        if (currentPlayerTurn < players)
        {
            currentPlayerTurn++;
        }
        else
        {
            currentPlayerTurn = 1;
        }

        Debug.Log("currentPlayerTurn: " + currentPlayerTurn);
        updateFloorSelectable();
    }

    public void updateFloorSelectable()
    {
        currentPlayer = GameObject.Find("player " + currentPlayerTurn);
        PlayerController playerController = currentPlayer.GetComponent<PlayerController>();
        playerController.turnOnSelectable();
    }
}
