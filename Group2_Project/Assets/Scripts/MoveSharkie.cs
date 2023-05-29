using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEditor.Rendering;
using UnityEngine;

public class MoveSharkie : MonoBehaviour
{
    //create empty game objects & place them around the scene as the path for the fish to take
    public Transform[] points;
    int current;
    public float speed = 5f;

    [SerializeField]
    private Vector3 rotation;

    private Transform PlayerPosition;

    public float CircleRadius = 14;

    public float ElevationOffset = 0;

    private Vector3 positionOffset;
    private Vector3 currentPosition;
    private float angle;


    private Quaternion _lookRotation;
    private Vector3 _direction;
    [SerializeField]
    private float turnSpeed;
    [SerializeField]
    private float waterLvl => GameManager.instance.waterLevel;
    public void Start() {
        current = 0;
    }

    void Update() {
        //from https://answers.unity.com/questions/669598/detect-if-player-is-in-range-1.html
        if (transform.position.y < waterLvl && PlayerPosition != null) {
            StartCoroutine(FollowPlayer());            
        }
        else {
            FishySwimPath();
        }
     
    }


    
    void OnTriggerEnter(Collider other) {
        //from https://answers.unity.com/questions/669598/detect-if-player-is-in-range-1.html
        if (other.tag == "Player") PlayerPosition = other.transform;
    }

    void OnTriggerExit(Collider other) {
        //from https://answers.unity.com/questions/669598/detect-if-player-is-in-range-1.html
        if (other.tag == "Player") PlayerPosition = null;
    }

    IEnumerator FollowPlayer() {
        float move = speed * Time.deltaTime;
        LookAtThing(PlayerPosition, 1);
        transform.position = Vector3.MoveTowards(transform.position, PlayerPosition.position, move);
        yield return null;
    }

    private void LookAtThing(Transform P, int a) {
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

    private void FishySwimPath() {
        //enemy pathing https://www.youtube.com/watch?v=BGe5HDsyhkY
        ; if (transform.position != points[current].position) {            
            transform.position = Vector3.MoveTowards(transform.position, points[current].position, speed * Time.deltaTime);
            //find the vector pointing from our position to the target
            _direction = (points[current].position - transform.position).normalized;

            //create the rotation we need to be in to look at the target
            _lookRotation = Quaternion.LookRotation(_direction);

            //rotate us over time according to speed until we are in the required rotation
            transform.rotation = Quaternion.Slerp(transform.rotation, _lookRotation, Time.deltaTime * turnSpeed);
        }
        else {
            current = (current + 1) % points.Length;
        }
        
    }

    IEnumerator FishySwimPathV2() {
        //rotate around a point https://answers.unity.com/questions/1614738/how-do-make-an-object-move-in-a-circle-around-the.html
        positionOffset.Set(Mathf.Cos(angle) * CircleRadius, ElevationOffset, 
            Mathf.Sin(angle) * CircleRadius);
        //make the side vector point at it, so fishie always facing 'forwards'
        LookAtThing(points[current], 0);
        currentPosition = points[current].position + positionOffset;
        transform.position = currentPosition;
        angle += Time.deltaTime/2;
        yield return null;
    }
    
    IEnumerator goHome(Vector3 targetPosition, float duration) {
        //with help from https://gamedevbeginner.com/the-right-way-to-lerp-in-unity-with-examples/#lerp_vector3
        float time = 0;
        Vector3 startPosition = transform.position;
        while (time < duration) {
            transform.position = Vector3.Lerp(startPosition, targetPosition, time / duration);
            //transform.position = Vector3.Lerp(startPosition, targetPosition, time / duration);
            time += Time.deltaTime;
            yield return null;
        }
        //transform.position = targetPosition;
    }
}