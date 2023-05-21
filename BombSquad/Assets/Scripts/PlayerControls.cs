using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
public class PlayerControls : MonoBehaviour
{
    [Header("General Setup Settings")]
    [SerializeField] InputAction movement;
    [SerializeField] InputAction fire;
    [Tooltip("How fast ship moves up and down based on player input")]
    [SerializeField] float controlSpeed = 10.0f;
    [Tooltip("How fast player moves horizontally")][SerializeField] float xRange = 5.0f;
    [Tooltip("How fast player moves vertically")][SerializeField] float yRange = 5.0f;

    [Header("Laser gun arrary")]
    [Tooltip("Add all player lasers here")][SerializeField] GameObject[] Lasers;

    [Header("Screen position based tuning")]
    [SerializeField] float positionPitchFactor = -2.0f;
    [SerializeField] float positionYawFactor = 2.0f;

    [Header("Player input based tuning")]
    [SerializeField] float controlPitchFactor = -15.0f;
    [SerializeField] float controlRollFactor = -20.0f;
    float xThrow;
    float yThrow;
    float xOffset;
    float transformedXPos;
    float yOffset;
    float transformedYPos;
    float clampedXPos;
    float clampedYPos;
    float pitch = 0.0f; // rotates around x axis
    float yaw = 0.0f;   // rotates around y axis
    float roll = 0.0f;  // rotates around z axis

    private void OnEnable()
    {
        movement.Enable();
        fire.Enable();
    }

    private void OnDisable()
    {
        movement.Disable();
        fire.Disable();
    }

    // Update is called once per frame
    void Update()
    {
        TransformEuclideanCoord();
        TransforRotationAngle();
        ProcessFiring();
    }
    private void TransforRotationAngle()
    {
        float pitchDueToPosition = transform.localRotation.y * positionPitchFactor;
        float pitchDueToControl = yThrow * controlPitchFactor;
        pitch = pitchDueToPosition + pitchDueToControl;
        yaw = transform.localPosition.x * positionYawFactor;
        roll = xThrow * controlRollFactor;

        transform.localRotation = Quaternion.Euler(pitch, yaw, roll);
    }
    private void TransformEuclideanCoord()
    {
        // get player key press input 
        xThrow = movement.ReadValue<Vector2>().x;
        yThrow = movement.ReadValue<Vector2>().y;

        // calcuate offset for moving left to right and ensure 
        // player remains in camera FoV
        xOffset = xThrow * Time.deltaTime * controlSpeed;
        transformedXPos = transform.localPosition.x + xOffset;
        clampedXPos = Mathf.Clamp(transformedXPos, -xRange, xRange);

        // calcuate offset for moving up and down and ensure 
        // player remains in camera FoV
        yOffset = yThrow * Time.deltaTime * controlSpeed;
        transformedYPos = transform.localPosition.y + yOffset;
        clampedYPos = Mathf.Clamp(transformedYPos, -yRange, yRange);

        // move player position
        transform.localPosition = new Vector3(clampedXPos, clampedYPos);
    }
    private void ProcessFiring()
    {
        if (fire.IsPressed())
        {
            SetLaserToActive(true);
        }
        else
        {
            SetLaserToActive(false);
        }
    }

    private void SetLaserToActive(bool isActive)
    {
        foreach (GameObject laser in Lasers)
        {
            var emissionModule = laser.GetComponent<ParticleSystem>().emission;
            emissionModule.enabled = isActive;
        }
    }
}
