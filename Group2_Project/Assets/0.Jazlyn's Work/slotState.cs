using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class slotState : MonoBehaviour
{
    public bool state;
    private GameObject slot;
    public GameObject Slot => slot;

    /// <summary>
    /// Method called on initialization.
    /// </summary>
    private void Awake() {
        // Get reference to the rigidbody
        slot = GetComponent<GameObject>();
    }

    private void Update() {
        //CheckState();
    }

    private bool CheckState(){
        if (this.transform.GetChild(0) == null){ 
            state = false;
        }
        else {
            state = true;
        }
        return state; }
}

