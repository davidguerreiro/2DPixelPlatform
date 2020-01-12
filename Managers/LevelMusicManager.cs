using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelMusicManager : MonoBehaviour {
    public static LevelMusicManager instance;          // Public static class instance.
    public AudioClip[] clips;                          // Level music clips.
    private AudioSource audio;                         // Audio component reference.

    /// <summary>
    /// Awake is called when the script instance is being loaded.
    /// </summary>
    void Awake() {
        if ( instance == null ) {
            instance = this;
        }
    }

    // Start is called before the first frame update
    void Start() {
        Init();
    }

    /// <summary>
    /// Play song in the level.
    /// </summary>
    /// <param name="songName">string - song name.</param.
    /// <returns>void</returns>
    public void PlaySong( string songName ) {
        if ( audio.isPlaying ) {
            audio.Stop();
        }

        switch ( songName ) {
            case "currentLevel":
                audio.clip = clips[0];
                audio.loop = true;
                break;
            case "gameOver":
                audio.clip = clips[1];
                audio.loop = false;
                break;
        }

        audio.Play();
    }

    /// <summary>
    /// Init class method.
    /// </summary>
    /// <returns>void</returns>
    private void Init() {

        // get audio component reference.
        audio = GetComponent<AudioSource>();
    }
}
