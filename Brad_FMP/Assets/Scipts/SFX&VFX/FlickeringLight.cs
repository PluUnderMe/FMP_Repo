using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FlickeringLight : MonoBehaviour
{
    private Light spotlight;

    // Minimum and maximum time for the light to stay on before it starts flashing
    public float initialOnTime = 2f;

    // Minimum and maximum time between flashes (light off)
    public float minFlashOffTime = 0.05f;
    public float maxFlashOffTime = 0.2f;

    // Minimum and maximum time between flash sequences
    public float minSequenceTime = 1f;
    public float maxSequenceTime = 3f;

    // Use this for initialization
    void Start()
    {
        spotlight = GetComponent<Light>();
        StartCoroutine(BrokenLightEffect());
    }

    IEnumerator BrokenLightEffect()
    {
        // Initially turn the light on
        spotlight.enabled = true;

        // Keep the light on for the initial on time
        yield return new WaitForSeconds(initialOnTime);

        while (true)
        {
            // Perform a sequence of quick flashes off
            int flashCount = Random.Range(2, 5); // Random number of flashes per sequence
            for (int i = 0; i < flashCount; i++)
            {
                spotlight.enabled = false; // Turn light off
                yield return new WaitForSeconds(Random.Range(minFlashOffTime, maxFlashOffTime));
                spotlight.enabled = true; // Turn light back on
                yield return new WaitForSeconds(Random.Range(minFlashOffTime, maxFlashOffTime));
            }

            // Ensure the light is on at the end of the flash sequence
            spotlight.enabled = true;

            // Wait for a random time before starting the next flash sequence
            yield return new WaitForSeconds(Random.Range(minSequenceTime, maxSequenceTime));
        }
    }
}
