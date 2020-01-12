using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerSoundManager : MonoBehaviour {
    public AudioClip[] clips;                               // AudioClips to display sounds from the player.
    private AudioSource audio;                              // Audio component reference.

    // Start is called before the first frame update
    void Start() {
        Init();
    }

    /// <summary>
    /// Init class method.
    /// </summary>
    /// <returns>void</returns>
    private void Init() {

        // get audio component reference.
        audio = GetComponent<AudioSource>();
    }

    /// <summary>
    /// Play sound.
    /// </summary>
    /// <param name="action">string - action to define which sounds is played.
    /// <returns>void</returns>
    public void PlaySound( string action ) {
        if ( audio.isPlaying ) {
            audio.Stop();
        }

        switch ( action ) {
            case "jump":
                audio.clip = clips[0];
                break;
            case "hurt":
                audio.clip = clips[1];
                break;
            case "die":
                audio.clip = clips[2];
                break;
        }

        audio.Play();
    }
}
