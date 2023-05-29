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

    private bool alive;

    // Start is called before the first frame update
    void Start()
    {
        alive = true;
        playerBoat = GameObject.Find("Scout_Boat");
        shipBox = this.GetComponentInChildren<BoxCollider>();
    }

    // Update is called once per frame
    void Update()
    {
        Debug.Log(alive);
        if (alive == true){
            MoveToShip();
            //replace with collision
            StartCoroutine(stealTreasure());
        }
    }

    private IEnumerator stealTreasure() {
        if(shipBox != null){
            if (shipBox.bounds.Intersects(playerBoat.GetComponent<Collider>().bounds)){
                //Debug.Log("COLLISION");
                GameManager.instance.AddMoney(-(treasureStealAmt));
                
                Destroy(gameObject);
                GameManager.instance.pirateShip = false;           
                yield return null;
            }        
            
        }
        else {
            alive = false;
        }
        yield return null;
    }

    private void MoveToShip() {
        transform.position = Vector3.MoveTowards(transform.position, new Vector3(playerBoat.transform.position.x-10, playerBoat.transform.position.y, playerBoat.transform.position.z+4), pirateSpeed * Time.deltaTime);
        transform.LookAt(new Vector3(playerBoat.transform.position.x, transform.position.y, playerBoat.transform.position.z));
    }
}
