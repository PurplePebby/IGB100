using Cinemachine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AntiPirateCannon : MonoBehaviour
{

    [SerializeField]
    private GameObject player;

    [SerializeField]
    CinemachineVirtualCamera currentCamera;
    [SerializeField]
    CinemachineVirtualCamera newCamera;
    [SerializeField]
    CinemachineBrain myBrain;


    void Update() {
        
        if (myBrain.ActiveVirtualCamera == currentCamera){
            //Debug.Log("Active");
            //run cannon
            FollowMouse();

            if (Input.GetKey("q")) {
                CharacterController cc = player.GetComponent<CharacterController>();
                this.transform.localRotation = Quaternion.Euler(Vector3.zero); 
                cc.enabled = true;
                newCamera.gameObject.SetActive(true);
                currentCamera.gameObject.SetActive(false);

				Cursor.visible = false;
				Cursor.lockState = CursorLockMode.Confined;
                GameManager.instance.onCannon = false;

			}
        }
    }
    private void FollowMouse() {
        transform.LookAt(Camera.main.ScreenToWorldPoint(Input.mousePosition + new Vector3(0, 0, 120)));
        transform.Rotate(-90.0f, 90f, 0f, Space.Self);
    }

}
