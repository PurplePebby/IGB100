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
    [Tooltip("The player's max health.")] private float maxHealth;
	[SerializeField]
	[Tooltip("The UI slider used to show health.")] private Slider healthBarSlider;

	[SerializeField]
	[Tooltip("The player's max oxygen.")] private float maxOxygen;
	[SerializeField]
	[Tooltip("The UI slider used to show oxygen.")] private Slider oxygenBarSlider;
    [SerializeField]
    [Tooltip("How fast the player will take damage when drowning.")] private float drownDPS = 1;

	[SerializeField]
	private GameObject pauseMenu;
	[SerializeField]
	private GameObject gameOverScreen;
    [SerializeField]
    private GameObject Panels;

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
        
        
        scoreText = scoreParent.GetComponent<Text>();

        UpdateScore();

        SetMaxHealth(maxHealth);
        SetMaxOxygen(maxOxygen);

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
		healthBarSlider.value = Mathf.Clamp(healthBarSlider.value + value, 0, this.maxHealth);
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
		oxygenBarSlider.value = Mathf.Clamp(oxygenBarSlider.value + value, 0, this.maxOxygen);
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
        //Counts how much treasure has spawned into the scene
        //then prints it out to the debug
        treasureCount += newScoreValue;
        Debug.Log("Amount of treasure in scene is "+ treasureCount);
    }

    public void RemoveScore(int newScoreValue) {
        score -= newScoreValue;

        UpdateScore();
    }

    /// <summary>
    /// Can GameOVER, Pause and Resume be in a separate script?
    /// </summary>
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


    //GLOBAL COROUTINES
    
    public void ShowE(bool state) {
        //attach a canvas
        //display text
        Panels.SetActive(state);
      //  StartCoroutine(turnOffE());
        //yield return null;
    }

    public IEnumerator ShowIfInteract() {
        Panels.SetActive(true);
        yield return null;
        //   yield return null;
    }    
    
    public IEnumerator HideIfNoInteract() {
        Panels.SetActive(false);
        yield return null;
        //   yield return null;
    }


}
