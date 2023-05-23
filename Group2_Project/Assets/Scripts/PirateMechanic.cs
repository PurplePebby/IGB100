using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public class PirateMechanic : MonoBehaviour {
    public float pirateHealth;
    public float Dmg;
    public float pirateSpeed;
    public float waterLevelPoint;
    private void Update() {
        //move
    }
    private void OnCollisionEnter(Collision collision) {
        if (collision == null) return;
        if (collision.gameObject.tag == "Pew") {
            //check health
            if (pirateHealth <= 0) {
                //kill boat
                Destroy(this.gameObject);
            }
            else {
                pirateHealth = pirateHealth - Dmg;
            }
            //lose health
        }
    }
}