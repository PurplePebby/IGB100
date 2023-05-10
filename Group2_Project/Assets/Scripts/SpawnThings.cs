using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnThings : MonoBehaviour
{
    public List<GameObject> spawnLocations;

    public List<GameObject> treasures;
    
    private List<int> Spawnned = new List<int>();

    private int treasureCount;
    public float instantiateRate = 10f;
    private float nextInstantiate;


    // Start is called before the first frame update
    void Start()
    {
        SpawnTreasure();
        nextInstantiate = Time.time + instantiateRate;
        
    }

    // Update is called once per frame
    void Update()
    {
        //if (Input.GetKeyDown("e")) {
        //    Spawnned.Clear();
        //    SpawnTreasure();
        //}
        //if (treasureCount > 3) {
            
        //}
        //if (Time.time > nextInstantiate && treasureCount < 5) {
        //    ideally would like to add a check
            
        //    nextInstantiate = Time.time + instantiateRate;
        //}
    }

    //randomly select a treasure to spawn
    private int SelectTreasure() {
        int selected = Random.Range(0, treasures.Count);
        return selected;
    }

    //randonly selects spawn location
    private int SelectLocation() {
        int selected = Random.Range(0, spawnLocations.Count);
        return selected;
    }
    private void SpawnTreasure() {
        //Debug.Log("Something Spawnned");\
        for (int i = 0; i <= 10; i++) {
            int a = SelectTreasure();
            int b = SelectLocation();
            if (Spawnned.Contains(b) == false || Spawnned == null) {
                Instantiate(treasures[a], spawnLocations[b].transform.position, spawnLocations[b].transform.rotation);
                Debug.Log("Spawn Location is: " + spawnLocations[b]);
                GameManager.instance.AddCount(1);
                Spawnned.Add(b);
            }
        }      
    }


}
