using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerHurtController : MonoBehaviour
{
	
    [SerializeField]
    [Tooltip("The time before the player can be hurt again in seconds.")] private float iFrames = 0.5f;
	private bool invincible = false;

	
	// Start is called before the first frame update
	private void Start()
	{
	}
	// Update is called once per frame
	void Update()
    {
        
    }

	private void OnTriggerEnter(Collider other)
	{
		if (other.CompareTag("Enemy") && !invincible){
			GameManager.instance.UpdateHealth(-other.GetComponentInParent<FishStats>().damage);
			invincible = true;
			StartCoroutine(InvincibleTimer());
		}
	}

	private IEnumerator InvincibleTimer()
	{
		yield return new WaitForSeconds(iFrames);
		invincible = false;
	}
}
