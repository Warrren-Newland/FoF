using UnityEngine;

public class PlayerMovement : MonoBehaviour {

    public PlayerController playerController;

    private TurnManager turnManager;
    private SummaryController summaryController;
    float t;
    Vector3 startPosition;
    Vector3 target;
    float timeToReachTarget;
    Vector3 destination;
    private Animator animator;


    void Start()
    {
        turnManager = GameObject.Find("GameManager").GetComponent<TurnManager>();
        summaryController = GameObject.Find("GameManager").GetComponent<SummaryController>();
        animator = GetComponentInChildren<Animator>();
        startPosition = target = transform.position;
    }
    void Update()
    {
        t += Time.deltaTime / timeToReachTarget;
        transform.position = Vector3.Lerp(startPosition, target, t);;
        turnManager.updateFloorSelectable();
    }
    public void SetDestination(string destinationName, float time)
    {
        Debug.Log("INCOMING: " + destinationName);
        summaryController.AssignPlayerToSummaryTile(destinationName, playerController.name, playerController.currentFloorTile);
        destination = GameObject.Find(destinationName).GetComponent<Transform>().position;
        Vector3 newPosition = new Vector3(destination.x, destination.y + 1f, destination.z);
        t = 0;
        startPosition = transform.position;
        timeToReachTarget = time;
        animator.SetTrigger("Jump");
        target = newPosition;
    }
}
