using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

// This script manages the behavior of the Game Over screen in the game. It displays the final timer and score when the game ends and makes the corresponding UI visible.
public class GameOverScreen : MonoBehaviour
{
    [SerializeField] GameObject gameOverUI;
    [SerializeField] TextMeshProUGUI timerText;
    [SerializeField] TextMeshProUGUI timerTextForGameOver;
    [SerializeField] TextMeshProUGUI scoreText;
    [SerializeField] TextMeshProUGUI scoreTextForGameOver;

    void Start()
    {
        gameOverUI.SetActive(false); // at start this screen is hidden by default
    }

    public void showGameOverUI()
    {
        // Copies the current in-game timer and score values to the Game Over version of the texts. Makes the Game Over screen visible.
        timerTextForGameOver.text = timerText.text;
        scoreTextForGameOver.text = scoreText.text;
        gameOverUI.SetActive(true);
    }

    // Subscribes and unsubscribes to a custom event called onGameOver managed by an EventManager.
    // When the game over event is fired, the showGameOverUI() method will be executed.
    void OnEnable()
    {
        EventManager.onGameOver += showGameOverUI;
    }

    void OnDisable()
    {
        EventManager.onGameOver -= showGameOverUI;
    }

}

