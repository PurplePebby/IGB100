using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public class PirateMechanic : MonoBehaviour {

    public GameObject pirateShip;
    public Transform spawnPos;
    public GameObject pirateShip_inScene;



    [Tooltip("Seconds until the pirates attack")]
    public float timeUntilRaid = 20;
    public float instantiateRate;
    public bool pirateSpawn = false;



    
    [Tooltip("DONT CHANGE THIS, JUST WATCH IT")]
    private float treasureCount => GameManager.instance.moneyBarSlider.value;

    
    [Tooltip("Minimum treasure score until pirate timer starts")]
    [SerializeField]
    private int minAmt;


    private void Start() {
    }

    void Update() {
        //start the countdown timer if the things are true
        if (treasureCount > minAmt) {
            CheckForShip();
            if (pirateSpawn == false) {
                //Debug.Log("Passed check");
                //Debug.Log("Timer is On");           
                StartCoroutine(StartPirateTimer()); 
            } 
        }


    }


    public IEnumerator StartPirateTimer() {
        if (timeUntilRaid > 0) {
            timeUntilRaid -= Time.deltaTime;
            Debug.Log("TIme until raid " + timeUntilRaid);
        }
        else {
            timeUntilRaid = 0;
            Debug.Log("Spawnned Ship");
            Instantiate(pirateShip, spawnPos.position, spawnPos.rotation);
            pirateSpawn = true;
        }
        yield break;
    }

    private void InstantiateCubeTimer() {
        if (Time.time > timeUntilRaid) {
            Instantiate(pirateShip, spawnPos.position, spawnPos.rotation);
            timeUntilRaid = Time.time + instantiateRate;
        }
    }
    

    private IEnumerator CheckForShip() {
        if (GameObject.Find(pirateShip.transform.name) != null){
            pirateSpawn = false;
            Debug.Log("False Spawns");
        }
        yield return new WaitForSeconds(5f);

    }

}