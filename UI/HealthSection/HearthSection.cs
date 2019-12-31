﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HearthSection : MonoBehaviour {
    private Hearth[] hearths;                           // Player current amount of hearths.
    private float halfHearthValue;                      // Half hearth value. When player health matches this value, a half hearth sprite will be used.

    // Start is called before the first frame update
    void Start() {
        Init();
    }

    /// <summary>
    /// Refresh hearts.
    /// This function must be called every
    /// the player is hurt or if recovers
    /// health.
    /// </summary>
    /// <returns>void</returns>
    public void UpdateHealth() {
        int maxHealth = PlayerController.instance.GetMaxHealth();
        float currentHealth = PlayerController.instance.GetHealth();

        for ( int i = 0; i < maxHealth; i++ ) {
            
            // full hearth 
            if ( i < currentHealth && i + this.halfHearthValue != currentHealth ) {
                hearths[ i ].UpdateValue( 1f );
            } else if ( i + this.halfHearthValue == currentHealth ) {
                // half hearth.
                hearths[ i ].UpdateValue( 0.5f );
            } else {
                // empty hearth.
                hearths[ i ].UpdateValue( 0f );
            }
        }
    }

    /// <summary>
    /// Init class method.
    /// </summary>
    /// <returns>void</returns>
    private void Init() {

        // get children hearts.
        hearths = GetComponentsInChildren<Hearth>();

        // get attributes default vaulues.
        this.halfHearthValue = 0.5f;
    }
}
