using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ButtonManager : MonoBehaviour
{

    public void StartButton()
    {
		SceneManager.LoadScene("Main_2");

	}

	public void QuitButton()
	{
		Application.Quit();
	}
}
