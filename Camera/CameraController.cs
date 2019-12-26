using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour {
    public GameObject target;                               // Which gameObject the camera focus to.
    public float followAhead;                               // Offset so the player is not in the middle of the screen.
    public float smoothing;                                 // How fast the camera moves when the player changes direction.
    private Vector3 targetPosition;                         // Temporal vector for target object.


    // Update is called once per frame
    void Update() {
        FollowTarget();
    }

    /// <summary>
    /// This function is called every fixed framerate frame, if the MonoBehaviour is enabled.
    /// </summary>
    void FixedUpdate() {
        // FollowTarget();
    }

    /// <summary>
    /// Folow gameObject position.
    /// This method has to be called
    /// in the Update method.
    /// </summary>
    /// <returns>void</returns>
    private void FollowTarget() {
        targetPosition = new Vector3( target.transform.position.x, transform.position.y, transform.position.z );

        // check player facing direction.
        if ( target.transform.localScale.x > 0f ) {
            targetPosition = new Vector3( targetPosition.x + followAhead, targetPosition.y, targetPosition.z );
        } else {
            targetPosition = new Vector3( targetPosition.x - followAhead, targetPosition.y, targetPosition.z );
        }

        // transform.position = targetPosition;
        transform.position = Vector3.Lerp( transform.position, targetPosition, smoothing * Time.deltaTime );
    }
}
