using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class BookPickUp : MonoBehaviour
{
    // Reference to the book game object
    [SerializeField] GameObject bookObject = null;
    // Reference to the pick-up sound
    [SerializeField] AudioSource pickUpSound = null;
    // Reference to the pick-up text UI element
    [SerializeField] GameObject pickUpText = null;

    // Flags to track interaction state
    private bool inReach = false; // Whether the player is within reach to pick up the book
    private bool canInteract = false; // Whether interaction with the book is allowed
    private bool pickedUp = false; // Whether the book has been picked up

    void Start()
    {
        // Initialize interaction flags and hide pick-up text
        inReach = false;
        canInteract = false;
        pickedUp = false;
        pickUpText.SetActive(false);
    }

    // Triggered when another collider enters the trigger collider attached to this object
    void OnTriggerEnter(Collider other)
    {
        // Check if the other collider has the tag "Reach"
        if (other.gameObject.tag == "Reach")
        {
            inReach = true;
            // Show pick-up text if interaction is allowed and the book has not been picked up
            if (canInteract && !pickedUp)
            {
                pickUpText.SetActive(true);
            }
        }
    }

    // Triggered when another collider exits the trigger collider attached to this object
    void OnTriggerExit(Collider other)
    {
        // Check if the other collider has the tag "Reach"
        if (other.gameObject.tag == "Reach")
        {
            inReach = false;
            // Hide pick-up text
            pickUpText.SetActive(false);
        }
    }

    void Update()
    {
        // Check if interaction is allowed, the book has not been picked up, and the player is within reach
        if (canInteract && !pickedUp && inReach && Input.GetButtonDown("Interact"))
        {
            // Call the PickUpBook method
            PickUpBook();
        }
    }

    // Method to set whether interaction with the book is allowed
    public void SetCanInteract(bool value)
    {
        // Set the canInteract flag
        canInteract = value;
        // Show pick-up text if interaction is allowed, the player is within reach, and the book has not been picked up
        if (canInteract && inReach && !pickedUp)
        {
            pickUpText.SetActive(true);
        }
        else
        {
            // Otherwise, hide pick-up text
            pickUpText.SetActive(false);
        }
    }

    // Method to handle picking up the book
    public void PickUpBook()
    {
        // Play pick-up sound if available
        if (pickUpSound != null)
        {
            pickUpSound.Play();
        }
        // Set pickedUp flag to true
        pickedUp = true;
        // Hide pick-up text
        pickUpText.SetActive(false);
        // Disable the book object
        bookObject.SetActive(false);
        // Additional logic for what happens when the book is picked up, such as loading a new scene
        SceneManager.LoadScene("Death Finished");
    }
}