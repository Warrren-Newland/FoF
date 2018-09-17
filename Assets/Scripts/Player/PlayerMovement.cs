using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour {

    public TurnManager turnManager;
    float t;
    Vector3 startPosition;
    Vector3 target;
    float timeToReachTarget;
    Vector3 destination;

    void Start()
    {
        turnManager = GameObject.Find("GameManager").GetComponent<TurnManager>();
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
        destination = GameObject.Find(destinationName).GetComponent<Transform>().position;
        Vector3 newPosition = new Vector3(destination.x, destination.y + 1, destination.z);
        t = 0;
        startPosition = transform.position;
        timeToReachTarget = time;
        target = newPosition;
    }
}
