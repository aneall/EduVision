using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using LookingGlass; // we must use the looking glass namespace, otherwise the HologramCamera class will not be recognized

public class changeLGSize : MonoBehaviour
{
    public HologramCamera hologramMicroscope;
    // public float CameraProperties; 
    public float sizeChangeSpeed = 1.0f;

    private float minSize = 1.2f;
    private float maxSize = 5.0f;

    // Start is called before the first frame update
    void Start()
    {
        // nothing here
    }

    // Update is called once per frame
    void Update()
    {
        if (hologramMicroscope == null) return; // If there's no camera, then this won't work

        // If the DOWN-arrow key is pressed, we will increase the size toward 5:
        if (Input.GetKey(KeyCode.DownArrow))
        {
            hologramMicroscope.CameraProperties.Size = Mathf.Min(hologramMicroscope.CameraProperties.Size + sizeChangeSpeed * Time.deltaTime, maxSize);
        }

        // If the UP-arrow key is pressed, we will decrease the size toward 1.2:
        if (Input.GetKey(KeyCode.UpArrow))
        {
            hologramMicroscope.CameraProperties.Size = Mathf.Max(hologramMicroscope.CameraProperties.Size - sizeChangeSpeed * Time.deltaTime, minSize);
        }
    }
}
