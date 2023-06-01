using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MelleAttack : MonoBehaviour
{
	public float damage = 1;

	private Animator animator;
    private BoxCollider spearCollider;
    
    // Start is called before the first frame update
    void Start()
    {
        animator = GetComponent<Animator>();
        spearCollider = GetComponent<BoxCollider>();
    }

    // Update is called once per frame
    void Update()
    {
        Attack();
    }

    private void Attack() {
        if (Input.GetButtonDown("Fire1") && !GameManager.instance.Paused) {
            ///SOUND
            ///
            //sound for attack
            //GameManager.instance.aM.playSoundeffect(GameManager.instance.aM.playerAttack);
            ///
            ///SOUND
            animator.SetBool("Attacking", true);
            spearCollider.enabled = true;

        }
        else if (Input.GetButtonUp("Fire1")) {
            animator.SetBool("Attacking", false);
            spearCollider.enabled = false;
        }
    }
}
