using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UIManager : MonoBehaviour {

    public static UIManager instance;                       // Public static class instance.
    public HearthSection hearthSection;                     // Hearth section class component reference.
    public TextComponent coinsText;                         // Coins text class component reference.
    public TextComponent lifes;                             // Lifes text class component reference.
    public GameObject GameOverPanel;                        // Game over panel gameObject.
    
    /// <summary>
    /// Awake is called when the script instance is being loaded.
    /// </summary>
    void Awake() {
        if ( instance == null ) {
            instance = this;
        }
    }

    /// <summary>
    /// Display game over
    /// panel on the screen.
    /// </summary>
    /// <returns>void</returns>
    public void DisplayGameOverPanel() {
        GameOverPanel.SetActive( true );
    }
}
