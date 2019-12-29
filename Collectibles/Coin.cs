using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Coin : MonoBehaviour {

    public int coinValue;                           // Coin value - different kinds of coins might gran higer points.
    
    /// <summary>
    /// Sent when another object enters a trigger collider attached to this
    /// object (2D physics only).
    /// </summary>
    /// <param name="other">The other Collider2D involved in this collision.</param>
    void OnTriggerEnter2D( Collider2D other ) {
        
        // collect coin.
        if ( other.tag == "Player" && other.GetComponent<PlayerController>().IsPlayerActive() ) {
            
            // add coin to the count.
            LevelManager.instance.AddCoins( this.coinValue );
            Destroy( this.gameObject );
        }
    }
}
