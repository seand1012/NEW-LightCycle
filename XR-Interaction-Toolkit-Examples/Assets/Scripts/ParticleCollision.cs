using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ParticleCollision : MonoBehaviour
{
    void OnParticleCollision(GameObject other)
    {
        // Check if the colliding object is the particle system you are interested in
        // You can add more checks here if there are multiple particle systems
        if (other.CompareTag("Particle")) // Make sure your particle system has the tag "Particle"
        {
            Destroy(gameObject); // Destroy the motorbike
        }
    }
}
