using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameUI : MonoBehaviour
{
    [SerializeField]GameObject menu;
    [SerializeField]GameObject gameUI;
    [SerializeField]GameObject gameOverUI;

    [SerializeField]GameObject playerPrefab;
    [SerializeField]GameObject playerStartPosition;
    
    void Start()
    {
        ShowMenu();
    }

    void OnEnable()
    {
        EventManager.onStartGame += showGameUI;
        EventManager.onPlayerDeath += ShowMenu;
        EventManager.onGameOver += showGameOverUI;
    }

    void OnDisable()
    {
        EventManager.onStartGame -= showGameUI;
        EventManager.onPlayerDeath -= ShowMenu;
        EventManager.onGameOver -= showGameOverUI;
    }

    void ShowMenu()
    {
        menu.SetActive(true);
        gameUI.SetActive(false);
        Cursor.visible = true;

        Instantiate(playerPrefab, playerStartPosition.transform.position, playerStartPosition.transform.rotation);
    }

    void showGameUI(string mode, string difficulty)
    {
        Cursor.visible = false;
        menu.SetActive(false);
        gameUI.SetActive(true); 
    }

    void showGameOverUI()
    {
        Cursor.visible = true;
        menu.SetActive(false);
        gameUI.SetActive(false);
        gameOverUI.SetActive(true);
    }

}
