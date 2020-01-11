using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ResetOnRespaw : MonoBehaviour {

    private Vector3 startPosition;                                          // Respawn item start position.
    private Quaternion startRotation;                                       // Respwan item start rotation
    private Vector3 startScale;                                             // Respawn item start scale.
    private Rigidbody2D myRigibody;                                         // Rigibody component reference.

    // Start is called before the first frame update.
    void Start() {
        Init();
    }

    /// <summary>
    /// Init class method.
    /// </summary>
    /// <returns>void</returns>
    private void Init() {

        // get transform initial values.
        this.startPosition = transform.position;
        this.startRotation = transform.rotation;
        this.startScale = transform.localScale;

        // get rigibody component reference.
        if ( GetComponent<Rigidbody2D>() != null ) {
            myRigibody = GetComponent<Rigidbody2D>();
        }
    }

    /// <summary>
    /// Respawn object in
    /// the game scene.
    /// </summary>
    /// <returns>void</returns>
    public void Respawn() {

        // reset transform.
        transform.position = this.startPosition;
        transform.rotation = this.startRotation;
        transform.localScale = this.startScale;

        // reset rigibody state if attached.
        if ( myRigibody != null ) {
            myRigibody.velocity = Vector3.zero;
        }
    }
}
