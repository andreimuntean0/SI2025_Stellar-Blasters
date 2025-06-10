using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

// This script handles transitions between different UI states of the game — the main menu, the in-game interface, and the game over screen. 
// It also instantiates the player when starting the game.
public class GameUI : MonoBehaviour
{
    // References to UI panels: the main menu, the in-game UI, and the game over screen.
    [SerializeField] GameObject menu;
    [SerializeField] GameObject gameUI;
    [SerializeField] GameObject gameOverUI;

    // The player object to spawn and the position where it should be instantiated when the game starts.
    [SerializeField] GameObject playerPrefab;
    [SerializeField] GameObject playerStartPosition;

    void Start()
    {
        ShowMenu();
    }

    // Subscribes and unsubscribes from game state events via the EventManager. This ensures UI updates based on player actions and game flow.
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
        // Activates the menu UI, hides the in-game UI, shows the cursor. Instantiates the player at the specified start position.
        menu.SetActive(true);
        gameUI.SetActive(false);
        Cursor.visible = true;

        Instantiate(playerPrefab, playerStartPosition.transform.position, playerStartPosition.transform.rotation);
    }

    void showGameUI(string mode, string difficulty)
    {
        // Hides the cursor, hides the menu, and shows the in-game UI. Triggered by onStartGame.
        Cursor.visible = false;
        menu.SetActive(false);
        gameUI.SetActive(true);
    }

    void showGameOverUI()
    {
        // Called when the game ends (onGameOver). Hides the in-game UI, shows the game over screen, and enables the cursor for user interaction.
        Cursor.visible = true;
        menu.SetActive(false);
        gameUI.SetActive(false);
        gameOverUI.SetActive(true);
    }

}
