using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PirateMechanic : MonoBehaviour {

    public GameObject pirateShip;
    public Transform spawnPos;
    public GameObject pirateShip_inScene;



    [Tooltip("Seconds between pirates attacks")]
    public float timeBetweenRaids = 20;
    public float instantiateRate;
    public bool pirateSpawn = false;

    public Slider pirateSlider;

    [Tooltip("DONT CHANGE THIS, JUST WATCH IT")]
    private float treasureCount => GameManager.instance.moneyBarSlider.value;

    
    [Tooltip("Minimum treasure score until pirate timer starts")]
    [SerializeField]
    private int minAmt;


    private void Start() {
        pirateSlider.maxValue = timeBetweenRaids;
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
        if (pirateSlider.value >= timeBetweenRaids) {
			pirateSlider.value += Time.deltaTime;
            Debug.Log("Time until raid " + pirateSlider.value);
            pirateSlider.value = pirateSlider.value;
        }
        else {
			pirateSlider.value = 0;
            Debug.Log("Spawnned Ship");
            Instantiate(pirateShip, spawnPos.position, spawnPos.rotation);
            pirateSpawn = true;
        }
        yield break;
    }

    private void InstantiateCubeTimer() {
        if (Time.time > pirateSlider.value) {
            Instantiate(pirateShip, spawnPos.position, spawnPos.rotation);
			pirateSlider.value = Time.time + instantiateRate;
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