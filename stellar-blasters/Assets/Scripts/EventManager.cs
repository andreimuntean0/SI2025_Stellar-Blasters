using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// The EventManager is a centralized class responsible for broadcasting important game events like starting the game, scoring points, taking damage, 
// and ending the game (either by death or game over). It uses delegates and events to decouple components and improve modularity.
public class EventManager : MonoBehaviour
{
    // A delegate type for starting the game, with parameters for game mode and difficulty.
    // onStartGame is the event that listeners can subscribe to when the game starts.
    public delegate void StartGameDelegate(string mode, string difficulty);
    public static StartGameDelegate onStartGame;

    // Delegates for stopping the game.
    // onPlayerDeath is triggered when the player dies.
    // onGameOver used for final cleanup and showing the Game Over UI.
    public delegate void StopGameDelegate();
    public static StopGameDelegate onPlayerDeath;
    public static StopGameDelegate onGameOver;

    // Triggered when the player takes damage. The float represents the remaining shield percentage.
    public delegate void TakeDamageDelegate(float amt);
    public static TakeDamageDelegate onTakeDamage;

    // Used to update the player's score
    public delegate void ScorePointsDelegate(int amt);
    public static ScorePointsDelegate onScorePoints;

    // --------------------------------------------------------------------------------------------------------------------------------------------------------------------

    public static void StartGame(string mode, string difficulty)
    {
        // Invokes onStartGame to notify all subscribers that a new game has started.
        if (onStartGame != null)
            onStartGame(mode, difficulty);
    }

    public static void TakeDamage(float percent)
    {
        // Called when the player's shield is damaged to update the UI or other components.
        if (onTakeDamage != null)
            onTakeDamage(percent);
    }

    public static void ScorePoints(int score)
    {
        // Broadcasts a change in score to subscribed listeners (like the score UI).
        if (onScorePoints != null)
            onScorePoints(score);
    }

    public static void PlayerDeath()
    {
        // Invokes both onPlayerDeath and onGameOver.
        if (onPlayerDeath != null)
        {
            onPlayerDeath();
            onGameOver();
        }
    }

}
