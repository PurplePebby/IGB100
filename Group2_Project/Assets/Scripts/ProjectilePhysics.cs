using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ProjectilePhysics : MonoBehaviour{
    public float lifeTime = 3.0f;
    Rigidbody blob_Rigidbody;
    public float moveSpeed = 80.0f;
    private bool applyForce = false;

    // Start is called before the first frame update
    void Start() {
        //when game starts, destroy the object X amt of time from now
        blob_Rigidbody = GetComponent<Rigidbody>();

        Destroy(this.gameObject, lifeTime);

    }


    void FixedUpdate() {
        if (applyForce == false) {
            blob_Rigidbody.AddForce(transform.forward * moveSpeed * 2f);
        }
        applyForce = true;
    }

}

