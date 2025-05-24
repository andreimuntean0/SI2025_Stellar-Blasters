using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class Score : MonoBehaviour
{
    [SerializeField]TextMeshProUGUI hiScoreText;
    [SerializeField]TextMeshProUGUI scoreText;

    [SerializeField]int score;
    [SerializeField]int hiScore;

    void Start()
    {
        LoadHiScore();
    }

    void OnEnable()
    {
        EventManager.onStartGame    += ResetScore;
        EventManager.onPlayerDeath  += CheckNewHiScore;
        EventManager.onScorePoints  += AddScore;
    }

    void OnDisable()
    {
        EventManager.onStartGame    -= ResetScore;
        EventManager.onPlayerDeath  -= CheckNewHiScore;
        EventManager.onScorePoints  -= AddScore;
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
        hiScore = PlayerPrefs.GetInt("hiScore", 0);
        DisplayHighScore();
    }

    void CheckNewHiScore()
    {
        if(score > hiScore)
        {
            hiScore = score;
            PlayerPrefs.SetInt("hiScore", score);
            PlayerPrefs.Save();
            DisplayHighScore();
        }
    }

    void DisplayHighScore()
    {
        hiScoreText.text = hiScore.ToString();
    }   
}
