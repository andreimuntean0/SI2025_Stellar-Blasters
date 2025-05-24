using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class Timer : MonoBehaviour
{
    [SerializeField]float timePassed;
    bool keepTime = false;
    [SerializeField]TextMeshProUGUI timerText;
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
        if(keepTime)
        {
            timePassed += Time.deltaTime;
            UpdateTimerDisplay();
        }
    }

    void StartTimer(string mode, string difficulty)
    {
        keepTime = true;
        timePassed = 0;
    }

    void StopTimer()
    {
        keepTime = false;
    }

    void UpdateTimerDisplay()
    {
        float minutes;
        float seconds;
        minutes = Mathf.FloorToInt(timePassed/60);
        seconds = timePassed%60;

        timerText.text = string.Format("{0}:{1:00.00}", minutes, seconds);
    }
}
