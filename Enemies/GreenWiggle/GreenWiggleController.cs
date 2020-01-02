using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GreenWiggleController : MonoBehaviour {

    public Transform leftPoint;                             // Max left position the enemy can move to.
    public Transform rightPoint;                            // Max right position the enemy can move to.
    public float moveSpeed;                                 // Enemy movement speed.
    private Rigidbody2D myRigibody;                         // Rigibody 2D component reference.
    private bool movingRight;                               // Enemy movement direction.

    // Start is called before the first frame update
    void Start() {
        Init();
    }

    // Update is called once per frame
    void Update() {
        MoveEnemy();
    }

    /// <summary>
    /// Check moving enemy direction.
    /// </summary>
    /// <returns>void</returns>
    public void MoveEnemy() {

        // check / set direction.
        if ( this.movingRight && transform.position.x > rightPoint.position.x ) {
            this.movingRight = false;
        }

        if ( ! this.movingRight && transform.position.x < leftPoint.position.x ) {
            this.movingRight = true;
        }

        // move enemy.
        if ( this.movingRight ) {
            myRigibody.velocity = new Vector3( moveSpeed, myRigibody.velocity.y, 0f );
        } else {
            myRigibody.velocity = new Vector3( - moveSpeed, myRigibody.velocity.y, 0f );
        }
    }

    /// <summary>
    /// Init class method.
    /// </summary>
    /// <returns>void</returns>
    private void Init() {

        // get rigibody 2d component reference.
        myRigibody = GetComponent<Rigidbody2D>();

        // set default values for attributes.
        this.movingRight = false;
    }
}
