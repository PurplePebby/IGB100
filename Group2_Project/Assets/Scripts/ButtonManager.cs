using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ButtonManager : MonoBehaviour
{

	public void RestartButton()
    {
		SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);

	}

	public void PrevScene()
	{
		SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex - 1);
	}

	public void NextScene()
	{
		SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
	}
	

	public void QuitButton()
	{
        //quit button managementfrom  https://community.gamedev.tv/t/how-do-i-make-the-quit-button-work-for-webgl/40403/4
		#if (UNITY_EDITOR || DEVELOPMENT_BUILD)
        Debug.Log(this.name + " : " + this.GetType() + " : " + System.Reflection.MethodBase.GetCurrentMethod().Name);
		#endif
		#if (UNITY_EDITOR)
				UnityEditor.EditorApplication.isPlaying = false;
		#elif (UNITY_STANDALONE)
			Application.Quit();
		#elif (UNITY_WEBGL)
			Application.OpenURL("about:blank");
		#endif
    }

    public void MainMenu()
	{
		SceneManager.LoadScene(0);
	}
}
