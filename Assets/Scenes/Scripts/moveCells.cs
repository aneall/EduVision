using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class moveCells : MonoBehaviour
{
    public float transformFrequency = 1.0f; // frequency for both translocation and rotation transforms

    public float translocateAmplitude = 0.15f; // maximum displacement from original x,y,z position
    public float rotateAmplitude = 15.0f; // maximum euler angler degree for x,y,z-axis rotation

    // Declaring private variables (only in scope of the component attached to a certain GameObject) for original position and rotation:
    private Vector3 originalPosition;
    private Vector3 originalRotation;

    // Start is called before the first frame update
    void Start()
    {
        originalPosition = transform.position;
        originalRotation = transform.eulerAngles;
    }

    // Update is called once per frame
    void Update()
    {
       // Oscillate cell objects' positions:
        float translocateSin = Mathf.Sin(Time.time * transformFrequency) * translocateAmplitude;
        // 0.5f is the amplitude of ???
        transform.position = originalPosition + new Vector3(translocateSin, translocateSin, translocateSin);
           


       // Oscillate cell objects' rotations:
        float rotateSin = Mathf.Sin(Time.time * transformFrequency) * rotateAmplitude;
        transform.eulerAngles = originalRotation + new Vector3(rotateSin, rotateSin, rotateSin);
    }
}