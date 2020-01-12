using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameOverButton : MonoBehaviour {

    /// <summary>
    /// Reload current scene.
    /// </summary>
    /// <returns>void</returns>
    public void RestartScene() {
        SceneManager.LoadScene( SceneManager.GetActiveScene().name );
    }

    /// <summary>
    /// Close game.
    /// </summary>
    /// <returns>void</returns>
    public void ExitGame() {
        Application.Quit();
    }
}
