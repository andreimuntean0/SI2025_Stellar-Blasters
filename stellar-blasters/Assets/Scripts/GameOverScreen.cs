using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class GameOverScreen : MonoBehaviour
{
    [SerializeField]GameObject gameOverUI;
    [SerializeField]TextMeshProUGUI timerText;
    [SerializeField]TextMeshProUGUI timerTextForGameOver;
    [SerializeField]TextMeshProUGUI scoreText;
    [SerializeField]TextMeshProUGUI scoreTextForGameOver;

    void Start()
    {
        gameOverUI.SetActive(false);
    }

    public void showGameOverUI()
    {
        timerTextForGameOver.text = timerText.text;
        scoreTextForGameOver.text = scoreText.text;
        gameOverUI.SetActive(true);
    }

    void OnEnable()
    {
        EventManager.onGameOver += showGameOverUI;
    }

    void OnDisable()
    {
        EventManager.onGameOver -= showGameOverUI;
    }

}

