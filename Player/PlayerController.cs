using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour {
    public float moveSpeed;                             // Player's movement speed.
    public float jumpSpeed;                             // Player's jump speed.
    public float groundCheckRadius;                     // Radious used for the circle which checks whether the player is in the fool.
    public LayerMask whatIsGround;                      // Layer mask to check if what we are colliding down is ground.
    public Transform groundCheck;                       // Transfor componet used to check where the ground is.
    public bool isGrounded;                             // Flag to check whether the player is on the air.
    private Rigidbody2D myRigibody;                     // Rigibody2D component reference.
    private Animator myAnim;                          // Animator component reference.
    private bool canMove;                               // Flag to control if the player can jump.
    private bool canJump;                               // Flag to control if the player can jump.

    // Start is called before the first frame update.
    void Start() {
        Init();
    }

    // Update is called once per frame.
    void Update() {

        // check if the player is grounded.
        CheckIfGrounded();

        // listen for movement players control.
        if ( this.canMove ) {
            MovePlayer();
        }

        // listen for player jump control input.
        if ( this.canJump && this.isGrounded ) {
            Jump();
        }

        // update animator variables so the player animation is updated.
        UpdateAnimatorVars();
    }

    /// <summary>
    /// Check if the player is grounded.
    /// </summary>
    /// <returns>void</returns>
    private void CheckIfGrounded() {
        // creates a virtual circle and checks if overlaps with ground layer.
        this.isGrounded = Physics2D.OverlapCircle( groundCheck.position, this.groundCheckRadius, whatIsGround );
    }

    /// <summary>
    /// Listen for player movement
    /// control input.
    /// This method has to be called in Update.
    /// </summary>
    /// <returns>void</returns>
    public void MovePlayer() {

        if ( Input.GetAxisRaw( "Horizontal" ) > 0f ) {

            // move player to the right.
            myRigibody.velocity = new Vector3( moveSpeed, myRigibody.velocity.y, 0f );

            // ensure player sprite faces right.
            transform.localScale = new Vector3( 1f, transform.localScale.y, transform.localScale.z );

        } else if ( Input.GetAxisRaw( "Horizontal" ) < 0f ) {

            // move player to the left.
            myRigibody.velocity = new Vector3( moveSpeed * - 1, myRigibody.velocity.y, 0f );

            // ensure player is facing left.
            transform.localScale = new Vector3( - 1f, transform.localScale.y, transform.localScale.z );

        } else {

            // player is stopped.
            myRigibody.velocity = new Vector3( 0f, myRigibody.velocity.y, 0f );
        }
    }

    /// <summary>
    /// Jump action method.
    /// </summary>
    /// <returns>void</returns>
    private void Jump() {

        // check if space bar or jump button is being pressed.
        if ( Input.GetButtonDown( "Jump" ) ) {
            myRigibody.velocity = new Vector3( myRigibody.velocity.x, jumpSpeed, 0f );
        }
    }

    /// <summary>
    /// Update animator variables
    /// so the different animation states
    /// can be triggered.
    /// </summary>
    /// <returns>void</returns>
    private void UpdateAnimatorVars() {

        // update animator speed variable.
        myAnim.SetFloat( "speed", Mathf.Abs( myRigibody.velocity.x ) );

        // update animator grounded variable.
        myAnim.SetBool( "Grounded", this.isGrounded );
    }

    /// <summary>
    /// Init class method.
    /// </summary>
    /// <returns>void</returns>
    public void Init() {

        // get rigibody 2D component reference.
        myRigibody = GetComponent<Rigidbody2D>();

        // get animator component reference.
        myAnim = GetComponent<Animator>();

        // set attributes default values.
        this.canJump = true;
        this.canMove = true;
    }

}
