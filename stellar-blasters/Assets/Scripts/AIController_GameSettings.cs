using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AIController_GameSettings : MonoBehaviour
{
    public bool solo;
    public float movementSpeed;

    void OnEnable()
    {
        EventManager.onStartGame += GameMode;
    }

    void OnDisable()
    {
        EventManager.onStartGame -= GameMode;
    }

    void GameMode(string mode, string difficulty)
    {
        if (mode.Equals("Solo Shooter"))
        {
            solo = true;
            if(difficulty.Equals("Easy"))
            {
                movementSpeed = 7f;
            }
            else if(difficulty.Equals("Medium"))
            {
                movementSpeed = 10f;
            }
            else
            {
                movementSpeed = 12f;
            }
        }
        else if (mode.Equals("Enemy Engage"))
        {
            solo = false;
            if(difficulty.Equals("Easy"))
            {
                movementSpeed = 5f;
            }
            else if(difficulty.Equals("Medium"))
            {
                movementSpeed = 6.5f;
            }
            else
            {
                movementSpeed = 7.5f;
            }        
        }
    }
}
