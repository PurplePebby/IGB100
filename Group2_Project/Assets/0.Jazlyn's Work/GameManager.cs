using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;
using static UnityEngine.Rendering.DebugUI;


public class GameManager : MonoBehaviour {
    public static GameManager instance = null;
    private GameObject scoreParent;
    private int score = 0;
    private int treasureCount = 0;
    private Text scoreText;
    
	[SerializeField]
	[Tooltip("The UI slider used to show health.")] private Slider healthBarSlider;

	[SerializeField]
	[Tooltip("The UI slider used to show oxygen.")] private Slider oxygenBarSlider;

	[SerializeField]
	private GameObject pauseMenu;
	[SerializeField]
	private GameObject gameOverScreen;

    [NonSerialized] public float drownDPS;

    private bool paused = false;
    public bool Paused
    {
        get { return paused; }
    }

	void Awake() {
        if (instance == null) {
            instance = this;
        }
        else if (instance != this) {
            Destroy(this.gameObject);
        }
    }

    void Start() {
        scoreParent = GameObject.Find("Score Text");
        scoreText = scoreParent.GetComponent<Text>();

        UpdateScore();

        

		Resume();
		Cursor.lockState = CursorLockMode.Locked;
	}

	private void Update()
	{
        if (Input.GetKeyDown(KeyCode.Escape))
        {
			if (Paused)
			{
				Resume();
				
			}
			else
			{
				Pause();
			}
		}		
	}

	#region player stats
	public void SetMaxHealth(float health) {
		healthBarSlider.maxValue = health;
		healthBarSlider.value = health;
	}
    public void SetHealth(float health) {
        healthBarSlider.value = health;
    }
	public void UpdateHealth(float value)
	{
		healthBarSlider.value = Mathf.Clamp(healthBarSlider.value + value, 0, healthBarSlider.maxValue);
		if (healthBarSlider.value <= 0)
		{
			GameOver();
		}
	}

    public void SetMaxOxygen(float oxygen)
    {
		oxygenBarSlider.maxValue = oxygen;
		oxygenBarSlider.value = oxygen;
	}
    public void SetOxygen(float oxygen)
    {
        oxygenBarSlider.value = oxygen;

	}
	public void UpdateOxygen(float value)
    {
		oxygenBarSlider.value = Mathf.Clamp(oxygenBarSlider.value + value, 0, oxygenBarSlider.maxValue);
        if (oxygenBarSlider.value <= 0)
        {
            Drowning();
        }
	}

    public void Drowning()
    {
        UpdateHealth(-drownDPS * Time.deltaTime);
	}
	#endregion

	private void UpdateScore() {
        scoreText.text = "Score: " + score;
    }

    public void AddScore(int newScoreValue) {
        score += newScoreValue;

        UpdateScore();
    }

    public void AddCount(int newScoreValue) {
        treasureCount += newScoreValue;
        Debug.Log("Amount of treasure in scene is "+ treasureCount);
    }

    public void RemoveScore(int newScoreValue) {
        score -= newScoreValue;

        UpdateScore();
    }

    public void GameOver() {
		paused = true;
		gameOverScreen.SetActive(true);
		Cursor.visible = true;
		Cursor.lockState = CursorLockMode.None;
	}

    public void Pause()
    {
        paused = true;
        pauseMenu.SetActive(true);
		Cursor.visible = true;
		Cursor.lockState = CursorLockMode.None;
	}

    public void Resume()
    {
        paused = false;
        pauseMenu.SetActive(false);
		Cursor.lockState = CursorLockMode.Locked;
	}

}
