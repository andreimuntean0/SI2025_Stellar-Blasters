using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EventManager : MonoBehaviour
{
    public delegate void StartGameDelegate(string mode, string difficulty);
    public static StartGameDelegate onStartGame;
    public delegate void StopGameDelegate();

    public static StopGameDelegate onPlayerDeath;


    public static StopGameDelegate onGameOver;

    public delegate void TakeDamageDelegate(float amt);
    public static TakeDamageDelegate onTakeDamage;

    public delegate void ScorePointsDelegate(int amt);
    public static ScorePointsDelegate onScorePoints;

    public static void StartGame(string mode, string difficulty)
    {
        if(onStartGame != null)
            onStartGame(mode, difficulty);
    }

    public static void TakeDamage(float percent)
    {
        if(onTakeDamage != null)
            onTakeDamage(percent);
    }

     public static void ScorePoints(int score)
    {
        if(onScorePoints != null)
            onScorePoints(score);
    }

    public static void PlayerDeath()
    {
        if(onPlayerDeath != null)
        {
            onPlayerDeath();
            onGameOver();
        }
    }

}
