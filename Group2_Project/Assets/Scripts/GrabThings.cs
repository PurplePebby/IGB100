using Cinemachine;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;


public class GrabThings : MonoBehaviour
{
    //FROM
    //https://www.patrykgalach.com/2020/03/16/pick-up-items-in-unity/
    [SerializeField]
    CinemachineVirtualCamera currentCamera;

    [SerializeField]
    CinemachineVirtualCamera newCamera;

    private RaycastHit hit;

    // Reference to the character camera.
    [SerializeField]
    private Camera characterCamera;

    // Reference to the slot for holding picked item.
    [SerializeField]
    private GameObject[] slot;
    

    //get position for player to tp to cannon
    [SerializeField]
    private GameObject cannonPos;

    //Max distance of raycast
    [SerializeField]
    private float interactionDistance = 5f;



    private GameObject useSlot;

    [SerializeField]
    private bool emptySlots;
    // Reference to the currently held item.
    private CollectibleThing thing;

    private bool InteractableItem;
    
    //[SerializeField]
    //private int GameManager.instance.inventoryTreasureCount;

    /// <summary>
    /// Method called very frame.
    /// </summary>
    private void Update() {

        DetectBoatItem();
        CastRays();

    }

    private void Start() {
        isSlotEmpty();
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
        item.gameObject.layer = 5;
        item.GetComponent<ParticleSystem>().Stop();

        // Set Slot as a parent
        DetermineSlot(slot);

        item.transform.SetParent(useSlot.transform);

        // Reset position and rotation
        item.transform.localPosition = Vector3.zero;
        item.transform.localEulerAngles = Vector3.zero;
        Debug.Log($"AfterPickItemTreasureCount: {GameManager.instance.inventoryTreasureCount}");

        //Debug.Log("treasure count: " + treasureCount);
        SoundManager.instance.PlaySingle(SoundManager.instance.treasureCollect);
        isSlotEmpty();
    }


    private void isSlotEmpty() {
       
        for (int i = 0; i < slot.Length; i++) {
            //if there is still space, return true
            if (slot[i].transform.childCount == 0) {
                emptySlots = true;
                useSlot = slot[i];
                return;
            }
            else {
             emptySlots = false; 
            }
        }
    }

    private void DetermineSlot(GameObject[] slot){
        for (int i = 0; i < slot.Length; i++){
            if (slot[i].transform.childCount == 0) {
                useSlot = slot[i];
                return;
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

    private void DetectBoatItem() {
        // If no, try to pick item in front of the player
        // Create ray from center of the screen
        var ray = characterCamera.ViewportPointToRay(Vector3.one * 0.5f);
        // Shot ray to find object to pick
        if (Physics.Raycast(ray, out hit, interactionDistance)) {
            // If object has PickableItem class
            var interactable = hit.transform.GetComponent<InteractableThing>();
            //Debug.Log("interactable"+ hit.transform.GetComponent<InteractableThing>());
                if (interactable.tag == "O2Tank") {
                    StartCoroutine(GameManager.instance.ShowIfInteract("refill oxygen"));
                }
                if (interactable.tag == "TreasureDropOff") {

                    StartCoroutine(GameManager.instance.ShowIfInteract("deposit treasure"));
                }
                if (interactable.tag == "CannonButton") {

                    StartCoroutine(GameManager.instance.ShowIfInteract("use the pirate cannon"));
                }

                if (interactable.tag == "TreasureDropOff" && Input.GetKey("e")) {
                    Debug.Log(hit.transform.GameObject().name);
                    //Dropoff Treasure
                    AddScore();
                    ///SOUND
                    ///
                    //sound for dropping off treasure
                    SoundManager.instance.PlaySingle(SoundManager.instance.treasureDropOffs);
                    ///
                    ///SOUND

                }
                if (interactable.tag == "O2Tank" && Input.GetKey("e")) {
               
                    //Refill Tank
                    GameManager.instance.SetOxygen(100);
                    ///SOUND
                    ///
                    //sound for refilling oxygen
                    SoundManager.instance.PlaySingle(SoundManager.instance.oxygenRefill);
                    ///
                    ///SOUND

                }
                if (interactable.tag == "CannonButton" && Input.GetKey("e")) {
                    StartCoroutine(GameManager.instance.HideIfNoInteract());
                    //TP Player
                    CharacterController cc = this.GetComponent<CharacterController>();
                    cc.enabled = false;
                    //change to camera for cannon
                    newCamera.gameObject.SetActive(true);
                    currentCamera.gameObject.SetActive(false);
				    Cursor.visible = false;
				    Cursor.lockState = CursorLockMode.Confined;
				    GameManager.instance.onCannon = true;
                    ///SOUND
                    ///
                    //sound for pressing the cannon button
                    //SoundManager.instance.PlaySingle(SoundManager.instance.NAME_OF_FIELD);
                    ///
                    ///SOUND
                } 
        }
        else {
            StartCoroutine(GameManager.instance.HideIfNoInteract());
        }
    }

    private void CastRays() {
        // If no, try to pick item in front of the player
        // Create ray from center of the screen
        var ray = characterCamera.ViewportPointToRay(Vector3.one * 0.5f);
        //RaycastHit hit;
        // Shot ray to find object to pick
        if (Physics.Raycast(ray, out hit, interactionDistance)) {
            // Check if object is pickable
            var pickable = hit.transform.GetComponent<CollectibleThing>();
            var interactable = hit.transform.GetComponent<InteractableThing>();


            if (emptySlots == true && pickable && Input.GetKey("e")) {

                // Pick it
                PickItem(pickable);

            }

            // If object has PickableItem class
            if (emptySlots == false) {
                return;
            }

            if (interactable.tag == "Treasure") {
                StartCoroutine(GameManager.instance.ShowIfInteract("pick up treasure"));
            }

            
            
            //GameManager.instance.ShowE(false);
        }
        else {
            StartCoroutine(GameManager.instance.HideIfNoInteract());
        }
    }

    //something buggy about when this is called
    public void AddScore() {
        for (int i = 0; i < slot.Length; i++) {
            if (slot[i].transform.childCount >= 1) {
                Debug.Log("Score Added");

                GameManager.instance.AddMoney(slot[i].transform.GetChild(0).GetComponent<CollectibleThing>().moneyValue);

                Destroy(slot[i].transform.GetChild(0).gameObject);
            }
        }
        emptySlots = true;
        Debug.Log($"AddScoreTreasureCount: {GameManager.instance.inventoryTreasureCount}");


        //for (int i = 0; i < slot.Length; i++) {
        //    if (slot[i].transform.childCount == 1) {
        //        //Debug.Log("Score Added");
        //        Destroy(slot[i].transform.GetChild(0).gameObject);
        //        GameManager.instance.AddTreasureCount(-1);
        //        GameManager.instance.AddMoney(slot[i].transform.GetChild(0).GetComponent<CollectibleThing>().moneyValue);
        //    }
        //}
        //GameManager.instance.inventoryTreasureCount -= GameManager.instance.inventoryTreasureCount;
    }

    ///debug notes:
    ///addscore was running twice, now it only runs once
    ///current issue seems to be with how the treasure inventory is iterated through and/or deposited

}
