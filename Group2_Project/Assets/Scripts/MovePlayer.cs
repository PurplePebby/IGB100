using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[RequireComponent(typeof(CharacterController))]
public class MovePlayer : MonoBehaviour {
    //private CharacterController controller;
    //private Vector3 playerVelocity;
    //private bool groundedPlayer;
    //private float playerSpeed = 2.0f;
    //private float jumpHeight = 1.0f;
    //private float gravityValue = -4f;


    //Start is called before the first frame update
    //void Start() {
    //    controller = gameObject.GetComponent<CharacterController>();

    //}

    //Update is called once per frame
    //void Update() {
    //    Movement();
    //}

    //void Movement() {
    //    Vector3 move = new Vector3(Input.GetAxis("Horizontal"), 0, Input.GetAxis("Vertical"));
    //    controller.Move(move * Time.deltaTime * playerSpeed);
    //    if (move != Vector3.zero) {
    //        gameObject.transform.forward = move;
    //    }
    //}

    //void Jump() {
    //    if (Input.GetButtonDown("space")) {
    //        playerVelocity.y += -3.0f * gravityValue;
    //    }
    //    playerVelocity.y += gravityValue * Time.deltaTime;
    //    controller.Move(playerVelocity * Time.deltaTime);
    //}

    public float speed = 3.0f;
    public float rotateSpeed = 3.0f;
    public CharacterController controller;

    private void Start() {
        controller = GetComponent<CharacterController>();
    }
    void Update() {
        //if (Input.GetKeyDown(KeyCode.Space)) {
        //    Console.WriteLine("Spaced");
        //}
        swim();
        movement();
    }

    void movement() {
        // Rotate around y - axis
        transform.Rotate(0, Input.GetAxis("Mouse X") * rotateSpeed, 0);

        // Move forward / backward
        Vector3 forward = transform.TransformDirection(Vector3.forward);
        float curSpeed = speed * Input.GetAxis("Vertical");
        controller.SimpleMove(forward * curSpeed);

        // Move forward / backward
        Vector3 sideways = transform.TransformDirection(Vector3.right);
        float burSpeed = speed * Input.GetAxis("Horizontal");
        controller.SimpleMove(sideways * burSpeed);
    }

    void swim() {
        if (Input.GetKeyDown(KeyCode.Space)) {
            Debug.Log("Space");
            Vector3 upwards = transform.TransformDirection(Vector3.up);
            float swimSpeed = speed * 50;
            controller.SimpleMove(upwards * swimSpeed);
        }
    }
}
