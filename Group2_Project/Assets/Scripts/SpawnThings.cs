using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnThings : MonoBehaviour
{
    public List<GameObject> spawnLocations;

    public List<GameObject> treasures;
    
    private List<int> Spawnned = new List<int>();


    // Start is called before the first frame update
    void Start()
    {
        SpawnTreasure();
    }

    // Update is called once per frame
    void Update()
    {
        
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
        
        for (int i = 0; i <= 4; i++) {
            int a = SelectTreasure();
            int b = SelectLocation();
            if (Spawnned.Contains(b) == false || Spawnned == null) {
                Instantiate(treasures[a], spawnLocations[b].transform.position, spawnLocations[b].transform.rotation); 
                Spawnned.Add(b);
            }
        }
    }


}
