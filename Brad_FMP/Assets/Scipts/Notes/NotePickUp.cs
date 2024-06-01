using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NotePickUp : MonoBehaviour
{
    // Note to be picked up
    [SerializeField] Note note = null;

    // Flag to automatically display the note when picked up
    [SerializeField] bool autoDisplay = false;
    // Flag to add the note to the NotesSystem
    [SerializeField] bool add = true;

    // Flag to check if the player is within reach to pick up the note
    private bool inReach;

    // UI element to show the pick-up text
    public GameObject pickUpText;

    // Start is called before the first frame update
    void Start()
    {
        // Initialize inReach to false and hide the pick-up text
        inReach = false;
        pickUpText.SetActive(false);
    }

    // Triggered when another collider enters the trigger collider attached to this object
    void OnTriggerEnter(Collider other)
    {
        // Check if the other collider has the tag "Reach"
        if (other.gameObject.tag == "Reach")
        {
            // Set inReach to true and show the pick-up text
            inReach = true;
            pickUpText.SetActive(true);
        }
    }

    // Triggered when another collider exits the trigger collider attached to this object
    void OnTriggerExit(Collider other)
    {
        // Check if the other collider has the tag "Reach"
        if (other.gameObject.tag == "Reach")
        {
            // Set inReach to false and hide the pick-up text
            inReach = false;
            pickUpText.SetActive(false);
        }
    }

    // Update is called once per frame
    void Update()
    {
        // Check if the interact button is pressed and the player is within reach
        if (Input.GetButtonDown("Interact") && inReach)
        {
            // Call the PickUpNote method
            PickUpNote();
        }
    }

    // Method to handle picking up the note
    public void PickUpNote()
    {
        // Set inReach to false and hide the pick-up text
        inReach = false;
        pickUpText.SetActive(false);
        // Destroy the game object (the note)
        Destroy(gameObject);

        // If autoDisplay is true, display the note using the NotesSystem
        if (autoDisplay)
        {
            NotesSystem.Display(note);
        }
        // If add is true, add the note to the NotesSystem
        if (add)
        {
            NotesSystem.AddNote(note.Label, note);
        }
    }
}