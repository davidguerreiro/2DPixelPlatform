using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HurtPlayer : MonoBehaviour {
    public float damage;                                    // Damage caused to the player.

    /// <summary>
    /// Sent when another object enters a trigger collider attached to this
    /// object (2D physics only).
    /// </summary>
    /// <param name="other">The other Collider2D involved in this collision.</param>
    void OnTriggerEnter2D( Collider2D other ) {
        
        if ( other.tag == "Player" && other.GetComponent<PlayerController>().IsPlayerActive() ) {

            // add damage to player if not invincible.
            if ( ! PlayerController.instance.invincible ) {
                PlayerController.instance.GetDamage( this.damage );
            }
        }
    }
}
