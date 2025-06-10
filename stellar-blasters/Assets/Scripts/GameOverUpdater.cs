using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

// This Unity script is responsible for updating the Game Over screen with final values for the timer, score, and high score when the game ends. 
// It does not show/hide the UI itself—just updates the text fields.
public class GameOverUpdater : MonoBehaviour
{
    [SerializeField] TextMeshProUGUI timerText;
    [SerializeField] TextMeshProUGUI timerTextForGameOver;
    [SerializeField] TextMeshProUGUI scoreText;
    [SerializeField] TextMeshProUGUI scoreTextForGameOver;

    [SerializeField] TextMeshProUGUI highscoreText;
    [SerializeField] TextMeshProUGUI highscoreTextForGameOver;

    // Start is called before the first frame update
    public void showGameOverUI()
    {
        // Copies the current timer, score, and high score from the active gameplay UI to the Game Over screen equivalents.
        // Ensures players see their final performance data at the end of the game.
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
