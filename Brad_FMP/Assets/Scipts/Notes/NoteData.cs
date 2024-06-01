using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class NoteData : MonoBehaviour
{
    // Reference to the background image of the note data
    [SerializeField] Image bgImage = null;
    // Reference to the label text of the note data
    [SerializeField] TextMeshProUGUI label = null;

    // The note associated with this note data
    private Note note = null;
    // The rect transform component of the note data game object
    private RectTransform rect = null;
    public RectTransform Rect
    {
        // Property to get the rect transform component
        get
        {
            if (rect == null)
            {
                // If rect transform is null, try to get the component
                rect = GetComponent<RectTransform>();
                // If still null, add a rect transform component to the game object
                if (rect == null) { rect = gameObject.AddComponent<RectTransform>(); }
            }
            return rect;
        }
    }

    // Method to update the information of the note data
    public void UpdateInfo(Note note, Color color)
    {
        // Set the note and update the label text and background color
        this.note = note;
        label.text = note.Label;
        bgImage.color = color;
    }

    // Method to display the associated note
    public void Display()
    {
        // Display the note using the NotesSystem
        NotesSystem.Display(note);
    }
}