using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Movepath : MonoBehaviour
{
	//create empty game objects & place them around the scene as the path for the fish to take
	public GameObject points;
	private Transform[] pointsArray;
	int current;
	public float speed = 5f;

    [SerializeField]
    private Vector3 rotation;

    private Vector3 positionOffset;
    
    private Vector3 currentPosition;
    private float angle;

    private Quaternion lookRotation;
    private Vector3 direction;
    [SerializeField]
    private float turnSpeed;
    // Start is called before the first frame update
    void Start()
    {
		List<Transform> pointsList = new List<Transform>();
		current = 0;
		foreach (Transform child in points.transform)
		{
			pointsList.Add(child.transform);
		}
		pointsArray = pointsList.ToArray();
	}

    // Update is called once per frame
    void Update()
    {
        MovePath();
    }

    private void MovePath() {
        //enemy pathing https://www.youtube.com/watch?v=BGe5HDsyhkY
        ; if (transform.position != pointsArray[current].position) {
            transform.position = Vector3.MoveTowards(transform.position, pointsArray[current].position, speed * Time.deltaTime);
            //find the vector pointing from our position to the target
            direction = (pointsArray[current].position - transform.position).normalized;

            //create the rotation we need to be in to look at the target
            lookRotation = Quaternion.LookRotation(direction);

            //rotate us over time according to speed until we are in the required rotation
            transform.rotation = Quaternion.Slerp(transform.rotation, lookRotation, Time.deltaTime * turnSpeed);
        }
        else {
            current = (current + 1) % pointsArray.Length;
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
