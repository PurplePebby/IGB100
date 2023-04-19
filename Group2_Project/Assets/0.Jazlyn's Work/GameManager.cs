using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;


public class GameManager : MonoBehaviour {
    public static GameManager instance = null;
    private GameObject parent;
    private int score = 0;
    private int treasureCount = 0;
    private Text scoreText;

    void Awake() {
        if (instance == null) {
            instance = this;
        }
        else if (instance != this) {
            Destroy(this.gameObject);
        }
    }

    void Start() {
        parent = GameObject.Find("Score Text");
        scoreText = parent.GetComponent<Text>();

        UpdateScore();
    }

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

}
