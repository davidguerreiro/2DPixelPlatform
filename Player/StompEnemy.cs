using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StompEnemy : MonoBehaviour {
    public float bounceForce;                                               // Player bounce force used after the enemy is destroyed.
    private GameObject deathSplosion;                                       // Particle effect displayed when an enemy is destroyed.
    private Rigidbody2D playerRigibody;                                     // Player's rigibody component reference.

    /// <summary>
    /// Start is called on the frame when a script is enabled just before
    /// any of the Update methods is called the first time.
    /// </summary>
    void Start() {
        Init();
    }

    /// <summary>
    /// Sent when another object enters a trigger collider attached to this
    /// object (2D physics only).
    /// </summary>
    /// <param name="other">The other Collider2D involved in this collision.</param>
    void OnTriggerEnter2D( Collider2D other ) {
        
        // check if the object collided is an enemy.
        if ( other.tag == "Enemy" ) {

            // check that we are jumping above the enemy.
            if ( transform.position.y > other.transform.position.y ) {
                
                this.deathSplosion = other.GetComponent<Enemy>().GetDefeatedParticles();

                // bounce player.
                playerRigibody.velocity = new Vector2( playerRigibody.velocity.x, bounceForce );

                // display particles and destroy enemy.
                Instantiate( deathSplosion, other.transform.position, other.transform.rotation );
                Destroy( other.gameObject );
            }
        }
    }

    /// <summary>
    /// Init class method.
    /// </summary>
    /// <returns>void</returns>
    private void Init() {

        // get player's rigibody component.
        playerRigibody = transform.parent.GetComponent<Rigidbody2D>();
    }
}
