using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FishStats : MonoBehaviour
{
	[Tooltip("The amount of damage dealt in DPS.")] public float damage = 1f;
    [Tooltip("The starting health of the fish.")] public float maxHealth = 5f;

	private float health;

	public void Start()
	{
		SetMaxHealth();
	}

	private void SetMaxHealth()
	{
		health = maxHealth;
	}
	public void DamageHealth(float value)
	{
		health = Mathf.Clamp(health - value, 0f, maxHealth);
		//sound for....
		SoundManager.instance.PlaySingle(SoundManager.instance.enemyHurt);
		if (health <= 0f)
		{
			//sound for....
			SoundManager.instance.PlaySingle(SoundManager.instance.enemyDies);
			Destroy(gameObject);
		}
	}
}
