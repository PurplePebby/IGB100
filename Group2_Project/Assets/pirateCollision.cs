using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class pirateCollision : MonoBehaviour
{
    public float pirateHealth;
    public float Dmg;


    private void OnCollisionEnter(Collision collision) {
        //Debug.Log("There was a collision");
        if (collision.gameObject.tag == "Pew") {
            //check health
            if (pirateHealth <= 0) {
                //kill boat
                Destroy(this.gameObject);
                GameManager.instance.pirateShip = false;
            }
            else {
                pirateHealth = pirateHealth - Dmg;
            }
            Debug.Log("Pirate health" + pirateHealth);
            //lose health
        }
    }
}
