using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
public class PlayerControls : MonoBehaviour
{
    [SerializeField] InputAction movement;
    [SerializeField] InputAction fire;
    [SerializeField] float controlSpeed = 10.0f;
    [SerializeField] float xRange = 5.0f;
    [SerializeField] float yRange = 5.0f;
    float xThrow;
    float yThrow;

    float xOffset;
    float transformedXPos;
    float yOffset;
    float transformedYPos;
    float clampedXPos;
    float clampedYPos;
    // Start is called before the first frame update
    void Start()
    {

    }

    private void OnEnable()
    {
        movement.Enable();
    }

    private void OnDisable()
    {
        movement.Disable();
    }

    // Update is called once per frame
    void Update()
    {
        TransformEuclideanCoord();
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
}
