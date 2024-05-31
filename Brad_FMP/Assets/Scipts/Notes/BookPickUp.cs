using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;


public class BookPickUp : MonoBehaviour
{
    [SerializeField] GameObject bookObject = null;
    [SerializeField] AudioSource pickUpSound = null;
    [SerializeField] GameObject pickUpText = null;

    private bool inReach = false;
    private bool canInteract = false;
    private bool pickedUp = false;

    void Start()
    {
        inReach = false;
        canInteract = false;
        pickedUp = false;
        pickUpText.SetActive(false);
    }

    void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "Reach")
        {
            inReach = true;
            if (canInteract && !pickedUp)
            {
                pickUpText.SetActive(true);
            }
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

    void Update()
    {
        if (canInteract && !pickedUp && inReach && Input.GetButtonDown("Interact"))
        {
            PickUpBook();
        }
    }

    public void SetCanInteract(bool value)
    {
        canInteract = value;
        if (canInteract && inReach && !pickedUp)
        {
            pickUpText.SetActive(true);
        }
        else
        {
            pickUpText.SetActive(false);
        }
    }

    public void PickUpBook()
    {
        if (pickUpSound != null)
        {
            pickUpSound.Play();
        }
        pickedUp = true;
        pickUpText.SetActive(false);
        bookObject.SetActive(false);
        // Additional logic for what happens when the book is picked up
        SceneManager.LoadScene("Death Finished");
    }
}
