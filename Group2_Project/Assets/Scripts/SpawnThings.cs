using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;

public class SpawnThings : MonoBehaviour
{
    public List<GameObject> spawnLocations;

    public List<GameObject> treasures;
    
    private List<int> Spawnned = new List<int>();

    private int treasureCount;
    //public float instantiateRate = 10f;
    //private float nextInstantiate;


    // Start is called before the first frame update
    void Start()
    {
        treasureCount = GameManager.instance.treasureCount;
        if (checkTreasure() == true) {
            SpawnTreasure();
        }  
    }

    void Update() {
        treasureCount = GameManager.instance.treasureCount;
        StartCoroutine(spawnthings());
    }

    //randomly select a treasure to spawn
    private int SelectTreasure() {
        int selected = 0;//Random.Range(0, treasures.Count);
        return selected;
    }

    //randonly selects spawn location
    private int SelectLocation() {
        Debug.Log("Is it null? "+spawnLocations[0]);
        int selected = Random.Range(0, spawnLocations.Count);
        //Debug.Log("Selected location:" + selected);
        return selected;
    }

    public bool checkTreasure() {
        treasureCount = GameManager.instance.treasureCount;
        if (treasureCount > 2) {
            return false;
        }
        else {
            return true;
        }
    }

    public void SpawnTreasure() {
        //Debug.Log("Something Spawnned");\
        Spawnned.Clear();
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


    private IEnumerator spawnthings() {
        if (checkTreasure() == true) {
            SpawnTreasure();
        }
        yield return new WaitForSeconds(3);
    }

}
