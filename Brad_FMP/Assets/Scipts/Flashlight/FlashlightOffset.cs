using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FlashlightOffset : MonoBehaviour
{
    // Vector to store the offset between the flashlight and the camera
    private Vector3 vectOffset;
    // GameObject that the flashlight will follow (in this case, the main camera)
    private GameObject goFollow;
    // Speed at which the flashlight will rotate to match the camera's rotation
    [SerializeField] private float speed = 3.0f;

    // Initialization method
    void Start()
    {
        // Assign the main camera to goFollow
        goFollow = Camera.main.gameObject;
        // Calculate the initial offset between the flashlight and the camera
        vectOffset = transform.position - goFollow.transform.position;
    }

    // Update method called once per frame
    void Update()
    {
        // Update the flashlight's position to maintain the offset from the camera
        transform.position = goFollow.transform.position + vectOffset;
        // Smoothly rotate the flashlight to match the camera's rotation
        transform.rotation = Quaternion.Slerp(transform.rotation, goFollow.transform.rotation, speed * Time.deltaTime);
    }
}
