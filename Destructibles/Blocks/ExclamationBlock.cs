using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ExclamationBlock : MonoBehaviour {
    public Sprite usedImage;                            // Used image sprite.
    private bool used;                                  // Whether this block has already been used.
    private Animation animation;                        // Animation component reference.
    private SpriteRenderer spriteRenderer;              // Sprite renderer component reference.                       

    // Start is called before the first frame update
    void Start() {
        Init();
    }

    /// <summary>
    /// Get used value.
    /// </summary>
    /// <returns>bool</returns>
    public bool IsUsed() {
        return this.used;
    }

    /// <summary>
    /// Block hit by the player.
    /// </summary>
    /// <returns>IEnumerator</returns>
    private void BlockHit() {
        // TODO: Add instantiate item got here.
        this.used = true;
        Utils.instance.TriggerAnimation( animation, "HitBlock" );
    }

    /// <summary>
    /// Change image sprite.
    /// To be used as an animation
    /// event.
    /// </summary>
    /// <returns>void</returns>
    public void SwitchSprite() {
        spriteRenderer.sprite = usedImage;
    }

    /// <summary>
    /// Sent when an incoming collider makes contact with this object's
    /// collider (2D physics only).
    /// </summary>
    /// <param name="other">The Collision2D data associated with this collision.</param>
    /// <returns>void</returns>
    void OnCollisionEnter2D( Collision2D other ) {
        
        // check if player collides the block from behind.
        if ( ! this.used && other.gameObject.tag == "Player" && other.gameObject.transform.position.x < transform.parent.position.x ) {
            BlockHit();
        }
    }

    /// <summary>
    /// Init class method.
    /// </summary>
    /// <returns>void</returns>
    private void Init() {

        // get animation component reference.
        animation = GetComponent<Animation>();

        // get sprite renderer component reference.
        spriteRenderer = GetComponent<SpriteRenderer>();

        // set default attributes values.
        this.used = false;
    }
}
