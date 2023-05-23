using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class pirateMovement : MonoBehaviour
{
    public GameObject playerBoat;

    public float pirateSpeed;
    public Vector3 waterLevelPoint;

    [SerializeField]
    private BoxCollider shipBox;
    public int treasureStealAmt;

    // Start is called before the first frame update
    void Start()
    {
        playerBoat = GameObject.Find("Scout_Boat");
    }

    // Update is called once per frame
    void Update()
    {
        MoveToShip();
        stealTreasure();
    }

    private void stealTreasure() {
        if (transform.position.z-8 < playerBoat.transform.position.z || transform.position.z < playerBoat.transform.position.z+8) {
            Debug.Log("In Z range");
            if (transform.position.x - 15 < playerBoat.transform.position.x || transform.position.x < playerBoat.transform.position.x + 15){
                Debug.Log("In X range");
                GameManager.instance.RemoveMoney(treasureStealAmt); 
                Destroy(gameObject, 2f);
            }   
                
        } 
    }

    private void MoveToShip() {
        transform.position = Vector3.MoveTowards(transform.position, new Vector3(playerBoat.transform.position.x-10, playerBoat.transform.position.y, playerBoat.transform.position.z+4), pirateSpeed * Time.deltaTime);
        transform.LookAt(new Vector3(playerBoat.transform.position.x, transform.position.y, playerBoat.transform.position.z));
    }
}
