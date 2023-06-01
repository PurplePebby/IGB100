using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;

public class SpawnThings : MonoBehaviour
{
    public GameObject spawnLocations;
    private List<GameObject> spawnLocationsList = new List<GameObject> ();

    public List<GameObject> treasures;
    public int spawnAmt;
    
    private List<int> Spawnned = new List<int>();

    private int treasureCount;
	//public float instantiateRate = 10f;
	//private float nextInstantiate;


	// Start is called before the first frame update
	void Start()
    {
		foreach (Transform child in spawnLocations.transform)
		{
			spawnLocationsList.Add(child.transform.gameObject);
		}

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
        int selected = Random.Range(0, treasures.Count);
        return selected;
    }

    //randonly selects spawn location
    private int SelectLocation() {
        //Debug.Log("Is it null? "+spawnLocations[0]);
        int selected = Random.Range(0, spawnLocationsList.Count);
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
        for (int i = 0; i <= spawnAmt; i++) {
            int a = SelectTreasure();
            int b = SelectLocation();
            if (Spawnned.Contains(b) == false || Spawnned == null) {
                treasures[a].transform.localPosition = Vector3.zero;
                treasures[a].transform.localEulerAngles = Vector3.zero;
				GameObject newTreasure = Instantiate(treasures[a], spawnLocationsList[b].transform.position, treasures[a].transform.rotation);
				//Debug.Log($"The treasure is at {newTreasure.transform.position} it should be at {spawnLocations[b].transform.position}");
				//Debug.Log("Spawn Location is: " + spawnLocations[b]);
				GameManager.instance.AddTreasureCount(1);
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
