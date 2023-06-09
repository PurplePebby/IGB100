using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GetOnBoat : MonoBehaviour
{
    [SerializeField]
    private GameObject pos;
    
    [SerializeField]
    private GameObject player;
   
    // TP onto boat
    void OnTriggerStay(Collider other) {
        CharacterController cc = player.GetComponent<CharacterController>();
        if (other.tag == "Player"){
            
            //display interaction
            StartCoroutine(GameManager.instance.ShowIfInteract("get on boat"));

            //if button, then player is tp onto boat
            if (Input.GetKey("e")) {
                //Debug.Log("Button press");

                cc.enabled = false;
                player.transform.position = pos.transform.position;
                
                cc.enabled = true;

				//sound for abovewater music
				SoundManager.instance.PlayMusic(SoundManager.instance.aboveWaterSounds);
			}

        }
        //player can just jump off boat prolly ngl
    }


}
