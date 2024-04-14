using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System;

public class FearBar : MonoBehaviour
{  

    public int maxValue = 100; // Maximum value of the progress bar
    public float increaseRate = 0.5f; // Rate at which the value increases per second

    private float currentValue = 0f; // Current value of the progress bar
    private Slider fearBarSlider; // Reference to the UI slider

    public Flashlight flashlight; // Reference to the Flashlight script

    // Event to notify when fear value changes
    public event Action<float> OnFearValueChanged;

    void Start()
    {
        // Get the Slider component attached to this GameObject
        fearBarSlider = GetComponent<Slider>();

        // Set the maximum value of the slider
        fearBarSlider.maxValue = maxValue;

        // Initialize the value of the slider
        fearBarSlider.value = currentValue;

        // Start increasing the value over time
        //InvokeRepeating("IncreaseValue", 1f, 1f); // Invoke the method "IncreaseValue" every second
    }

    void IncreaseValue()
    {
        // Increase the current value by the specified rate
        currentValue += increaseRate;

        // Ensure the current value does not exceed the maximum value
        currentValue = Mathf.Min(currentValue, maxValue);

        // Update the slider value to reflect the current progress
        fearBarSlider.value = currentValue;

        // Invoke the event to notify subscribers about the fear value change
        OnFearValueChanged?.Invoke(currentValue);
    }
    void Update()
    {
        // Check if flashlight is on
        if (flashlight.isOn)
        {
            // Decrease fear bar value while flashlight is on
            currentValue -= increaseRate * Time.deltaTime;
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
    }
}
