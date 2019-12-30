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

    /// <summary>
    /// Trigger an animation.
    /// </summary>
    /// <param name="animation">Animation - animation component reference.</param>
    /// <param name="clipName">string - clip name to be played</param>
    /// <param name="loop">bool - wheter to keep playing the animation in a loop</param>
    /// <returns>void</returns>
    public void TriggerAnimation( Animation animation, string clipName, bool loop = false ) {
        if ( animation.isPlaying ) {
            animation.Stop();
        }

        // set animation to play in a loop if required.
        if ( loop ) {
            animation.wrapMode = WrapMode.Loop;
        } else {
            animation.wrapMode = WrapMode.Once;
        }

        // get clip from animation array of clips.
        animation.clip = animation.GetClip( clipName );

        animation.Play();
    }
    
}
