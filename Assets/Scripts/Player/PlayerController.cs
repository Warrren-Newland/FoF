using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour {

    public int range=2;
    public string currentFloorTile;
    public Weapon weapon;
    GameObject player;
    Vector3 playerPosition;
    FloorGenerator floorGenerator;
    Collider capsuleCollider;

    private void Awake()
    {
        player = GameObject.FindGameObjectWithTag("Player");
        playerPosition = player.GetComponent<Transform>().position;
        floorGenerator = GameObject.Find("GameManager").GetComponent<FloorGenerator>();
    }

    public void turnOnSelectable()
    { 
        int x = 0;
        int.TryParse(currentFloorTile.Substring(0, 1), out x);
        int y = 0;
        int.TryParse(currentFloorTile.Substring(2, 1), out y);
        floorGenerator.TurnOnSelected(x, y, range);
    }

    public void activateWeapon(){
        if(weapon !=null){
            useWeapon();  
        }
    }

    private void useWeapon(){
         switch(weapon.direction){
         case "around":
                Debug.Log("currentFloorTile: around "+currentFloorTile);
                break;
         case "straight":
                Debug.Log("currentFloorTile: straight "+currentFloorTile);
                break;
         default:
                break;
         }
    }

    void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.layer == 9)
        {
            currentFloorTile = other.name;
        }
    }
}
