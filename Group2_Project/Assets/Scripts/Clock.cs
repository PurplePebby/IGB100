using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Clock : MonoBehaviour
{
    public float TimeLeft = 120;
    public bool TimerOn = false;

    private bool coroutine = false;
    private bool isTrue = false;

    void Update() {
        StartCoroutine(Countdown());
        isTrue = true;
    }

    private void IsTrue() {
        if (coroutine) {
            return;
        }
        if (TimerOn) {
            StartTimer();
        }
    }

    private void StartTimer() {
        StartCoroutine(Countdown());
    }

    private IEnumerator Timer() {
        Debug.Log("Hello");
        yield return new WaitForSeconds(3);
        Debug.Log("Hello... Again");
        coroutine = false;
        isTrue = false;
    }

    private IEnumerator Countdown() {
        if (TimeLeft > 0) {
            TimeLeft -= Time.deltaTime;
            updateTimer(TimeLeft);
        }
        else {
            Time.timeScale = 0;

            TimeLeft = 0;
            TimerOn = false;
        }
        yield break;

    }

    void updateTimer(float currentTime) {
        currentTime += 1;

        float minutes = Mathf.FloorToInt(currentTime / 60);
        float seconds = Mathf.FloorToInt(currentTime % 60);

        //GameManager.instance.AddTime(minutes, seconds);
    }
}
