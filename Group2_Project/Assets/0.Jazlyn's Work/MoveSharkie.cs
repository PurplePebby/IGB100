using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoveSharkie : MonoBehaviour
{
    public Transform position;
    private float speed = 5.0f;

    void Start() {
        position = GameObject.Find("PlayerCapsule").transform;
    }

    void Update() {
        float move = speed * Time.deltaTime;
        FollowPlayer();
        transform.position = Vector3.MoveTowards(transform.position, position.position, move);
    }

    private void FollowPlayer() {
        transform.LookAt(position);

        //remove this if shark model is facing in the wrong direction
        transform.Rotate(-90f, 0f, 0f, Space.Self);
    }
}
