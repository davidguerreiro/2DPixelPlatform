using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Utils : MonoBehaviour {
    
    public static Utils instance;                   // Utils class static instance.

    /// <summary>
    /// Awake is called when the script instance is being loaded.
    /// </summary>
    void Awake() {
        if ( instance == null ) {
            instance = this;
        }
    }

    /// <summary>
    /// Destroy item over time.
    /// </summary>
    /// <param name="object">GameObject - game object to destroy.</param>
    /// <param name="secondsToWait">float - how long the game waits till destroy the object.</param>
    /// <returns>void</returns>
    public void DestroyOverTime( GameObject theObject, float secondsToWait ) {
        Destroy( theObject, secondsToWait );
    }
}
