using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// This script manages the player's shield/health system, including damage handling, regeneration, and triggering the game-over sequence when health reaches zero.
public class Shield : MonoBehaviour
{
    [SerializeField] int maxHealth = 20;
    [SerializeField] int curHealth;
    [SerializeField] float regenarationRate = 2f; // how often shield regeneration occurs (in seconds).
    [SerializeField] int regenarateAmount = 10; // how much shield is restored per tick.
    [SerializeField] UnityEngine.GameObject gameOverCamera;
    [SerializeField] GameObject gameOver;
    [SerializeField] GameObject menu;

    public GameOverScreen GameOverScreen;

    void Start()
    {
        gameOver.SetActive(false);
        curHealth = maxHealth;      // Initializes shield to full health.
        InvokeRepeating("Regenerate", regenarationRate, regenarationRate);  // Starts a repeating method call to Regenerate() every regenerationRate seconds.
    }

    void Regenerate()
    {
        // Increases the shield up to its maximum.
        // Sends an update through EventManager (presumably to update UI like a health bar), passing the normalized health value (between 0 and 1).

        if (curHealth < maxHealth)
            curHealth += regenarateAmount;

        if (curHealth > maxHealth)
            curHealth = maxHealth;

        EventManager.TakeDamage(curHealth / (float)maxHealth);
    }

    public void TakeDamage(int dmg = 10)
    {
        curHealth -= dmg;   // Reduces shield health by dmg (default is 10).
        if (curHealth < 0)  // Prevents curHealth from going below zero.
            curHealth = 0;
        EventManager.TakeDamage(curHealth / (float)maxHealth);  // Notifies the system about the new health level.
        if (curHealth < 1)
        {
            // player is dead -> trigger PlayerDeath() event and trigger visual effect BlowUp().
            EventManager.PlayerDeath();
            GetComponent<Explosion>().BlowUp();
        }
    }

}
