using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour {
    
    [SerializeField]
    private GameObject defeatedParticles;                    // Particle system used when the enemy is defeated.

    /// <summary>
    /// Get defeated particle
    /// system gameObject.
    /// </summary>
    /// <returns>GameObject</returns>
    public GameObject GetDefeatedParticles() {
        return this.defeatedParticles;
    }

}
