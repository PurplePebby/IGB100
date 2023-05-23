using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyHurtDetection : MonoBehaviour
{
	[SerializeField]
	[Tooltip("The time before the player can be hurt again in seconds.")] private float iFrames = 0.5f;
	private bool invincible = false;
	[SerializeField]
	[Tooltip("The main Shark so the health can be changed.")] GameObject fish;

	private FishStats fishScript;

	private void Awake()
	{
		fishScript = fish.GetComponent<FishStats>();
	}
	private void OnTriggerEnter(Collider other)
	{
		if (other.CompareTag("Spear") && !invincible)
		{
			fishScript.DamageHealth(other.GetComponentInParent<MelleAttack>().damage);
			invincible = true;
			StartCoroutine(InvincibleTimer());
			//Debug.Log("You got me");
		}
	}

	private IEnumerator InvincibleTimer()
	{
		yield return new WaitForSeconds(iFrames);
		invincible = false;
	}
}
