using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SelectDifficulty : MonoBehaviour
{
    string difficulty = "Easy";

    public void HandleInputData(int val)
    {
        if(val == 0)
        {
            difficulty = "Easy";
        }
        if(val == 1)
        {
            difficulty = "Medium";
        }
        if(val == 2)
        {
            difficulty = "Hard";
        }
        FindObjectOfType<PlayButton>().UpdateDifficulty(difficulty);
    }

}
