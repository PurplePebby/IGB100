using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ButtonSounds : MonoBehaviour
{
    public void PlayHoverSound()
    {
        SoundManager.instance.PlaySingle(SoundManager.instance.buttonHover);
    }

	public void PlayClickSound()
	{
		SoundManager.instance.PlaySingle(SoundManager.instance.buttonPress);
	}
}
