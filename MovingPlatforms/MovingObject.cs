using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovingObject : MonoBehaviour {

    public Transform objectToMove;                      // Gameobject to move in the scene.
    public Transform startPoint;                        // Starting movement point.
    public Transform endPoint;                          // Ending movement point.
    public float moveSpeed;                             // Object movement speed.
    public Vector3 currentTarget;                       // To which point the object is moving.
    private bool canMove;                               // Whether the object can move. If false, will be stopped.

    // Start is called before the first frame update
    void Start() {
        Init();    
    }

    // Update is called once per frame
    void Update() {
        if ( this.canMove ) {
            MoveObject();
        }
    }
    

    /// <summary>
    /// Stop moving object.
    /// </summary>
    /// <returns>void</returns>
    public void StopMovingObject() {
        this.canMove = false;
    }

    /// <summary>
    /// Move object.
    /// This methdo has to be called
    /// in the Updated method.
    /// </summary>
    /// <returns>void</returns>
    private void MoveObject() {
        objectToMove.transform.position = Vector3.MoveTowards( objectToMove.transform.position, currentTarget, moveSpeed * Time.deltaTime );


        // set endpoint when the platform reachs the end point.
        if ( objectToMove.transform.position == endPoint.transform.position ) {
            currentTarget = startPoint.position;
        }

        // set endpoing when the platform reachs the start point.
        if ( objectToMove.transform.position == startPoint.position ) {
            currentTarget = endPoint.position;
        }
    }

    /// <summary>
    /// Init class method.
    /// </summary>
    /// <returns>void</returns>
    private void Init() {

        // set attributes default values.
        this.canMove = true;

        // set current target so the object can start moving.
        this.currentTarget = endPoint.position;
    }
}
