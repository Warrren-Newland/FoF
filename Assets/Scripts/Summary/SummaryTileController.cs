using UnityEngine;
using UnityEngine.EventSystems;

public class SummaryTileController : MonoBehaviour,IPointerClickHandler{

    public string name;

    private GameObject gameManager;
    private TurnManager turnManager;
    private PlayerMovement playerMovement;

    private void Awake()
    {
        name = transform.name;
        gameManager = GameObject.FindGameObjectWithTag("Manager");
        turnManager = gameManager.GetComponent<TurnManager>();
    }

    public void OnPointerClick(PointerEventData pointerEventData)
    {
        if (pointerEventData.button == PointerEventData.InputButton.Right)
        {
            Debug.Log(" RIGHT CLICK!");
        }
        
        if (pointerEventData.button == PointerEventData.InputButton.Left)
        {
            string playerName = "player " + turnManager.currentPlayerTurn;
            Debug.Log("NAME: " + playerName);
            GameObject Player = GameObject.Find(playerName);
            playerMovement = Player.GetComponent<PlayerMovement>();
            playerMovement.SetDestination(name, 1f);
        }        
    }
}
