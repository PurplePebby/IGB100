using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ButtonManager : MonoBehaviour
{
	public GameObject[] cutsceneImages;

	private int currentCutSceneIndex = 0;

	public void RestartButton()
    {
		SceneManager.LoadScene("Main_2");

	}

	public void PrevScene()
	{
		SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex - 1);
	}

	public void NextScene()
	{
		SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
	}

	public void CutsceneForward()
	{
		if (currentCutSceneIndex < cutsceneImages.Length - 1)
		{
			Debug.Log(currentCutSceneIndex);
			cutsceneImages[currentCutSceneIndex].SetActive(false);
			currentCutSceneIndex++;
			cutsceneImages[currentCutSceneIndex].SetActive(true);
		}
		else
		{
			NextScene();
		}
		
	}

	public void QuitButton()
	{
		Application.Quit();
	}

	
}
