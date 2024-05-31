using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CandleView : MonoBehaviour
{
    public GameObject objectToShow; // Reference to the object to make visible

    public void MakeVisible()
    {
        // Check if the object to show is not null
        if (objectToShow != null)
        {
            // Set the object to be visible
            objectToShow.SetActive(true);
        }
        else
        {
            Debug.LogError("Object to show is null!");
        }
    }
}
