using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
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
    private GameObject[] slot;
    
    private GameObject useSlot;
    // Reference to the currently held item.
    private CollectibleThing thing;

    private bool InteractableItem;
    private int treasureCount;
    /// <summary>
    /// Method called very frame.
    /// </summary>
    private void Update() {

        CastRays();
        DetectTreasureChest();
        
    }

    private void Start() {
        this.AddComponent<SpawnThings>();
    }

    /// <summary>
    /// Method for picking up item.
    /// </summary>
    /// <param name="item">Item.</param>
    private void PickItem(CollectibleThing item) {
        DetermineSlot(slot);
            // Assign reference
        thing = item;
        // Disable rigidbody and reset velocities
        item.Rb.isKinematic = true;
        item.Rb.velocity = Vector3.zero;
        item.Rb.angularVelocity = Vector3.zero;
        item.gameObject.layer = 5;

        
        // Set Slot as a parent
        DetermineSlot(slot);
        
        item.transform.SetParent(useSlot.transform);


        // Reset position and rotation
        item.transform.localPosition = Vector3.zero;
        item.transform.localEulerAngles = Vector3.zero;
        treasureCount =+1;
        Debug.Log("treasure count: " + treasureCount);
        
    }

    private void DetermineSlot(GameObject[] slot){
        for (int i = 0; i < slot.Length; i++){
            if (slot[i].transform.childCount == 0) {
                useSlot = slot[i];                  
            }
        }
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
        //item.Rb.AddForce(item.transform.forward * 2, ForceMode.VelocityChange);
    }

    private void DetectTreasureChest() {
        // If no, try to pick item in front of the player
        // Create ray from center of the screen
        var ray = characterCamera.ViewportPointToRay(Vector3.one * 0.5f);
        RaycastHit hit;
        // Shot ray to find object to pick
        if (Physics.Raycast(ray, out hit, 5f)) {
            // If object has PickableItem class
            var interactable = hit.transform.GetComponent<InteractableThing>();
            if (interactable) {

                StartCoroutine(GameManager.instance.ShowIfInteract());
            }
            else if (interactable == false) {
                StartCoroutine(GameManager.instance.HideIfNoInteract());
            }
            if (hit.transform.GameObject().tag == "TreasureDropOff" && Input.GetKey("e")) {
                //Dropoff Treasure
                for (int i = 0; i < 1; i++) {
                    AddScore();
                }

            }
        }
    }

    private void CastRays() {
        // If no, try to pick item in front of the player
        // Create ray from center of the screen
        var ray = characterCamera.ViewportPointToRay(Vector3.one * 0.5f);
        RaycastHit hit;
        // Shot ray to find object to pick
        if (Physics.Raycast(ray, out hit, 4f)) {
            // Check if object is pickable
            var pickable = hit.transform.GetComponent<CollectibleThing>();
            var interactable = hit.transform.GetComponent<InteractableThing>();
            if (interactable) {
                
                StartCoroutine(GameManager.instance.ShowIfInteract());
            }
            else {
                StartCoroutine(GameManager.instance.HideIfNoInteract());
            }

            // If object has PickableItem class
            if (Input.GetKey("e") && pickable && treasureCount <3) {

                // Pick it
                PickItem(pickable);

            }
            else if (treasureCount == 3) {
                DropItem(pickable);
            }
            //GameManager.instance.ShowE(false);
        }
    }
    public void AddScore() {
        for (int i = 0; i < slot.Length; i++) {
            if (slot[i].transform.childCount == 1) {               
                //Debug.Log("Score Added");
                Destroy(slot[i].transform.GetChild(0).gameObject);
                GameManager.instance.AddCount(-1);
                GameManager.instance.AddScore(slot[i].transform.GetChild(0).GetComponent<CollectibleThing>().moneyValue);
            }
        }
        treasureCount =- treasureCount;
    }

}
