using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Audio;

public class StaminaBar : MonoBehaviour
{
    // Maximum stamina the player can have
    public float maxStamina = 100f;
    // Current stamina of the player
    public float currentStamina;
    // Rate at which stamina decreases per second when running
    public float staminaDecreaseRate = 10f;
    // Rate at which stamina regenerates per second when not running
    public float staminaRegenRate = 10f;
    // Key to trigger running
    public KeyCode runKey = KeyCode.LeftShift;

    // Reference to the Audio Mixer
    public AudioMixer mixer;
    // Name of the exposed parameter for exhaustion sound
    public string exhaustionParameter = "Exhaustion";

    // Flag indicating whether the player is running
    private bool isRunning = false;

    void Start()
    {
        // Initialize current stamina to maximum stamina
        currentStamina = maxStamina;
    }

    void Update()
    {
        // Check if the run key is pressed and current stamina is greater than 0
        if (Input.GetKey(runKey) && currentStamina > 0)
        {
            // Player is running
            isRunning = true;
            // Decrease current stamina based on decrease rate and time
            currentStamina -= staminaDecreaseRate * Time.deltaTime;
        }
        else
        {
            // Player is not running
            isRunning = false;
            // Increase current stamina based on regen rate and time
            currentStamina += staminaRegenRate * Time.deltaTime;
        }

        // Clamp current stamina between 0 and maximum stamina
        currentStamina = Mathf.Clamp(currentStamina, 0f, maxStamina);

        // Check if current stamina is depleted
        if (currentStamina <= 0)
        {
            // Set the exhaustion parameter in the Audio Mixer to play the exhaustion sound
            mixer.SetFloat(exhaustionParameter, 100); // Assuming 1 is the maximum value for the parameter
        }
        else
        {
            // Reset the exhaustion parameter in the Audio Mixer
            mixer.SetFloat(exhaustionParameter, 0); // Assuming 0 is the minimum value for the parameter
        }
    }
}