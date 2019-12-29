﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelManager : MonoBehaviour {
    public static LevelManager instance;                // Public static instance of this class.
    public float waitToRespawn;                         // Time to wait before the player respawn.
    public PlayerController thePlayer;                  // Player controller class component reference.

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

        // remove player, display death particles and disable controls.
        thePlayer.SetPlayerDefeated( true );
        yield return new WaitForSeconds( toWait );

        // respawn player, display player and give controls back to the player.
        thePlayer.gameObject.transform.position = thePlayer.GetRespawnPosition();
        thePlayer.SetPlayerActive();
    }

    /// <summary>
    /// Init class method.
    /// </summary>
    /// <returns>void</returns>
    public void Init() {
        // thePlayer = FindObjectOfType<PlayerController>();
    }
}