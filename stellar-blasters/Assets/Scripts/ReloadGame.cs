using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ReloadGame : MonoBehaviour
{
    // Awake() = Unity lifecycle method that runs before Start() and is called as soon as the script instance is loaded.
    // DontDestroyOnLoad(this.gameObject): This tells Unity not to destroy the attached GameObject when changing scenes.
    void Awake() {
        DontDestroyOnLoad(this.gameObject);
    }

}