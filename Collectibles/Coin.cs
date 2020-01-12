using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Coin : MonoBehaviour {

    public int coinValue;                           // Coin value - different kinds of coins might gran higer points.
    public float rotateSpeed;                       // Coin rotate speed.

    /// <summary>
    /// This function is called every fixed framerate frame, if the MonoBehaviour is enabled.
    /// </summary>
    void FixedUpdate() {
        RotateCoin();
    }
    
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

    /// <summary>
    /// Rotate coin.
    /// This method must be called
    /// at FixedUpdate.
    /// </summary>
    /// <returns>void</returns>
    private void RotateCoin() {
        transform.Rotate( 0f, this.rotateSpeed * Time.deltaTime, 0f );
    }
}
