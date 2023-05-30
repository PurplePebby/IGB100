using StarterAssets;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class playerManager : MonoBehaviour
{
	[SerializeField]
	[Tooltip("The player's max health.")] private float maxHealth;
	[SerializeField]
	[Tooltip("The player's max oxygen.")] private float maxOxygen;

    [SerializeField]
    [Tooltip("The UI slider used to show money.")] private float maxMoney;

    [SerializeField]
	[Tooltip("How fast the player will take damage when drowning.")] private float drownDPS = 1;
	[SerializeField]
	[Tooltip("How long it take for the player to start losing oxygen.")] private float oxygenDepletionDelay = 2f;
	[SerializeField]
	[Tooltip("The speed that the player regains their breath. Measured in seconds of oxygen per second.")] private float breathSpeed;
	[SerializeField]
	[Tooltip("How high above the water the player needs to be in order to breath.")] private float breathHeightOffset;
	[SerializeField]
	[Tooltip("Does the player gain oxygen when above water.")] private bool refillAboveWater = false;


	private PlayerMovementController playerMove;
	private bool canDrown = false;

	public void Awake()
	{
		playerMove = gameObject.GetComponent<PlayerMovementController>();
	}
	// Start is called before the first frame update
	void Start()
    {
		GameManager.instance.SetMaxHealth(maxHealth);
		GameManager.instance.SetMaxOxygen(maxOxygen);
		GameManager.instance.SetMaxMoney(maxMoney);

		GameManager.instance.drownDPS = drownDPS;
	}

    // Update is called once per frame
    void Update()
    {
		if (!GameManager.instance.Paused)
		{
			UnderWaterCheck();
		}
	}

	private void UnderWaterCheck()
	{
		if (transform.position.y < GameManager.instance.waterLevel + breathHeightOffset)
		{
			if (canDrown)
			{
				GameManager.instance.UpdateOxygen(-1 * Time.deltaTime);
			}
			else
			{
				StartCoroutine(Timer());
			}
		}
		else
		{
			canDrown = false;
			if (refillAboveWater)
			{
				GameManager.instance.UpdateOxygen(breathSpeed * Time.deltaTime);
			}
		}
		if (transform.position.y < GameManager.instance.waterLevel - 1f)
		{
			playerMove.underWater = true;

		}
		else
		{
			playerMove.underWater = false;

		}
	}
	private IEnumerator Timer()
	{
		yield return new WaitForSeconds(oxygenDepletionDelay);
		canDrown = true;
	}
}
