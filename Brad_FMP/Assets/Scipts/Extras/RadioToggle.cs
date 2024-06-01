using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RadioToggle : MonoBehaviour
{
    // Flag to check if the player is in reach of the radio
    private bool inReach;

    // GameObject representing the radio static effect
    public GameObject radioStatic;
    // GameObject representing the UI text for turning the radio on/off
    public GameObject TurnOnOffText;

    // AudioSource for the toggle sound effect
    public AudioSource toggleSound;

    // Initialization method
    void Start()
    {
        inReach = false; // Initially, the player is not in reach
        TurnOnOffText.SetActive(false); // Hide the on/off text initially
    }

    // Method called when another collider enters the trigger collider
    void OnTriggerEnter(Collider other)
    {
        // Check if the entering collider has the tag "Reach"
        if (other.gameObject.tag == "Reach")
        {
            inReach = true; // Set the inReach flag to true
            TurnOnOffText.SetActive(true); // Show the on/off text
        }
    }

    // Method called when another collider exits the trigger collider
    void OnTriggerExit(Collider other)
    {
        // Check if the exiting collider has the tag "Reach"
        if (other.gameObject.tag == "Reach")
        {
            inReach = false; // Set the inReach flag to false
            TurnOnOffText.SetActive(false); // Hide the on/off text
        }
    }

    // Update method called once per frame
    void Update()
    {
        // Check if the interact button is pressed and the player is in reach
        if (Input.GetButtonDown("Interact") && inReach)
        {
            toggleSound.Play(); // Play the toggle sound effect
            inReach = false; // Set the inReach flag to false
            TurnOnOffText.SetActive(false); // Hide the on/off text
            // Toggle the active state of the radioStatic GameObject
            radioStatic.SetActive(!radioStatic.activeSelf);
        }
    }
}