using UnityEngine;
using UnityEngine.UI;

public class StaminaBar : MonoBehaviour
{
    public float maxStamina = 100f;
    public float currentStamina;
    public float staminaDecreaseRate = 10f; // Stamina decrease rate per second when running
    public float staminaRegenRate = 5f; // Stamina regen rate per second when not running
    public KeyCode runKey = KeyCode.LeftShift;


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


    }

}
