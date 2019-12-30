using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Hearth : MonoBehaviour {

    [SerializeField]
    private int id;                                      // Heath id.
    public Sprite[] hearthSprites = new Sprite[3];      // Possible hearth sprites.

    [SerializeField]
    private float value = 1f;                           // Heath value. It can be 1, 0.5 or 0 if empty.
    private SpriteRenderer spriteRenderer;              // Sprite Renderer component reference.

    // Start is called before the first frame update
    void Start() {
        Init();
    }

    /// <summary>
    /// Get heath value.
    /// </summary>
    /// <retunrs>float</returns>
    public float GetValue() {
        return this.value;
    }

    /// <summary>
    /// Get id value.
    /// </summary>
    /// <returns>int</returns>
    public int GetId() {
        return this.id;
    }

    /// <summary>
    /// Update hearth value and update
    /// hearth sprite.
    /// </summary>
    /// <param name="newValue">float - new value</param>
    /// <returns>float</returns>
    public void UpdateValue( float newValue ) {
        this.value = newValue;

        switch ( this.value ) {
            case 1f:
                spriteRenderer.sprite = hearthSprites[2];
                break;
            case 0.5f:
                spriteRenderer.sprite = hearthSprites[1];
                break;
            case 0f:
                spriteRenderer.sprite = hearthSprites[0];
                break;
        }
    }

    /// <summary>
    /// Restore hearth.
    /// </summary>
    /// <returns>void</returns>
    public void Resotore() {
        this.value = 1f;
        spriteRenderer.sprite = hearthSprites[2];
    }


    /// <summary>
    /// Init class method.
    /// </summary>
    /// <returns>void</returns>
    private void Init() {

        // get sprite renderer component.
        spriteRenderer = GetComponent<SpriteRenderer>();
    }
}
