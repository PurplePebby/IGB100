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
	[SerializeField, Tooltip("How high above the water the player needs to be in order to switch to land movement")]
	private float walkHeightOffset = -1f;
	[SerializeField]
	[Tooltip("Does the player gain oxygen when above water.")] private bool refillAboveWater = false;

	public ParticleSystem bubbles;

	// Reference to the character camera.
	[SerializeField]
	private Camera characterCamera;
	[SerializeField, Tooltip("This value is just used to determine if an enemy is within range of attack. To change atatck range change the animation")] private float attackDistance;

	private PlayerMovementController playerMove;
	private bool Drowning = false;
	private bool canBreath = true;

	private RaycastHit hit;

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
			CheckDistance();
			UnderWaterCheck();
		}
	}


	private void UnderWaterCheck()
	{
		if (transform.position.y < GameManager.instance.waterLevel + breathHeightOffset && canBreath)
		{
			StartCoroutine(DrownTimer());
			canBreath = false;
			bubbles.Play();
			//sound for underwater music
			SoundManager.instance.PlayMusic(SoundManager.instance.underWaterSounds);

		}
		else if (transform.position.y > GameManager.instance.waterLevel + breathHeightOffset && !canBreath)
		{
			canBreath = true;
			bubbles.Stop();
			GameManager.instance.O2WarningGiven = false;
			//sound for abovewater music
			SoundManager.instance.PlayMusic(SoundManager.instance.aboveWaterSounds);
		}
		if (transform.position.y < GameManager.instance.waterLevel + walkHeightOffset && !playerMove.underWater)
		{
			playerMove.underWater = true;
			
			
			

		}
		else if (transform.position.y > GameManager.instance.waterLevel + walkHeightOffset && playerMove.underWater)
		{
			playerMove.underWater = false;

		}
		if (canBreath)
		{
			Drowning = false;
			GameManager.instance.drowning = false;
		}
		if (Drowning)
		{
			GameManager.instance.UpdateOxygen(-1 * Time.deltaTime);
		}
		if (refillAboveWater && canBreath)
		{
			GameManager.instance.UpdateOxygen(breathSpeed * Time.deltaTime);
		}
	}
	private IEnumerator DrownTimer()
	{
		yield return new WaitForSeconds(oxygenDepletionDelay);
		Drowning = true;
	}

	private void CheckDistance()
	{
		var ray = characterCamera.ViewportPointToRay(Vector3.one * 0.5f);
		if (Physics.Raycast(ray, out hit, attackDistance) && !GameManager.instance.onCannon)
		{
			var interactable = hit.transform.GetComponent<InteractableThing>();
			//Debug.Log("interactable"+ hit.transform.GetComponent<InteractableThing>());
			if (hit.transform.CompareTag("Enemy"))
			{
				GameManager.instance.CrossHairColour(Color.red);
			}
		}
		else
		{
			if (!GameManager.instance.onCannon)
			{
				GameManager.instance.ResetCrossHair();
			}
			
		}
	}
}
