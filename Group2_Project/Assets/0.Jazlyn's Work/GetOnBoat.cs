using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GetOnBoat : MonoBehaviour
{
    [SerializeField]
    private GameObject slot;
    
    [SerializeField]
    private GameObject player;
   
    // Start is called before the first frame update
    void OnTriggerStay(Collider other) {
        CharacterController cc = player.GetComponent<CharacterController>();
        if (other.tag == "Player"){
            
            //display interaction
            StartCoroutine(GameManager.instance.ShowIfInteract("get on boat"));

            //if button, then player is tp onto boat
            if (Input.GetKey("e")) {
                Debug.Log("Button press");

                cc.enabled = false;
                player.transform.position = slot.transform.position;
                cc.enabled = true;
            }

        }
        //player can just jump off boat prolly ngl
    }

}
