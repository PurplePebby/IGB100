using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Movepath : MonoBehaviour
{
    public Transform[] points;
    int current;
    public float speed = 5f;

    [SerializeField]
    private Vector3 rotation;

    private Vector3 positionOffset;
    
    private Vector3 currentPosition;
    private float angle;
    // Start is called before the first frame update
    void Start()
    {
        current = 0;
    }

    // Update is called once per frame
    void Update()
    {
        MovePath();
    }

    private void MovePath() {
        //enemy pathing https://www.youtube.com/watch?v=BGe5HDsyhkY
        ; if (transform.position != points[current].position) {
            LookAtNextPoint(points[current], 1);
            transform.position = Vector3.MoveTowards(transform.position, points[current].position, speed * Time.deltaTime);
        }
        else {
            current = (current + 1) % points.Length;
        }

    }

    private void LookAtNextPoint(Transform P, int a) {
        if (a == 0) {
            transform.LookAt(P);
            transform.Rotate(0f, 180f, 0f, Space.Self);
        }
        else {
            transform.LookAt(P);

            //remove this if shark model is facing in the wrong direction
            transform.Rotate(rotation, Space.Self);
        }

    }
}
