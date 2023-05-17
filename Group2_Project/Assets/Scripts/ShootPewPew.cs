using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static UnityEditor.PlayerSettings;

public class ShootPewPew : MonoBehaviour
{
    [SerializeField]
    private GameObject obj;

    [SerializeField]
    private Transform spawnPosition;

    public float fireRate = 0.15f;
    private float fireTime;

    void Update() {
        InstantiateProjectile();
    }

    private void InstantiateProjectile() {

        if (Input.GetKeyDown("space") && Time.time > fireTime) {
            Instantiate(obj, spawnPosition.position, spawnPosition.rotation);
            fireTime = Time.time + fireRate;
        }
    }
}
