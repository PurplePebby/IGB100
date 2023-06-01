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
    public float fulltimeBetween;
    public bool pirateSpawn = false;

    

    [Tooltip("DONT CHANGE THIS, JUST WATCH IT")]
    private float treasureCount => GameManager.instance.moneyBarSlider.value;

    
    [Tooltip("Minimum treasure score until pirate timer starts")]
    [SerializeField]
    private int minAmt;


    private void Start() {
        fulltimeBetween = timeBetweenRaids;
        GameManager.instance.pirateShip = false;
        GameManager.instance.SetMaxTime(timeBetweenRaids);
        GameManager.instance.addPirateTime(0);
    }

    void Update() {
        //start the countdown timer if the things are true
        if (treasureCount > minAmt) {
            if (GameManager.instance.pirateShip == false) {
                         
                StartCoroutine(StartPirateTimer()); 
            } 
        }


    }


    public IEnumerator StartPirateTimer() {
        if (GameManager.instance.pirateSlider.value < timeBetweenRaids) {
			GameManager.instance.addPirateTime(Time.deltaTime);
            //Debug.Log(GameManager.instance.pirateSlider.value);
            //Debug.Log("Time until attack" + (timeBetweenRaids-GameManager.instance.pirateSlider.value));
        }
        else {
            GameManager.instance.pirateSlider.value = 0;
            StartCoroutine(GameManager.instance.ShowPrompt("The pirates are here!"));
            Instantiate(pirateShip, spawnPos.position, spawnPos.rotation);
            ///SOUND EFFECT
            ///
            //sound for spawnning a pirate ship
            SoundManager.instance.PlaySingle(SoundManager.instance.pirates);
            ///
            ///SOUND EFFECT
            ///
            GameManager.instance.pirateShip = true;
 
            //Debug.Log("PirateSpwan=" + pirateSpawn);
        }
        yield break;
    }

}