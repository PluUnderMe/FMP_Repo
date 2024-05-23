using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NotePickUp : MonoBehaviour
{
    [SerializeField] Note note = null;

    [SerializeField] bool autoDisplay = false;
    [SerializeField] bool add = true;
    
    private bool inReach;

    public GameObject pickUpText;

    public AudioSource pickUpSound;

    // Start is called before the first frame update
    void Start()
    {
        inReach = false;
        pickUpText.SetActive(false);
    }

    void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "Reach")
        {
            inReach = true;
            pickUpText.SetActive(true);
        }

    }

    void OnTriggerExit(Collider other)
    {
        if (other.gameObject.tag == "Reach")
        {
            inReach = false;
            pickUpText.SetActive(false);
        }
    }

    // Update is called once per frame
    void Update()
    {
        if(Input.GetButtonDown("Interact") && inReach)
        {
            PickUpNote();               
        }
    }

    public void PickUpNote()
    {
        pickUpSound.Play();
        inReach = false;
        pickUpText.SetActive(false);
        Destroy(gameObject);

        if (autoDisplay)
        {
            NotesSystem.Display(note);
        }
        if (add)
        {
            NotesSystem.AddNote(note.Label, note);
        }

    }
}
