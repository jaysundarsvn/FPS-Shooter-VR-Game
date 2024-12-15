using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class Health : MonoBehaviour
{
    // Public variables accessible from the Unity Editor
    public float health = 100; // Initial health value
    public Image healthImg; // Reference to the Image component for visual representation of health
    public TextMeshProUGUI healthText; // Reference to the TextMeshPro component for displaying health value as text

    // Method for dealing damage to the health
    public void Damage(int damage)
    {
        // Reduce health by the damage amount
        health -= damage;

        // Update the visual representation of health using the fill amount of the healthImg Image component
        if(healthImg != null)
        {
            healthImg.fillAmount = health / 100f; // Divide current health by maximum health to get fill amount (normalized between 0 and 1)
        }

        if(healthText != null)
        {
            healthText.text = health.ToString("F0"); // Convert health to string and format it with no decimal places
        }
        // Update the text displaying the health value
    }

    public void Heal(int life)
    {
        // Reduce health by the damage amount
        health += life;

        // Update the visual representation of health using the fill amount of the healthImg Image component
        if (healthImg != null)
        {
            healthImg.fillAmount = health / 100f; // Divide current health by maximum health to get fill amount (normalized between 0 and 1)
        }

        if (healthText != null)
        {
            healthText.text = health.ToString("F0"); // Convert health to string and format it with no decimal places
        }
        // Update the text displaying the health value
    }

    public void ResetHealth()
    {
        health = 100;
        // Update the visual representation of health using the fill amount of the healthImg Image component
        if (healthImg != null)
        {
            healthImg.fillAmount = 1; // Divide current health by maximum health to get fill amount (normalized between 0 and 1)
        }

        if (healthText != null)
        {
            healthText.text = health.ToString("F0"); // Convert health to string and format it with no decimal places
        }
        
    }
}
