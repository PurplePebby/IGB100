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
	[Tooltip("The speed that the player regains their breath. Measured in seconds of oxygen per second.")] private float breathSpeed;

	[SerializeField]
	[Tooltip("The point that marks the top of the water.")] private GameObject waterLevelPoint;

	private PlayerMovementController playerMove;

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
		UnderWaterCheck();

	}

	private void UnderWaterCheck()
	{
		if (transform.position.y < waterLevelPoint.transform.position.y - 0.5f)
		{
			GameManager.instance.UpdateOxygen(-1 * Time.deltaTime);
		}
		else
		{
			GameManager.instance.UpdateOxygen(breathSpeed * Time.deltaTime);
		}
		if (transform.position.y < waterLevelPoint.transform.position.y)
		{
			playerMove.underWater = true;
		}
		else
		{
			playerMove.underWater = false;
		}
	}
}
