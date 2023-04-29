using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerHurtController : MonoBehaviour
{
	[SerializeField]
	[Tooltip("The speed that the player regains their breath. Measured in seconds of oxygen per second.")] private float breathSpeed;
    [SerializeField]
    [Tooltip("The time before the player can be hurt again in seconds.")] private float iFrames = 0.5f;
	private bool invincible = false;

	[SerializeField]
    [Tooltip("The point that marks the top of the water.")] private GameObject waterLevelPoint;
	// Start is called before the first frame update
	private void Start()
	{
	}
	// Update is called once per frame
	void Update()
    {
        if (transform.position.y < waterLevelPoint.transform.position.y)
        {
            GameManager.instance.UpdateOxygen(-1 * Time.deltaTime);
        }
        else
        {
            GameManager.instance.UpdateOxygen(breathSpeed * Time.deltaTime);
		}
    }

	private void OnTriggerEnter(Collider other)
	{
		if (other.CompareTag("Enemy") && !invincible){
			GameManager.instance.UpdateHealth(-other.GetComponentInParent<MoveSharkie>().damage);
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
