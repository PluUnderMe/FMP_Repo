using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Audio;

public class StaminaBar : MonoBehaviour
{
    public float maxStamina = 100f;
    public float currentStamina;
    public float staminaDecreaseRate = 10f; // Stamina decrease rate per second when running
    public float staminaRegenRate = 10f; // Stamina regen rate per second when not running
    public KeyCode runKey = KeyCode.LeftShift;

    public AudioMixer mixer; // Reference to the Audio Mixer
    public string exhaustionParameter = "Exhaustion"; // Name of the exposed parameter for exhaustion sound


    private bool isRunning = false;

    void Start()
    {
        currentStamina = maxStamina;
    }

    void Update()
    {
        if (Input.GetKey(runKey) && currentStamina > 0)
        {
            isRunning = true;
            currentStamina -= staminaDecreaseRate * Time.deltaTime;
        }
        else
        {
            isRunning = false;
            currentStamina += staminaRegenRate * Time.deltaTime;
        }

        currentStamina = Mathf.Clamp(currentStamina, 0f, maxStamina);

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
