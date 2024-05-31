using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class TextScroll : MonoBehaviour
{
    public float scrollSpeed = 20f; // Adjust this value to control the speed of scrolling
    private TextMeshProUGUI textMeshPro;

    private void Start()
    {
        textMeshPro = GetComponent<TextMeshProUGUI>();
        StartCoroutine(ScrollText());
    }

    private System.Collections.IEnumerator ScrollText()
    {
        // Calculate the initial position of the text
        float startY = textMeshPro.rectTransform.position.y;
        float endY = startY + textMeshPro.rectTransform.rect.height;

        // Set the starting position
        Vector3 startPos = new Vector3(textMeshPro.rectTransform.position.x, startY, textMeshPro.rectTransform.position.z);

        // Set the ending position
        Vector3 endPos = new Vector3(textMeshPro.rectTransform.position.x, endY, textMeshPro.rectTransform.position.z);

        // Move the text from start to end position
        float t = 0f;
        while (t < 1f)
        {
            t += Time.deltaTime * (scrollSpeed / textMeshPro.rectTransform.rect.height);
            textMeshPro.rectTransform.position = Vector3.Lerp(startPos, endPos, t);
            yield return null;
        }

        // Reset position to start position for continuous scrolling
        textMeshPro.rectTransform.position = startPos;
        StartCoroutine(ScrollText());
    }
}
