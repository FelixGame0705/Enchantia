using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class WaveTimeController : MonoBehaviour
{
    [SerializeField] private Text waveTxt;
    [SerializeField] private Text countdownTxt;
    [SerializeField] private float countdownTime = 60.0f; // Set your countdown time here
    [SerializeField] private float currentTime;

    private void Start()
    {
        currentTime = countdownTime;
        UpdateTimerText();
        StartCoroutine(Countdown());
    }

    public IEnumerator Countdown()
    {
        while (currentTime > 0)
        {
            yield return new WaitForSeconds(1.0f);
            currentTime--;
            UpdateTimerText();
        }

        // Timer has reached zero; you can perform some actions here
        countdownTxt.text = "Time's up!";
        GamePlayController.Instance.LevelWave();
    }

    private void UpdateTimerText()
    {
        int minutes = Mathf.FloorToInt(currentTime / 60);
        int seconds = Mathf.FloorToInt(currentTime % 60);
        countdownTxt.text = string.Format("{0:00}:{1:00}", minutes, seconds);
    }

    public void SetCoundownTime(float seconds)
    {
        currentTime = seconds;
    }

    public void SetWave(string wave)
    {
        waveTxt.text = wave;
    }
}
