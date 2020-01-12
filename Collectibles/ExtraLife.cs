using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ExtraLife : MonoBehaviour {
    public int lifesToGive;                                 // How many lives this item grants to the player.

    /// <summary>
    /// Sent when another object enters a trigger collider attached to this
    /// object (2D physics only).
    /// </summary>
    /// <param name="other">The other Collider2D involved in this collision.</param>
    void OnTriggerEnter2D(Collider2D other) {

        // add the life to the player.
        if ( other.tag == "Player" ) {
            PlayerController.instance.UpdateLifes( this.lifesToGive );
            Destroy( this.gameObject );
        }
    }
}
