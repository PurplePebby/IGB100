using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class ShootPewPew : MonoBehaviour
{
    [SerializeField]
    private GameObject obj;

    [SerializeField]
    private Transform spawnPosition;
    

    public float fireRate = 0.15f;
    private float fireTime;

    public ParticleSystem cannonFire;

    void Update() {
        InstantiateProjectile();
    }

    private void InstantiateProjectile() {

        if (Input.GetButtonDown("Fire1") && Time.time > fireTime && GameManager.instance.onCannon && !GameManager.instance.Paused) {
            ///SOUND
            ///
            //sound for shooting an object
            SoundManager.instance.PlaySingle(SoundManager.instance.cannonFire);
            ///
            ///SOUND
            cannonFire.Play();
			Instantiate(obj, spawnPosition.position, spawnPosition.rotation);
            fireTime = Time.time + fireRate;
        }
    }

    
    
}
