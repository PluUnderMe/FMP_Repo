using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RadioToggle : MonoBehaviour
{
    private bool inReach;

    public GameObject radioStatic;
    public GameObject TurnOnOffText;

    public AudioSource toggleSound;

    void Start()
    {
        inReach = false;
        TurnOnOffText.SetActive(false);
        
    }

    void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "Reach")
        {
            inReach = true;
            TurnOnOffText.SetActive(true);
        }

    }

    void OnTriggerExit(Collider other)
    {
        if (other.gameObject.tag == "Reach")
        {
            inReach = false;
            TurnOnOffText.SetActive(false);
        }
    }




    void Update()
    {
        if (Input.GetButtonDown("Interact") && inReach)
        {

            toggleSound.Play();
            inReach = false;
            TurnOnOffText.SetActive(false);
            // Toggle the active state of the radioStatic GameObject
            radioStatic.SetActive(!radioStatic.activeSelf);
        }

    }
}
