using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

// This script handles the game’s score tracking and high score system. 
// It listens to events such as starting a new game, earning points, and player death to update and persist scores using Unity’s PlayerPrefs.
public class Score : MonoBehaviour
{
    [SerializeField] TextMeshProUGUI hiScoreText;
    [SerializeField] TextMeshProUGUI scoreText;

    [SerializeField] int score;
    [SerializeField] int hiScore;

    void Start()
    {   
        // When the game starts, loads the previously saved high score from player preferences.
        LoadHiScore();
    }

    void OnEnable()
    {
        EventManager.onStartGame += ResetScore;
        EventManager.onPlayerDeath += CheckNewHiScore;
        EventManager.onScorePoints += AddScore;
    }

    void OnDisable()
    {
        EventManager.onStartGame -= ResetScore;
        EventManager.onPlayerDeath -= CheckNewHiScore;
        EventManager.onScorePoints -= AddScore;
    }

    void ResetScore(string mode, string difficulty)
    {
        score = 0;
        DisplayScore();
    }

    void AddScore(int amt)
    {
        score += amt;
        DisplayScore();
    }

    void DisplayScore()
    {
        scoreText.text = score.ToString();
    }

    void LoadHiScore()
    {
        // Loads the high score from persistent storage (default is 0).
        hiScore = PlayerPrefs.GetInt("hiScore", 0);
        DisplayHighScore();
    }

    void CheckNewHiScore()
    {
        // Checks if the current score is greater than the previous high score.
        if (score > hiScore)
        {
            // If so, updates and saves the new high score in PlayerPrefs.
            hiScore = score;
            PlayerPrefs.SetInt("hiScore", score);
            PlayerPrefs.Save();
            DisplayHighScore();
        }
    }

    void DisplayHighScore()
    {
        // Updates the UI to show the current high score.
        hiScoreText.text = hiScore.ToString();
    }
}
