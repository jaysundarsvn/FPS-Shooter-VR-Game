using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Shooting : MonoBehaviour
{
    // Public variables to adjust in Unity Inspector
    public float bulletSpeed;               // Speed of the bullet
    public float fireRate;                  // Time between each shot
    public GameObject bulletRound;         // Reference to the bullet prefab

    bool shotsFired;                        // Flag to prevent rapid firing

    // Method to shoot the bullet
    public void Shoot()
    {
        // Check if a shot has already been fired
        if (!shotsFired)
        {
            // Instantiate a bullet at the current position and rotation
            var spawnBullet = GameObject.Instantiate(bulletRound, transform.position, transform.rotation);
            // Access the rigidbody component of the bullet
            var rb = spawnBullet.GetComponent<Rigidbody>();

            // Calculate and set the velocity of the bullet
            rb.velocity = spawnBullet.transform.forward * bulletSpeed;
            // Destroy the bullet after 3 seconds to avoid cluttering the scene
            Destroy(spawnBullet, 3f);

            // Set shotsFired to true to prevent rapid firing
            shotsFired = true;
            // Schedule a method call to reset shotsFired after the fireRate duration
            Invoke("ResetShots", fireRate);
        }
    }

    // Method to reset the shotsFired flag
    public void ResetShots()
    {
        shotsFired = false;
    }
}
