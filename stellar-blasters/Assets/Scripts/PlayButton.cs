using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayButton : MonoBehaviour
{   

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
        EventManager.StartGame(mode, difficulty);
    }
}
