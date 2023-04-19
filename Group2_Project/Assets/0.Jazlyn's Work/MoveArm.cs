using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoveArm : MonoBehaviour
{
    public GameObject arm;
    public Camera cam;


    // Update is called once per frame
    void Update()
    {
        FollowMouse();
    }

    private void FollowMouse() {
        arm.transform.LookAt(cam.ScreenToWorldPoint(Input.mousePosition + new Vector3(0, 0, 120)));
        arm.transform.Rotate(-60.0f, 0f, 0f, Space.Self);
    }
}
