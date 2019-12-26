using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour {
    public float moveSpeed;                             // Player's movement speed.
    private Rigidbody2D myRigibody;                     // Rigibody2D component reference.

    // Start is called before the first frame update.
    void Start() {
        Init();
    }

    // Update is called once per frame.
    void Update() {
        
    }

    public void MovePlayer() {

    }

    /// <summary>
    /// Init class method.
    /// </summary>
    /// <returns>void</returns>
    public void Init() {

        // get rigibody 2D component reference.
        myRigibody = GetComponent<Rigidbody2D>();
    }

}
