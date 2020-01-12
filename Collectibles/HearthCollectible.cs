using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HearthCollectible : MonoBehaviour {

    public float healthToGive;                                // Health granted to the player the this collectible is picked up.

    /// <summary>
    /// Sent when another object enters a trigger collider attached to this
    /// object (2D physics only).
    /// </summary>
    /// <param name="other">The other Collider2D involved in this collision.</param>
    void OnTriggerEnter2D(Collider2D other) {
        
        // add lives to the player.
        if ( other.tag == "Player" ) {
            PlayerController.instance.RecoverHealth( healthToGive );
            this.gameObject.SetActive( false );
        }
    }
}
