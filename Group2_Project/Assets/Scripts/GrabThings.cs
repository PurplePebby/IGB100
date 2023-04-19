using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GrabThings : MonoBehaviour
{
    //FROM
    //https://www.patrykgalach.com/2020/03/16/pick-up-items-in-unity/
    // Reference to the character camera.
    [SerializeField]
    private Camera characterCamera;
    // Reference to the slot for holding picked item.
    [SerializeField]
    private Transform slot;
    // Reference to the currently held item.
    private CollectibleThing thing;
    /// <summary>
    /// Method called very frame.
    /// </summary>
    private void Update() {
        // Execute logic only on button pressed
        if (Input.GetMouseButtonDown(0)) {
            // Check if player picked some item already
            if (thing) {
                // If yes, drop picked item
                DropItem(thing);
            }
            else {
                // If no, try to pick item in front of the player
                // Create ray from center of the screen
                var ray = characterCamera.ViewportPointToRay(Vector3.one * 0.5f);
                RaycastHit hit;
                // Shot ray to find object to pick
                if (Physics.Raycast(ray, out hit, 1.5f)) {
                    // Check if object is pickable
                    var pickable = hit.transform.GetComponent<CollectibleThing>();
                    // If object has PickableItem class
                    if (pickable) {
                        // Pick it
                        PickItem(pickable);
                    }
                }
            }
        }
    }
    /// <summary>
    /// Method for picking up item.
    /// </summary>
    /// <param name="item">Item.</param>
    private void PickItem(CollectibleThing item) {
        // Assign reference
        thing = item;
        // Disable rigidbody and reset velocities
        item.Rb.isKinematic = true;
        item.Rb.velocity = Vector3.zero;
        item.Rb.angularVelocity = Vector3.zero;
        // Set Slot as a parent
        item.transform.SetParent(slot);
        // Reset position and rotation
        item.transform.localPosition = Vector3.zero;
        item.transform.localEulerAngles = Vector3.zero;
    }
    /// <summary>
    /// Method for dropping item.
    /// </summary>
    /// <param name="item">Item.</param>
    private void DropItem(CollectibleThing item) {
        // Remove reference
        thing = null;
        // Remove parent
        item.transform.SetParent(null);
        // Enable rigidbody
        item.Rb.isKinematic = false;
        // Add force to throw item a little bit
        item.Rb.AddForce(item.transform.forward * 2, ForceMode.VelocityChange);
    }
}
