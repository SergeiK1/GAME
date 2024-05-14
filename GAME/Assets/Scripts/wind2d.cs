using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Wind2D : MonoBehaviour
{
    public Vector2 windForce = new Vector2(5f, 0f); // Initial wind force
    private float switchTime = 5f; // Time interval for switching wind direction
    private float timer = 0f; // Timer to track time

    // References to the two particle systems
    public ParticleSystem windParticleRL; // Particle system for right-to-left wind
    public ParticleSystem windParticleLR; // Particle system for left-to-right wind

    void Start()
    {
        // Ensure only one particle system is playing initially
        if (windForce.x > 0)
        {
            windParticleRL.Stop();
            windParticleLR.Play();
        }
        else
        {
            windParticleRL.Play();
            windParticleLR.Stop();
        }
    }

    void Update()
    {
        // Update the timer
        timer += Time.deltaTime;

        // Check if it's time to switch the wind direction
        if (timer >= switchTime)
        {
            windForce = -windForce; // Reverse the wind direction
            timer = 0f; // Reset the timer
            UpdateParticleDirection(); // Update particle direction
        }
    }

    void OnTriggerStay2D(Collider2D other)
    {
        if (other.CompareTag("Player")) // Assuming the player object has a tag "Player"
        {
            Rigidbody2D rb = other.GetComponent<Rigidbody2D>();
            if (rb != null)
            {
                rb.velocity += windForce * Time.deltaTime; // Apply wind force to the player's Rigidbody2D
            }
        }
    }

    void OnTriggerExit2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            // Stop both particle systems when the player exits the wind zone
            windParticleRL.Stop();
            windParticleLR.Stop();
        }
    }

    void UpdateParticleDirection()
    {
        if (windForce.x > 0)
        {
            windParticleRL.Stop();
            windParticleLR.Play();
        }
        else
        {
            windParticleRL.Play();
            windParticleLR.Stop();
        }
    }
}
