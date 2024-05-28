using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class Flashlight : MonoBehaviour
{
    public FearBar fearBar; // Reference to the FearBar script
    public NotesSystem notesSystem; 

    public Light light;
    public TMP_Text lifetimeText;

    public TMP_Text batteryText;

    public float decreaseRate = 1f; // Rate at which the flashlight consumes battery
    public float lifetime = 100f; // Lifetime of the flashlight in seconds

    public float batteries = 0;

    public AudioSource flashON;
    public AudioSource flashOFF;

    private bool on;
    private bool off;

    public bool isOn = false; // Track the state of the flashlight

    void Start()
    {
        light = GetComponent<Light>();

        off = true;
        light.enabled = false;

        // Get the FearBar component attached to this GameObject
        fearBar = GetComponent<FearBar>();

    }



    void Update()
    {
        lifetimeText.text = lifetime.ToString("0") + "%";
        batteryText.text = batteries.ToString();

        if (!PauseMenu.GameIsPaused && !NotesSystem.usingNoteSystem)
        {
            // Check flashlight input and toggle state
            if (Input.GetButtonDown("Flashlight"))
            {
                ToggleFlashlight();
            }

            if (isOn)
            {
                lifetime -= decreaseRate * Time.deltaTime;
                lifetime = Mathf.Max(lifetime, 0f); // Ensure the lifetime doesn't go below 0
            }

            if (lifetime <= 0)
            {
                // Turn off the flashlight if the battery is depleted
                TurnOffFlashlight();
                lifetime = 0;
            }

            if (lifetime >= 100)
            {
                lifetime = 100;
            }

            if (Input.GetButtonDown("Reload") && batteries >= 1)
            {
                batteries -= 1;
                lifetime += 50;
            }

            if (Input.GetButtonDown("Reload") && batteries == 0)
            {
                return;
            }

            if (batteries <= 0)
            {
                batteries = 0;
            }
        }
        void ToggleFlashlight()
        {
            if (!PauseMenu.GameIsPaused && !NotesSystem.usingNoteSystem)
            {
                // Toggle flashlight state
                isOn = !isOn;

                // Turn flashlight on/off based on state
                if (isOn)
                {
                    light.enabled = true;
                    flashON.Play();
                    // Decrease the FearBar's value while flashlight is on
                    fearBar.increaseRate = -0.5f; // Decrease rate
                }
                else
                {
                    TurnOffFlashlight();
                }
            }
        }
    }

    void TurnOffFlashlight()
    {
        if (light.enabled) // Check if the light is currently on
        {
            light.enabled = false;
            flashOFF.Play();
            isOn = false;
            // Set FearBar's increase rate back to its default value
            fearBar.increaseRate = 0.5f; // Default increase rate
        }
    }
}