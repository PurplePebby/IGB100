using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Cutscene : MonoBehaviour
{
	public List<GameObject> cutsceneImages;
	public GameObject nextCutscene;

	public GameObject nextButton;

	private int currentCutSceneIndex = 0;


	public void CutsceneForward()
	{
		//Debug.Log(currentCutSceneIndex);		
		if (currentCutSceneIndex < cutsceneImages.Count - 1)
		{
			cutsceneImages[currentCutSceneIndex].SetActive(false);
			currentCutSceneIndex++;
			cutsceneImages[currentCutSceneIndex].SetActive(true);
		}
		if (currentCutSceneIndex == cutsceneImages.Count - 1)
		{
			nextButton.SetActive(false);
		}

	}

	public void EndCutScene()
	{
		gameObject.SetActive(false);
	}

	public void PlayNextCutScene()
	{
		nextCutscene.SetActive(true);
	}
}
