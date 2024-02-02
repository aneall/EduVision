using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class changeMobileFOV : MonoBehaviour
{
    public Camera microscope;
    public float fovChangeSpeed = 1.0f;
    private float minFov = 7.0f;
    private float maxFov = 61.0f;

    // Start is called before the first frame update
    void Start()
    {
        // nothing here
    }

    // Update is called once per frame
    void Update()
    {
        if (microscope == null) return; // If there's no camera, then this won't work

        // If the DOWN-arrow key is pressed, we will increase the FOV toward 61.0:
        if (Input.GetKey(KeyCode.DownArrow))
        {
            microscope.fieldOfView = Mathf.Min(microscope.fieldOfView + fovChangeSpeed * Time.deltaTime, maxFov);
        }

        // If the UP-arrow key is pressed, we will decrease the FOV toward 7.0:
        if (Input.GetKey(KeyCode.UpArrow))
        {
            microscope.fieldOfView = Mathf.Max(microscope.fieldOfView - fovChangeSpeed * Time.deltaTime, minFov);
        }
    }
}
