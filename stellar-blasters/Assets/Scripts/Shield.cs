using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Shield : MonoBehaviour
{
    [SerializeField]int maxHealth = 20;
    [SerializeField]int curHealth;
    [SerializeField]float regenarationRate = 2f;
    [SerializeField]int regenarateAmount = 10;
    [SerializeField]UnityEngine.GameObject gameOverCamera;
    [SerializeField]GameObject gameOver;
    [SerializeField]GameObject menu;

    public GameOverScreen GameOverScreen;

    void Start()
    {
        gameOver.SetActive(false);
        curHealth = maxHealth;
        InvokeRepeating("Regenerate", regenarationRate, regenarationRate);
    }

    void Regenerate()
    {
        if(curHealth < maxHealth)
            curHealth += regenarateAmount;

        if(curHealth > maxHealth)
            curHealth = maxHealth;

        EventManager.TakeDamage(curHealth/(float)maxHealth);
    }

    public void TakeDamage(int dmg = 10)
    {
        curHealth -= dmg;
        if(curHealth < 0)
            curHealth = 0;
        EventManager.TakeDamage(curHealth/(float)maxHealth);
        if(curHealth < 1)
        {
            EventManager.PlayerDeath();
            GetComponent<Explosion>().BlowUp();
        }
    }

}
