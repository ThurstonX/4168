using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.SceneManagement;

public class PlayerCharacter : MonoBehaviour
{
    // Public variables
    public int currentHealth;
    public TMP_Text healthText;
    public float invincibilityDuration = 1f; 
    private bool isInvincible = false;
    private float invincibilityTimer;
    public AudioSource dmgSound;
    public AudioSource healSound;

    void Start()
    {
        // Initialize the player's health to the maximum at the start of the game
        currentHealth = 3;
    }

    void Update()
    {
        // Handle invincibility timer countdown
        if (isInvincible)
        {
            invincibilityTimer -= Time.deltaTime;
            if (invincibilityTimer <= 0)
            {
                isInvincible = false;
            }
        }

        // If you fall, you died
        if (transform.position.y < -10f)
        {
            Die();
        }
    }

    // This function will be called when the player collides with something
    void OnCollisionEnter(Collision collision)
    {
        // Check if the player collided with an enemy
        if (collision.gameObject.CompareTag("Enemy"))
        {
            // Only take damage if not invincible
            if (!isInvincible)
            {
                currentHealth -= 1;
                dmgSound.Play();
                BecomeInvincible();
            }

            // If you has no health left, you dies
            if (currentHealth <= 0)
            {
                Die();
            }
        }
        
        // Update health UI
        healthText.text = currentHealth.ToString();
    }

    // Check if the player collided with a heart, and destroy the heart
    void OnTriggerEnter(Collider heart)
    {
        if (heart.gameObject.CompareTag("Heart"))
        {
            currentHealth += 1;
            healthText.text = currentHealth.ToString();
            healSound.Play();
            Destroy(heart.gameObject);
        }
    }

    // If you are injured, you become invincible for a while
    void BecomeInvincible()
    {
        isInvincible = true;
        invincibilityTimer = invincibilityDuration;
    }

    // If you die, go to main menu
    void Die()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }
}
