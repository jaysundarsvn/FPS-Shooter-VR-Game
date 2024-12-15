using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletRound : MonoBehaviour
{
    public ParticleSystem hitEffectPrefab; // Prefab of the particle system
    public int targetObjectLayer;
    public int damage;

    private void OnCollisionEnter(Collision collision)
    {

        if(collision.gameObject.layer == targetObjectLayer)
        {
            collision.gameObject.GetComponent<Health>().Damage(damage);
        }
        // Check if the collision object has a collider
        if (collision.collider != null)
        {
            // Instantiate the hit effect at the collision point
            if (hitEffectPrefab != null)
            {
                Instantiate(hitEffectPrefab, collision.contacts[0].point, Quaternion.identity);
            }
        }

        // Destroy the bullet GameObject after collision
        Destroy(gameObject);
    }
}
