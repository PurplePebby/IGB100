using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class pirateCollision : MonoBehaviour
{
    public float pirateHealth;
    public float Dmg;

    public ParticleSystem shipDestroy;


    private void OnCollisionEnter(Collision collision) {
        //Debug.Log("There was a collision");
        if (collision.gameObject.tag == "Pew") {
            collision.gameObject.GetComponent<ProjectilePhysics>().Impact();
            ///SOUND EFFECT
            ///
            //sound for boat hit
            SoundManager.instance.PlaySingle(SoundManager.instance.cannonHit);
            ///
            ///SOUND EFFECT
            ///
            //check health
            if (pirateHealth <= 0) {
                shipDestroy.Play();
    //            if (!shipDestroy.IsAlive())
    //            {
					
				//}
				//kill boat
				Destroy(this.gameObject);
				///SOUND EFFECT
				///
				//sound for boat killed
				SoundManager.instance.PlaySingle(SoundManager.instance.Explosion);
                ///
                ///SOUND EFFECT
                ///
                GameManager.instance.pirateShip = false;
            }
            else {
                pirateHealth = pirateHealth - Dmg;
            }
            Debug.Log("Pirate health" + pirateHealth);
            //lose health
        }
    }
}
