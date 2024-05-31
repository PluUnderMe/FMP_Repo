using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TriggerSFX : MonoBehaviour
{

    public AudioSource playSound;
    private bool hasTriggered = false; // Flag to ensure the trigger can only be activated once

    public FearBar fearBar; // Reference to the FearBar script

    private void OnTriggerEnter(Collider other)
    {
        // Check if the trigger has already been activated
        if (hasTriggered)
        {
            return;
        }

        // Check if the other collider is the player or any other specific object you want
        if (other.CompareTag("Player")) // Assuming the player has a tag "Player"
        {
            hasTriggered = true; // Set the flag to true to prevent re-triggering

            playSound.Play();

            // Add 10 to the fear bar
            if (fearBar != null)
            {
                fearBar.IncreaseFear(10f);
            }

            // Destroy the GameObject after the clip has finished playing
            Destroy(gameObject, playSound.clip.length);
        }
    }
}
