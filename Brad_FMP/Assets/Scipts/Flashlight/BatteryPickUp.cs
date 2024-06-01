using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BatteryPickUp : MonoBehaviour
{
    private bool inReach; // Tracks whether the player is in reach to pick up the battery

    public GameObject pickUpText; // Reference to the UI text that indicates the pick-up action
    private GameObject flashlight; // Reference to the flashlight object

    public AudioSource pickUpSound; // Sound to play when the battery is picked up

    void Start()
    {
        inReach = false; // Initialize inReach to false
        pickUpText.SetActive(false); // Hide the pick-up text initially
        flashlight = GameObject.Find("Flashlight"); // Find the flashlight object in the scene
    }

    // Detect when the player enters the trigger area
    void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "Reach") // Check if the object entering the trigger has the "Reach" tag
        {
            inReach = true; // Set inReach to true
            pickUpText.SetActive(true); // Show the pick-up text
        }
    }

    // Detect when the player exits the trigger area
    void OnTriggerExit(Collider other)
    {
        if (other.gameObject.tag == "Reach") // Check if the object exiting the trigger has the "Reach" tag
        {
            inReach = false; // Set inReach to false
            pickUpText.SetActive(false); // Hide the pick-up text
        }
    }

    void Update()
    {
        if (Input.GetButtonDown("Interact") && inReach) // Check if the interact button is pressed and the player is in reach
        {
            flashlight.GetComponent<Flashlight>().batteries += 1; // Increase the battery count on the flashlight
            flashlight.GetComponent<Flashlight>().UpdateBatteryCountImage(); // Update the battery count image on the UI
            pickUpSound.Play(); // Play the pick-up sound
            inReach = false; // Set inReach to false
            pickUpText.SetActive(false); // Hide the pick-up text
            Destroy(gameObject); // Destroy the battery object
        }
    }
}