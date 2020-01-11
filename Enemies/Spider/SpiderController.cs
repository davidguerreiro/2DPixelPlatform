using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpiderController : MonoBehaviour {
    public float moveSpeed;                             // Spider movement speed.
    private bool canMove;                               // Flag to control whether the spider starts moving.
    private Rigidbody2D myRigiBody;                     // Rigibody component reference.

    // Start is called before the first frame update
    void Start() {
        Init();
    }

    // Update is called once per frame
    void Update() {
       if ( this.canMove ) {
           MoveSpeed();
       }
    }

    /// <summary>
    /// Move spider.
    /// </summary>
    /// <returns>void</returns>
    private void MoveSpeed() {
        myRigiBody.velocity = new Vector3( - moveSpeed, myRigiBody.velocity.y, 0f );
    }

    /// <summary>
    /// Init class method.
    /// </summary>
    /// <returns>void</returns>
    private void Init() {
        
        // get rigidoby componente.
        myRigiBody = GetComponent<Rigidbody2D>();
        
        // get attributes default values.
        this.canMove = false;
    }

    /// <summary>
    /// OnBecameVisible is called when the renderer became visible by any camera.
    /// </summary>
    /// <returns>void</returns>
    void OnBecameVisible() {
        
        // allow spider to move in the world.
        this.canMove = true;
    }

    /// <summary>
    /// Sent when another object enters a trigger collider attached to this
    /// object (2D physics only).
    /// </summary>
    /// <param name="other">The other Collider2D involved in this collision.</param>
    void OnTriggerEnter2D( Collider2D other ) {
        
        // check if the spider has fallen off the scene so it has to be destroyed.
        if ( other.tag == "KillPlane" ) {
            this.gameObject.SetActive( false );
        }
    }
}
