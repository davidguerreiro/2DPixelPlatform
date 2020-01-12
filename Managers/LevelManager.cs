using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelManager : MonoBehaviour {
    public static LevelManager instance;                // Public static instance of this class.
    public float waitToRespawn;                         // Time to wait before the player respawn.
    public PlayerController thePlayer;                  // Player controller class component reference.
    public int coinsCount;                              // Total number of coins collected.
    public int bonusLifeThreshold;                      // Threshold to grant a player lifes for collecting coins.
    private ResetOnRespaw[] objectsToReset;             // Objects which are respawn every time the player dies.

    /// <summary>
    /// Awake is called when the script instance is being loaded.
    /// </summary>
    void Awake() {
        if ( instance == null ) {
            instance = this;
        } 
    }

    // Start is called before the first frame update
    void Start() {
        Init();
    }

    // Update is called once per frame
    void Update() {
        
    }

    /// <summary>
    /// Respawn player in the last
    /// respawn position saved.
    /// </summary>
    /// <returns>IEnumerator</returns>
    public IEnumerator Respawn() {
        float toWait = .8f;

        // remove all player coins.
        this.coinsCount = 0;
        AddCoins( this.coinsCount );

        // remove player, display death particles and disable controls.
        thePlayer.SetPlayerDefeated( true );

        // udpate player current lifes.
        thePlayer.UpdateLifes( -1 );
        yield return new WaitForSeconds( toWait );

        // respaw only if the player has enough lifes, if not display gameOver.
        if ( thePlayer.GetLifes() > 0 ) {

            // respawn defeated enemies.
            foreach ( ResetOnRespaw item in objectsToReset ) {
                item.gameObject.SetActive( true );
                item.Respawn();
            }

            // respawn player, display player and give controls back to the player.
            thePlayer.gameObject.transform.position = thePlayer.GetRespawnPosition();
            thePlayer.SetPlayerActive();
        } else {

            // play game over music.
            LevelMusicManager.instance.PlaySong( "gameOver" );

            // display game over panel.
            UIManager.instance.DisplayGameOverPanel();
        }

    }

    /// <summary>
    /// Add coins to our
    /// internal coins count.
    /// </summary>
    /// <param name="coinsToAdd">int - coints to be added</param>
    /// <returns>void</returns>
    public void AddCoins( int coinsToAdd ) {
        this.coinsCount += coinsToAdd;

        // check for bonus.
        if ( this.coinsCount >= this.bonusLifeThreshold ) {
            PlayerController.instance.UpdateLifes( 1 );

            // increase the bonus to avoid getting extra lifes at same threshold per level ( or after respawn )
            this.bonusLifeThreshold += this.bonusLifeThreshold;
        }
        
        // update coins in the UI.
        UIManager.instance.coinsText.UpdateContent( this.coinsCount.ToString() );
    }

    /// <summary>
    /// Init class method.
    /// </summary>
    /// <returns>void</returns>
    public void Init() {
        
        // set default values for attributes.
        this.coinsCount = 0;

        // get all respawn objects in the current scene.
        objectsToReset = FindObjectsOfType<ResetOnRespaw>();
    }
}
