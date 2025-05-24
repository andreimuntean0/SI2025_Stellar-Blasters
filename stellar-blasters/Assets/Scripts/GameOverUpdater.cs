using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class GameOverUpdater : MonoBehaviour
{
    [SerializeField]TextMeshProUGUI timerText;
    [SerializeField]TextMeshProUGUI timerTextForGameOver;
    [SerializeField]TextMeshProUGUI scoreText;
    [SerializeField]TextMeshProUGUI scoreTextForGameOver;

    [SerializeField]TextMeshProUGUI highscoreText;
    [SerializeField]TextMeshProUGUI highscoreTextForGameOver;

    // Start is called before the first frame update
    public void showGameOverUI()
    {
        timerTextForGameOver.text = timerText.text;
        scoreTextForGameOver.text = scoreText.text;
        highscoreTextForGameOver.text = highscoreText.text;
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
