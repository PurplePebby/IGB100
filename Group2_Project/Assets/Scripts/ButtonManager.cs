using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ButtonManager : MonoBehaviour
{

    public void StartButton()
    {
        SceneManager.LoadScene("Main");
    }

	public void QuitButton()
	{
		Application.Quit();
	}
}