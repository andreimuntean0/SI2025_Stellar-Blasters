using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// This script is attached to the “Play” button in the main menu UI. It handles the selection of game mode and difficulty, and starts the game when the button is clicked.
public class PlayButton : MonoBehaviour
{

    // These are the default values for difficulty and game mode. If the player doesn’t manually change them, the game starts with these defaults.
    string difficulty = "Easy";
    string mode = "Solo Shooter";

    public void UpdateDifficulty(string diff)
    {
        difficulty = diff;
    }

    public void UpdateMode(string md)
    {
        mode = md;
    }

    public void Click()
    {
        // Called when the Play button is pressed. It triggers the StartGame method in EventManager, passing the selected mode and difficulty.
        // This action notifies all subscribed scripts (e.g., UI handler, game manager, asteroid generator) to begin the game with the specified settings.
        EventManager.StartGame(mode, difficulty);
    }
}
