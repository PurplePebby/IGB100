using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[RequireComponent(typeof(Rigidbody))]
public class CollectibleThing : MonoBehaviour
{
    //FROM
    //https://www.patrykgalach.com/2020/03/16/pick-up-items-in-unity/
    /// <summary>
    /// Attach this class to make object pickable.
    /// </summary>
    // Reference to the rigidbody
    private Rigidbody rb;
    public Rigidbody Rb => rb;

    /// <summary>
    /// Method called on initialization.
    /// </summary>
    private void Awake() {
        // Get reference to the rigidbody
        rb = GetComponent<Rigidbody>();
    }
}
