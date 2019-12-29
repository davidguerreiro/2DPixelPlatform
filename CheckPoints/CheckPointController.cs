using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CheckPointController : MonoBehaviour {
    public Sprite flagClosed;                           // 2D sprite for flag when is closed.
    public Sprite flagOpened;                           // 2D sprite for flag when is opened.
    private SpriteRenderer theSpriteRenderer;           // Sprite renderer component reference.
    private bool enabled;                               // Check if the checkpoint has already been enabled by the player.

    // Start is called before the first frame update
    void Start() {
        Init();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    /// <summary>
    /// Sent when another object enters a trigger collider attached to this
    /// object (2D physics only).
    /// </summary>
    /// <param name="other">The other Collider2D involved in this collision.</param>
    /// <returns>void</returns>
    void OnTriggerEnter2D( Collider2D other ) {
        
        // check if the player is entering the checkpoint.
        if ( other.tag == "Player" && ! this.enabled ) {

            // display opened flag.
            theSpriteRenderer.sprite = flagOpened;
            this.enabled = true;

            // update player respawn position.
            other.GetComponent<PlayerController>().UpdateRespawnPosition( transform.position );

        }
    }

    /// <summary>
    /// Init class method.
    /// </summary>
    /// <returns>void</reuturns>
    private void Init() {

        // get renderer component reference.
        theSpriteRenderer = GetComponent<SpriteRenderer>();

        // set attributes defaul values.
        this.enabled = false;
    }
}
