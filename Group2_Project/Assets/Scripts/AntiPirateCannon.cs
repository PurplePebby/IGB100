using Cinemachine;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class AntiPirateCannon : MonoBehaviour
{

    [SerializeField]
    private GameObject player;
	[SerializeField] private RectTransform Crosshair;
    public Canvas canvas;

	[SerializeField]
    CinemachineVirtualCamera currentCamera;
    [SerializeField]
    CinemachineVirtualCamera newCamera;
    [SerializeField]
    CinemachineBrain myBrain;


    void Update() {
        
        if (myBrain.ActiveVirtualCamera == currentCamera && !GameManager.instance.Paused && GameManager.instance.onCannon){
            //Debug.Log("Active");
            //run cannon
            FollowMouse();
            AimCrosshair();
			Cursor.visible = false;
			Cursor.lockState = CursorLockMode.Confined;
			if (Input.GetKey("q")) {
                CharacterController cc = player.GetComponent<CharacterController>();
                this.transform.localRotation = Quaternion.Euler(Vector3.zero); 
                cc.enabled = true;
                newCamera.gameObject.SetActive(true);
                currentCamera.gameObject.SetActive(false);

				Cursor.visible = false;
				Cursor.lockState = CursorLockMode.Confined;
                GameManager.instance.ResetCrossHair();
                GameManager.instance.onCannon = false;

			}
        }
    }
    private void FollowMouse() {
        transform.LookAt(Camera.main.ScreenToWorldPoint(Input.mousePosition + new Vector3(0, 0, 120)));
        transform.Rotate(-90.0f, 90f, 0f, Space.Self);
    }
	private void AimCrosshair()
	{
		Vector2 pos;
		RectTransformUtility.ScreenPointToLocalPointInRectangle(canvas.transform as RectTransform, Input.mousePosition, canvas.worldCamera, out pos);
		Crosshair.position = canvas.transform.TransformPoint(pos);
	}
}
