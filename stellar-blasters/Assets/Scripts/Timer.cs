using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class Timer : MonoBehaviour
{
    [SerializeField]float timePassed;  // keeps track of total time elapsed since the timer started.
    bool keepTime = false;  // a flag to control whether the timer should count.
    [SerializeField]TextMeshProUGUI timerText; // a UI element that displays the current time.
    void OnEnable()
    {
        EventManager.onStartGame += StartTimer;
        EventManager.onPlayerDeath += StopTimer;
    }

    void onDisable()
    {
        EventManager.onStartGame -= StartTimer;
        EventManager.onPlayerDeath -= StopTimer;
    }

    void Update()
    {
        // This method is called every frame. If the timer is active (keepTime is true), 
        // it increments timePassed using Time.deltaTime (the time since the last frame) and updates the displayed time.
        if (keepTime)
        {
            timePassed += Time.deltaTime;
            UpdateTimerDisplay();
        }
    }

    void StartTimer(string mode, string difficulty)
    {
        // Resets and starts the timer when a new game begins. It accepts two parameters, likely passed by EventManager.
        keepTime = true;
        timePassed = 0;
    }

    void StopTimer()
    {
        // Stops the timer upon game over or player death.
        keepTime = false;
    }

    void UpdateTimerDisplay()
    {
        // Converts timePassed into a formatted string of minutes and seconds (mm:ss.xx) and displays it in the UI.
        float minutes;
        float seconds;
        minutes = Mathf.FloorToInt(timePassed/60);
        seconds = timePassed%60;

        timerText.text = string.Format("{0}:{1:00.00}", minutes, seconds);
    }
}
