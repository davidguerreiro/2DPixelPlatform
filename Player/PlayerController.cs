using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour {

    public static PlayerController instance;              // Public static instance reference.
    public float moveSpeed;                             // Player's movement speed.
    public float jumpSpeed;                             // Player's jump speed.
    public float groundCheckRadius;                     // Radious used for the circle which checks whether the player is in the fool.
    public LayerMask whatIsGround;                      // Layer mask to check if what we are colliding down is ground.
    public Transform groundCheck;                       // Transfor componet used to check where the ground is.
    public GameObject stompEnemy;                       // GameObject used to check whether we are jumping over an enemy and causing damage to him.
    public bool isGrounded;                             // Flag to check whether the player is on the air.
    public Vector3 respawnPosition;                     // Player respawn position.
    public GameObject playerDeathParticles;             // Player death partices.
    public float knockBackForce;                        // Force applied to the player when hit by enemy or hazard.
    public float knockBackLenght;                       // For how long the player is in knocked state.
    public bool invincible;                             // Whether the player is invincible or not. If invincible, it cannot be hurt by enemies or hazards.
    public float invincibleLength;                      // For how long ghe player is in invincible status.
    private float invincibleCounter;                    // Internal counter for invincible status.
    private float knockBackCounter;                     // Internal counter for knock status.
    private Rigidbody2D myRigibody;                     // Rigibody2D component reference.
    private Animator myAnim;                            // Animator component reference.
    private bool playerActive;                          // Flag to control whether the player is active in the game. Active means is playable in the scene and collisions / other gameplay stuff affects him.
    private bool canMove;                               // Flag to control if the player can jump.
    private bool canJump;                               // Flag to control if the player can jump.
    private SpriteRenderer spriteRenderer;              // Player's sprite renderer component reference.
    [SerializeField]
    private float health;                               // Current player health.
    private int maxHealth;                              // Player's max health.
    [SerializeField]
    private int lifes;                                  // Current player's lifes.
    private bool running;                               // Flag to control whether the player is running.


    /// <summary>
    /// Awake is called when the script instance is being loaded.
    /// </summary>
    void Awake() {
        if ( instance == null ) {
            instance = this;
        }
    }

    // Start is called before the first frame update.
    void Start() {
        Init();
    }

    // Update is called once per frame.
    void Update() {

        // check if the player is grounded.
        CheckIfGrounded();

        // check knock status.
        KnockBackControl();

        // check invincible control.
        InvencibleControl();

        // listen for movement players control.
        if ( this.canMove ) {
            MovePlayer();
        }

        // listen for player jump control input.
        if ( this.canJump && this.isGrounded ) {
            Jump();
        }

        // liste for player running control input.
        if ( this.canMove && this.isGrounded ) {
            Run();
        }

        // update animator variables so the player animation is updated.
        UpdateAnimatorVars();
    }

    /// <summary>
    /// This function is called every fixed framerate frame, if the MonoBehaviour is enabled.
    /// </summary>
    /// <returns>void</returns>
    void FixedUpdate() {
        
        // check if we are falling so enemys can be hit by the played.
        CheckIfAttacking();
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
    /// Get player running
    /// state.
    /// </summary>
    /// <returns>bool</returns>
    public bool IsRunning() {
        return this.running;
    }

    /// <summary>
    /// Get player health.
    /// </summary>
    /// <returns>float</returns>
    public float GetHealth() {
        return this.health;
    }

    /// <summary>
    /// Get max health.
    /// </summary>
    /// <returns>void</returns>
    public int GetMaxHealth() {
        return this.maxHealth;
    }

    /// <summary>
    /// Get damage.
    /// </summary>
    /// <param name="damage">float - amount of damage received</param>
    /// <returns>void</returns>
    public void GetDamage( float damage ) {

        this.health -= damage;

        // update health in the UI.
        UIManager.instance.hearthSection.UpdateHealth();

        // knock player.
        KnockBack();

        if ( this.health <= 0 ) {

            // TODO: Call life lost here.
            health = 0;
            StartCoroutine( LevelManager.instance.Respawn() );
        }
    }

    /// <summary>
    /// Get current lifes.
    /// </summary>
    /// <returns>int</returns>
    public int GetLifes() {
        return this.lifes;
    }

    /// <summary>
    /// Update lifes.
    /// </summary>
    /// <param name="toAdd">int - how many lifes to add or remove</param>
    /// <returns>void</returns>
    public void UpdateLifes( int toAdd ) {
        this.lifes += toAdd;
        
        // ensure max lifes and min lifes is never outreached.
        if ( this.lifes > 99 ) {
            this.lifes = 99;
        }

        if ( this.lifes < 0 ) {
            this.lifes = 0;
        }

        // update UI.
        UIManager.instance.lifes.UpdateContent( this.lifes.ToString() );
    }

    /// <summary>
    /// Recover health.
    /// </summary>
    /// <param name="recovered">float - how much health is recovered</param>
    /// <returns>void</returns>
    public void RecoverHealth( float recovered ) {
        this.health += recovered;

        if ( this.health > this.maxHealth ) {
            this.health = (float) this.maxHealth;
        }
    }


    /// <summary>
    /// Get player active
    /// status.
    /// </summary>
    /// <returns>bool</returns>
    public bool IsPlayerActive() {
        return this.playerActive;
    }

    /// <summary>
    /// Set up new player status.
    /// </summary>
    /// <param name="newPlayerStatus">bool - new player status</param>
    /// <returns>void</returns>
    public void SetPlayerStatus( bool newPlayerStatus ) {
        this.playerActive = newPlayerStatus;
    }

    /// <summary>
    /// Get respawn position.
    /// </summary>
    /// <returns>Vector3</returns>
    public Vector3 GetRespawnPosition() {
        return this.respawnPosition;
    }

    /// <summary>
    /// Listen for player movement
    /// control input.
    /// This method has to be called in Update.
    /// </summary>
    /// <returns>void</returns>
    public void MovePlayer() {

        // alter moveSpeed if running.
        float playerSpeed = ( this.running ) ? this.moveSpeed * 2f : this.moveSpeed;


        if ( Input.GetAxisRaw( "Horizontal" ) > 0f ) {

            // move player to the right.
            myRigibody.velocity = new Vector3( playerSpeed, myRigibody.velocity.y, 0f );

            // ensure player sprite faces right.
            transform.localScale = new Vector3( 1f, transform.localScale.y, transform.localScale.z );

        } else if ( Input.GetAxisRaw( "Horizontal" ) < 0f ) {

            // move player to the left.
            myRigibody.velocity = new Vector3( playerSpeed * - 1, myRigibody.velocity.y, 0f );

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

        // check if space bar or jump button is pressed.
        if ( Input.GetButtonDown( "Jump" ) ) {
            myRigibody.velocity = new Vector3( myRigibody.velocity.x, jumpSpeed, 0f );
        }
    }

    /// <summary>
    /// Run action method.
    /// </summary>
    /// <returns>void</returns>
    public void Run() {

        // check if left shift button is being pressed.
        if ( Input.GetButton( "Run" ) ) {
            this.running = true;
        } else {
            this.running = false;
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
    /// Sent when another object enters a trigger collider attached to this
    /// object (2D physics only).
    /// </summary>
    /// <param name="other">The other Collider2D involved in this collision.</param>
    /// <returns>void</returns>
    void OnTriggerEnter2D( Collider2D other ) {

        // all trigger collisions to be checked only if the player is active.
        if ( this.playerActive ) {
        
            // check if the player is entering the kill plane.
            if ( other.tag == "KillPlane" ) {
                
                // set player as current respawn position.
                StartCoroutine( LevelManager.instance.Respawn() );
            }
        }
    }

    /// <summary>
    /// Sent when an incoming collider makes contact with this object's
    /// collider (2D physics only).
    /// </summary>
    /// <param name="other">The Collision2D data associated with this collision.</param>
    void OnCollisionEnter2D( Collision2D other ) {

        // all collisions to be checked only if the player is active.
        if ( this.playerActive ) {

            // check if the player is jumping into a moving platform.
            if ( other.gameObject.tag == "MovingPlatform" && this.isGrounded ) {
                transform.parent = other.transform;
            }
        }
    }

    /// <summary>
    /// Sent when a collider on another object stops touching this
    /// object's collider (2D physics only).
    /// </summary>
    /// <param name="other">The Collision2D data associated with this collision.</param>
    void OnCollisionExit2D( Collision2D other ) {
        
        // check if the player is leaving a moving platform.
        if ( other.gameObject.tag == "MovingPlatform" ) {
            transform.parent = null;
        }
    }

    /// <summary>
    /// Update respawn point.
    /// </summary>
    /// <param name="newPosition">Vector3 - new respawn position</param>
    /// <returns>void</returns>
    public void UpdateRespawnPosition( Vector3 newPosition ) {
        this.respawnPosition = newPosition;
    }

    /// <summary>
    /// Block player input movements.
    /// Use this method to remove player
    /// control for the main character.
    /// </summary>
    /// <returns>void</returns>
    public void LockPlayerInput() {
        this.canMove = false;
        this.canJump = false;
    }

    /// <summary>
    /// Unlock player input movements.
    /// Use this method to grant the player
    /// the ability to control the main character.
    /// </summary>
    /// <returns>void</returns>
    public void UnlockPlayerInput() {
        this.canJump = true;
        this.canMove = true;
    }

    /// <summary>
    /// Set player as defeated.
    /// Sprite disappear and movement is
    /// blocked.
    /// </summary>
    /// <param name="displayDestroyedParticles">bool - optional - wheter to display player defeated particles</param>
    /// <returns>void</returns>
    public void SetPlayerDefeated( bool displayDestroyedParticles = false ) {
        this.playerActive = false;

        // stop velocity and gravity.
        myRigibody.velocity = new Vector3( 0f, 0f, 0f );
        myRigibody.gravityScale = 0;
        spriteRenderer.color = new Color( spriteRenderer.color.r, spriteRenderer.color.g, spriteRenderer.color.b, 0f );

        if ( displayDestroyedParticles ) {
            GameObject particleInstance = Instantiate( playerDeathParticles, transform.position, transform.rotation );
            
            // destroy particles after the animation is completed.
            Utils.instance.DestroyOverTime( particleInstance, 1.1f );
        }

        // restore transform parent - just in case the player is defeated in a platform or something similar.
        transform.parent = null;

        // lock player controls.
        LockPlayerInput();
    }

    /// <summary>
    /// Set player active in the scene and ready
    /// to get player input.
    /// </summary>
    /// <returns>void</returns>
    public void SetPlayerActive() {
        this.playerActive = true;
        
        // reset gravity.
        myRigibody.gravityScale = 2.5f;
        spriteRenderer.color = new Color( spriteRenderer.color.r, spriteRenderer.color.g, spriteRenderer.color.b, 1f );

        // restore player health.
        this.health = this.maxHealth;
        UIManager.instance.hearthSection.UpdateHealth();

        // unlock player controls.
        UnlockPlayerInput();
    }

    /// <summary>
    /// Check if we are attacking
    /// and enemy. This only happens
    /// when we are failing as the 
    /// only way to destroy enemies is
    /// jumpling over them.
    /// This method must be called in
    /// FixedUpdate.
    /// </summary>
    /// <returns>void</returns>
    private void CheckIfAttacking() {

        // enable destroy enemy collider if falling.
        if ( myRigibody.velocity.y < 0 ) {
            stompEnemy.SetActive( true );
        } else {
            stompEnemy.SetActive( false );
        }
    }

    /// <summary>
    /// Knock player when hit by
    /// an enemy or hazard.
    /// </summary>
    /// <returns>void</returns>
    public void KnockBack() {
        this.knockBackCounter = this.knockBackLenght;
        
        // set player as invencible only if it is still alive.
        if ( this.health > 0 ) {
            Invencible();
        }
    }

    /// <summary>
    /// Set player as invincibe.
    /// Usually this status is granted
    /// to player after being hurt by an
    /// enemy or hazards.
    /// </summary>
    /// <returns>void</returns>
    public void Invencible() {
        this.invincibleCounter = this.invincibleLength;
        this.invincible = true; 
    }

    /// <summary>
    /// Invencible status
    /// control. This method
    /// must be called in 
    /// the Update loop.
    /// </summary>
    /// <returns>void</returns>
    private void InvencibleControl() {
        if ( invincibleCounter <= 0 ) {

            if ( this.invincible ) {
                AlterTransparency( 1f );
            }

            this.invincible = false;
        } else {
            AlterTransparency( 0.4f );
            invincibleCounter -= Time.deltaTime;
        }
    }

    /// <summary>
    /// Control whether the player
    /// is in knock state.
    /// This method must be called
    /// in Update.
    /// </summary>
    /// <returns>void</returns>
    private void KnockBackControl() {

        if ( knockBackCounter <= 0 ) {
            this.canMove = true;
        } else {
            this.canMove = false;
            knockBackCounter -= Time.deltaTime;

            // check player direction before applying the knock force.
            if ( transform.localScale.x > 0 ) {
                myRigibody.velocity = new Vector3( - knockBackForce, knockBackForce, 0f );
            } else {
                myRigibody.velocity = new Vector3( knockBackForce, knockBackForce, 0f );
            }
        }
    }

    /// <summary>
    /// Modify player transparency sprite
    /// Usually this is used when the player
    /// is knocked by an enemy.
    /// </summary>
    /// <param name="value">float - new value after knocked</param>
    /// <returns>void</returns>
    public void AlterTransparency( float value ) {
        spriteRenderer.color = new Color( spriteRenderer.color.r, spriteRenderer.color.g, spriteRenderer.color.b, value );
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

        // get sprite renderer component.
        spriteRenderer = GetComponent<SpriteRenderer>();

        // set attributes default values.
        this.playerActive = true;
        this.canJump = true;
        this.canMove = true;
        this.running = false;

        // set defalt maxHealth.
        this.maxHealth = 3;

        // set default value for respawn position.
        this.respawnPosition = transform.position;
    }

}
