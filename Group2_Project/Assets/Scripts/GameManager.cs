using Cinemachine;
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
    private GameObject collectParent;
    private int score = 0;
    public int treasureCount = 0;
    private Text scoreText;
    private Text collectText;

    CinemachineBrain myBrain; 
    CinemachineVirtualCamera startCam;

    [SerializeField]
	[Tooltip("The UI slider used to show health.")] private Slider healthBarSlider;

	[SerializeField]
	[Tooltip("The UI slider used to show oxygen.")] private Slider oxygenBarSlider;

    [Tooltip("The UI slider used to show money.")] public Slider moneyBarSlider;

	[SerializeField]
	private GameObject pauseMenu;
	[SerializeField]
	private GameObject gameOverScreen;
	[SerializeField]
	private GameObject victoryCutscene;
	[SerializeField]
	private GameObject pirateCutscene;
	[SerializeField]
	private GameObject pirateEnding;
	[SerializeField]
	private GameObject grandmaEnding;
	[SerializeField]
	private GameObject Panels;

	[NonSerialized] public float drownDPS;

    private bool paused = false;
    public bool Paused
    {
        get { return paused; }
    }

	[NonSerialized] public bool onCannon = false;

	void Awake() {
        if (instance == null) {
            instance = this;
        }
        else if (instance != this) {
            Destroy(this.gameObject);
        }
    }

    void Start() {
        //startCam = GameObject.Find("PlayerFollowCamera").GetComponent<CinemachineVirtualCamera>();
        
        scoreParent = GameObject.Find("Score Text");
        scoreText = scoreParent.GetComponent<Text>(); 
        
        
        scoreText = scoreParent.GetComponent<Text>();

        UpdateMoney();

        

		Resume();
	}

	private void Update()
	{
        if (Input.GetKeyDown(KeyCode.Escape))
        {
			if (Paused)
			{
				Resume();
				HidePauseMenu();
				
				
			}
			else
			{
				Pause();
				ShowPauseMenu();
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

	#region Money
	public void SetMaxMoney(float money)
	{
		moneyBarSlider.maxValue = money;
	}

	private void UpdateMoney()
	{
		scoreText.text = "" + score;
		moneyBarSlider.value = moneyBarSlider.value + score;
		if (moneyBarSlider.value >= moneyBarSlider.maxValue)
		{
			Pause();
			victoryCutscene.SetActive(true);
		}
	}

	public void AddScore(int newScoreValue)
	{
		score += newScoreValue;

		UpdateMoney();
	}

	public void AddMoney(int newMoneyValue)
	{
		//Counts how much treasure has spawned into the scene
		//then prints it out to the debug
		treasureCount += newMoneyValue;
		//Debug.Log("Amount of treasure in scene is " + treasureCount);
	}

	public void RemoveMoney(int newMoneyValue)
	{

        score -= newMoneyValue;
        treasureCount -= newMoneyValue;

		UpdateMoney();
	}

	#endregion

	#region cutscenes and menus

	public void PlayPirateEnding()
	{
		pirateEnding.SetActive(true);
	}

	public void PlayGrandmaEnding()
	{
		grandmaEnding.SetActive(true);
	}

	public void ShowPauseMenu()
	{
		pauseMenu.SetActive(true);
	}

	public void HidePauseMenu()
	{
		pauseMenu.SetActive(false);
	}

	#endregion

	/// <summary>
	/// Can GameOVER, Pause and Resume be in a separate script?
	/// </summary>
	/// 
	#region Game states
	public void GameOver()
	{
		Pause();
		gameOverScreen.SetActive(true);
	}

	public void Pause()
	{
		paused = true;
		Time.timeScale = 0;
		Cursor.visible = true;
		Cursor.lockState = CursorLockMode.None;
	}

	

	public void Resume()
	{
		paused = false;
		Time.timeScale = 1;
		pauseMenu.SetActive(false);
		Cursor.visible = false;
		Cursor.lockState = CursorLockMode.Locked;
	}

	#endregion


	//GLOBAL COROUTINES

	public void ShowE(bool state) {
        //attach a canvas
        //display text
        Panels.SetActive(state);
      //  StartCoroutine(turnOffE());
        //yield return null;
    }

    public IEnumerator ShowIfInteract(string a) {
        Panels.SetActive(true);
        //Debug.Log(collectText);
        collectParent = GameObject.Find("InteractText");
        collectText = collectParent.GetComponent<Text>();
        collectText.text = "Press 'E' to " + a;
        yield return null;
        //   yield return null;
    }    
    
    public IEnumerator HideIfNoInteract() {
        Panels.SetActive(false);
        yield return null;
        //   yield return null;
    }


}