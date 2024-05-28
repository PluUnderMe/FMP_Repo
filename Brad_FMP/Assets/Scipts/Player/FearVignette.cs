using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering.PostProcessing;

public class FearVignette : MonoBehaviour
{
    public FearBar fearBar; // Reference to the FearBar script
    public PostProcessVolume volume; // Reference to the Post-Processing Volume
    private Vignette vignette; // Reference to the Vignette effect

    void Start()
    {
        // Get the Vignette effect from the Post-Processing Volume
        volume.profile.TryGetSettings(out vignette);

        // Subscribe to the fear bar's event
        fearBar.OnFearValueChanged += UpdateVignette;
    }

    void UpdateVignette(float fearValue)
    {
        // Map the fear value to the vignette intensity (adjust as needed)
        float vignetteIntensity = Mathf.Lerp(0f, 1f, fearValue / fearBar.maxValue);

        // Set the vignette intensity
        vignette.intensity.value = vignetteIntensity;
    }

    void OnDestroy()
    {
        // Unsubscribe from the fear bar's event
        fearBar.OnFearValueChanged -= UpdateVignette;
    }
}
