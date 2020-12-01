using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    [Header("General")]
    [Tooltip("In ms^-1")][SerializeField] float Speed = 15f;
    [Tooltip("In m")] [SerializeField] float xRange = 5.75f;
    [Tooltip("In m")] [SerializeField] float yRange = 3f;

    [Header("Screen-position Based")]
    [SerializeField] float positionPitchFactor = -5f;
    [SerializeField] float positionYawFactor = 5f;
   
    [Header("Control-throw Based")]
    [SerializeField] float controlPitchFactor = -15f;
    [SerializeField] float controlRollFactor = -15f;

    float xThrow, yThrow;
    bool isControlEnabled = true;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (isControlEnabled)
        {
            ProcessTranslation();
            ProcessRotation();
        }
    }

    private void ProcessTranslation()
    {
        xThrow = Input.GetAxis("Horizontal");
        yThrow = Input.GetAxis("Vertical");
        float xOffset = xThrow * Speed * Time.deltaTime, yOffset = yThrow * Speed * Time.deltaTime;
        float rawXPos = transform.localPosition.x + xOffset, rawYPos = transform.localPosition.y + yOffset;

        float clampedXPos = Mathf.Clamp(rawXPos, -xRange, xRange);
        float clampedYPos = Mathf.Clamp(rawYPos, -yRange, yRange);

        transform.localPosition = new Vector3(clampedXPos, clampedYPos, transform.localPosition.z);
    }

    private void ProcessRotation()
    {
        float pitch = transform.localPosition.y * positionPitchFactor + yThrow * controlPitchFactor;
        float yaw = transform.localPosition.x * positionYawFactor;
        float roll = xThrow * controlRollFactor;
        transform.localRotation = Quaternion.Euler(pitch, yaw, roll);
    }
    
    void StartDeathSequence()
    {
        isControlEnabled = false;
    }
}
