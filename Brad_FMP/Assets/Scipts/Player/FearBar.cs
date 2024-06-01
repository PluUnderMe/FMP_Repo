using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System;
using UnityEngine.SceneManagement;
using UnityEngine.Audio;

public class FearBar : MonoBehaviour
{
    public int maxValue = 100; // Maximum value of the progress bar
    public float increaseRate = 1f; // Rate at which the value increases per second
    public float decreaseRate = 0.5f; // Rate at which the value decreases per second

    private float currentValue = 0f; // Current value of the progress bar
    private Slider fearBarSlider; // Reference to the UI slider

    public Flashlight flashlight; // Reference to the Flashlight script

    // Event to notify when fear value changes
    public event Action<float> OnFearValueChanged;

    public AudioMixer fearAudioMixer; // Reference to the Audio Mixer
    public string fearVolumeParameter = "FearVolume"; // Name of the exposed parameter

    void Start()
    {
        // Get the Slider component attached to this GameObject
        fearBarSlider = GetComponent<Slider>();

        // Set the maximum value of the slider
        fearBarSlider.maxValue = maxValue;

        // Initialize the value of the slider
        fearBarSlider.value = currentValue;
    }

    void Update()
    {
        // Check if flashlight is on
        if (flashlight.isOn)
        {
            // Decrease fear bar value while flashlight is on
            currentValue -= decreaseRate * Time.deltaTime;
            currentValue = Mathf.Max(currentValue, 0f); // Ensure the value doesn't go below 0
        }
        else
        {
            // Increase fear bar value while flashlight is off
            currentValue += increaseRate * Time.deltaTime;
            currentValue = Mathf.Min(currentValue, maxValue); // Ensure the value doesn't exceed maxValue
        }

        // Update the slider value to reflect the current fear bar value
        fearBarSlider.value = currentValue;

        // Update the audio mixer's volume based on the fear bar value
        UpdateAudioVolume(currentValue);

        // Check if the current value has reached the maximum value
        if (currentValue >= maxValue)
        {
            // Call the function DyingOfFear
            DyingOfFear();
        }

        // Invoke the event to notify subscribers about the fear value change
        OnFearValueChanged?.Invoke(currentValue);
    }

    // Function to update the audio mixer's volume based on the fear bar value
    void UpdateAudioVolume(float value)
    {
        // Convert the fear value to a decibel value 
        float volume = Mathf.Lerp(-10f, 20f, value / maxValue);
        fearAudioMixer.SetFloat(fearVolumeParameter, volume);
    }

    // Function to be called when the maximum value is reached
    void DyingOfFear()
    {
        // Load the death scene
        SceneManager.LoadScene("Death");
        //Debug.Log("Dying of Fear...");
    }

    // Function to increase fear value
    public void IncreaseFear(float amount)
    {
        currentValue += amount;
        currentValue = Mathf.Min(currentValue, maxValue); // Ensure the value doesn't exceed maxValue
        fearBarSlider.value = currentValue;
        OnFearValueChanged?.Invoke(currentValue);
        UpdateAudioVolume(currentValue);
    }

}