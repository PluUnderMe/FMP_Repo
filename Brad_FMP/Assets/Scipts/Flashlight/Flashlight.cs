using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class Flashlight : MonoBehaviour
{
    public FearBar fearBar; // Reference to the FearBar script
    public NotesSystem notesSystem; // Reference to the NotesSystem script

    public Light light; // Light component for the flashlight
    public TMP_Text lifetimeText; // UI Text to display flashlight lifetime
    public TMP_Text batteryText; // UI Text to display battery count
    public Image batteryImage; // UI Image for battery lifetime
    public Image batteryCountImage; // UI Image for battery count

    public Sprite[] batterySprites; // Array of sprites for different battery levels
    public Sprite[] batteryCountSprites; // Array of sprites for different battery counts

    public float decreaseRate = 1f; // Rate at which the flashlight consumes battery
    public float lifetime = 100f; // Lifetime of the flashlight in seconds
    public float batteries = 0f; // Number of spare batteries

    public AudioSource flashON; // Sound for turning the flashlight on
    public AudioSource flashOFF; // Sound for turning the flashlight off
    public AudioSource reloadSound; // Sound for reloading batteries

    private bool on; // Internal state for whether the flashlight is on
    private bool off; // Internal state for whether the flashlight is off

    public bool isOn = false; // Public state for whether the flashlight is on

    void Start()
    {
        light = GetComponent<Light>(); // Get the Light component attached to this GameObject

        off = true; // Flashlight starts off
        light.enabled = false; // Disable the Light component

        // Get the FearBar component attached to this GameObject
        fearBar = GetComponent<FearBar>();

        UpdateBatteryImage(); // Initialize battery image
        UpdateBatteryCountImage(); // Initialize battery count image
    }

    void Update()
    {
        lifetimeText.text = lifetime.ToString("0") + "%"; // Update lifetime text
        batteryText.text = batteries.ToString(); // Update battery count text

        // Check if the game is not paused and the notes system is not being used
        if (!PauseMenu.GameIsPaused && !NotesSystem.usingNoteSystem)
        {
            // Check flashlight input and toggle state
            if (Input.GetButtonDown("Flashlight"))
            {
                ToggleFlashlight();
            }

            // If the flashlight is on, decrease the battery lifetime
            if (isOn)
            {
                lifetime -= decreaseRate * Time.deltaTime;
                lifetime = Mathf.Max(lifetime, 0f); // Ensure the lifetime doesn't go below 0
                UpdateBatteryImage(); // Update battery image as lifetime decreases
            }

            // If the battery is depleted, turn off the flashlight
            if (lifetime <= 0)
            {
                TurnOffFlashlight();
                lifetime = 0;
            }

            // Ensure the lifetime doesn't exceed 100
            if (lifetime >= 100)
            {
                lifetime = 100;
            }

            // Check for reload input and reload batteries if available
            if (Input.GetButtonDown("Reload") && batteries >= 1)
            {
                batteries -= 1;
                lifetime += 50;
                reloadSound.Play();
                UpdateBatteryImage(); // Update battery image when reloading
                UpdateBatteryCountImage(); // Update battery count image when reloading
            }

            // If no batteries are available, do nothing on reload input
            if (Input.GetButtonDown("Reload") && batteries == 0)
            {
                return;
            }

            // Ensure battery count doesn't go below 0
            if (batteries <= 0)
            {
                batteries = 0;
                UpdateBatteryCountImage(); // Update battery count image when batteries are 0
            }
        }
    }

    // Method to toggle the flashlight on and off
    void ToggleFlashlight()
    {
        // Check if the game is not paused and the notes system is not being used
        if (!PauseMenu.GameIsPaused && !NotesSystem.usingNoteSystem)
        {
            isOn = !isOn; // Toggle flashlight state

            if (isOn)
            {
                light.enabled = true; // Enable the Light component
                flashON.Play(); // Play flashlight on sound
                fearBar.increaseRate = -0.5f; // Decrease FearBar's value while flashlight is on
            }
            else
            {
                TurnOffFlashlight(); // Turn off the flashlight
            }
        }
    }

    // Method to turn off the flashlight
    void TurnOffFlashlight()
    {
        if (light.enabled) // Check if the light is currently on
        {
            light.enabled = false; // Disable the Light component
            flashOFF.Play(); // Play flashlight off sound
            isOn = false; // Set flashlight state to off
            fearBar.increaseRate = 1f; // Reset FearBar's increase rate to default value
        }
    }

    // Method to update the battery image based on the current lifetime
    void UpdateBatteryImage()
    {
        if (lifetime > 75)
        {
            batteryImage.sprite = batterySprites[0]; // 100% image
        }
        else if (lifetime > 50)
        {
            batteryImage.sprite = batterySprites[1]; // 75% image
        }
        else if (lifetime > 25)
        {
            batteryImage.sprite = batterySprites[2]; // 50% image
        }
        else if (lifetime > 0)
        {
            batteryImage.sprite = batterySprites[3]; // 25% image
        }
        else
        {
            batteryImage.sprite = batterySprites[4]; // 0% image
        }
    }

    // Method to update the battery count image based on the current battery count
    public void UpdateBatteryCountImage()
    {
        int batteryIndex = Mathf.Clamp((int)batteries, 0, batteryCountSprites.Length - 1);
        batteryCountImage.sprite = batteryCountSprites[batteryIndex];
    }
}