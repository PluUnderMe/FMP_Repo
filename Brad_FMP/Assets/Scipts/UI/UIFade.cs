using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class UIFade : MonoBehaviour
{
    public TMP_Text uiText;       // Reference to the TextMeshPro Text component
    public GameObject gameOverScreen; // Reference to the GameOverScreen GameObject
    public float fadeDuration = 5f;   // Duration for the fade in/out
    public float displayDuration = 5f; // Time the text stays fully visible

    private void Start()
    {
        if (uiText == null)
        {
            uiText = GetComponent<TMP_Text>();
        }
        if (uiText != null)
        {
            StartCoroutine(FadeInAndOut());
        }
        else
        {
            Debug.LogError("No TMP_Text component found on the GameObject.");
        }
    }

    private IEnumerator FadeInAndOut()
    {
        // Fade In
        yield return StartCoroutine(FadeTextToFullAlpha());
        // Display for the specified duration
        yield return new WaitForSeconds(displayDuration);
        // Fade Out
        yield return StartCoroutine(FadeTextToZeroAlpha());
        // Activate the GameOverScreen object
        gameOverScreen.SetActive(true);
    }

    private IEnumerator FadeTextToFullAlpha()
    {
        uiText.color = new Color(uiText.color.r, uiText.color.g, uiText.color.b, 0);
        while (uiText.color.a < 1.0f)
        {
            uiText.color = new Color(uiText.color.r, uiText.color.g, uiText.color.b, uiText.color.a + (Time.deltaTime / fadeDuration));
            yield return null;
        }
    }

    private IEnumerator FadeTextToZeroAlpha()
    {
        uiText.color = new Color(uiText.color.r, uiText.color.g, uiText.color.b, 1);
        while (uiText.color.a > 0.0f)
        {
            uiText.color = new Color(uiText.color.r, uiText.color.g, uiText.color.b, uiText.color.a - (Time.deltaTime / fadeDuration));
            yield return null;
        }
    }
}
