using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// This script controls the visual representation of the player's shield status (typically a health bar or energy bar) by resizing a UI element 
// based on the current shield percentage.
public class ShieldUI : MonoBehaviour
{
    [SerializeField] RectTransform barRectTransform; // A reference to the visual UI element (e.g., a health bar), specifically a RectTransform, 
                                                     // which allows dynamic resizing.
    float maxWidth; // stores the maximum width the bar can have (full shield).

    void Awake()
    {
        maxWidth = barRectTransform.rect.width;
    }

    void OnEnable()
    {
        EventManager.onTakeDamage += UpdateShieldDisplay;
    }

    void OnDisable()
    {
        EventManager.onTakeDamage -= UpdateShieldDisplay;
    }

    void UpdateShieldDisplay(float percentage)
    {
        // This method is triggered when the player’s shield is damaged. It receives a percentage value (between 0 and 1) and updates the width 
        // of the shield bar proportionally, keeping its height fixed at 30f.
        barRectTransform.sizeDelta = new Vector2(maxWidth * percentage, 30f);
    }
}
