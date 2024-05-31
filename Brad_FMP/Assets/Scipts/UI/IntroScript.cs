using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class IntroScript : MonoBehaviour
{
    public float delayInSeconds = 20f; // Default delay before loading the next scene

    // Start is called before the first frame update
    void Start()
    {
        Invoke("LoadNextScene", delayInSeconds);
    }

    void LoadNextScene()
    {
        int nextSceneIndex = SceneManager.GetActiveScene().buildIndex + 1;
        if (nextSceneIndex < SceneManager.sceneCountInBuildSettings)
        {
            SceneManager.LoadScene(nextSceneIndex);
        }
        else
        {
            Debug.LogWarning("No next scene available. Make sure your scene build settings are configured correctly.");
        }
       
    }
}
