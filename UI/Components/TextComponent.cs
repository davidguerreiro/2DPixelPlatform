using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TextComponent : MonoBehaviour {
    private Text content;                               // Text component reference.

    // Start is called before the first frame update
    void Start() {
        Init();
    }

    /// <summary>
    /// Get content.
    /// </summary>
    /// <returns>void</returns>
    public string GetContent() {
        return content.text;
    }

    /// <summary>
    /// Update text content.
    /// </summary>
    /// <param name="newContent">string - new content displayed by the text component</param>
    /// <returns>void</returns>
    public void UpdateContent( string newContent ) {
        content.text = newContent;
    }

    /// <summary>
    /// Init class method.
    /// </summary>
    /// <returns>void</returns>
    public void Init() {

        // get text component reference.
        content = GetComponent<Text>();
    }
}
