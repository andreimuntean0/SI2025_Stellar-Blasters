using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SelectGameMode : MonoBehaviour
{
    string mode = "Solo Shooter";

    public void HandleInputData(int val)
    {
        if(val == 0)
        {
            mode = "Solo Shooter";
        }
        else
        {
            mode = "Enemy Engage";
        }
        FindObjectOfType<PlayButton>().UpdateMode(mode);
    }
}
