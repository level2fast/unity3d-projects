using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Movement : MonoBehaviour
{
    Rigidbody movementRigidBody;
    [SerializeField] float _thrust = 1000.0f;
    [SerializeField] float _rotation = 100.0f;
    // Start is called before the first frame update
    void Start()
    {
        movementRigidBody = GetComponent<Rigidbody>();
    }

    // Update is called once per frame
    void Update()
    {
        ProcessRotation();
        ProcessThrust();
    }

    void ProcessThrust()
    {
        if (Input.GetKey(KeyCode.Space))
        {
            movementRigidBody.AddRelativeForce(Vector3.up * _thrust * Time.deltaTime);
        }
    }
    void ProcessRotation()
    {
        if (Input.GetKey(KeyCode.A))
        {
            ApplyRotation(_rotation);
        }
        else if (Input.GetKey(KeyCode.D))
        {
            ApplyRotation(-_rotation);
        }
    }

    void ApplyRotation(float rotationThisFrame)
    {
        // This stops the player from rotating so we can
        // manually rotate
        movementRigidBody.freezeRotation = true;
        transform.Rotate(Vector3.forward * rotationThisFrame * Time.deltaTime);
        movementRigidBody.freezeRotation = false;
    }


}
