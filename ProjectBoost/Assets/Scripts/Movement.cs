using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Movement : MonoBehaviour
{
    // PARAMETERS - for tuning, typically set in editor
    // CACHE - e.g. references for readability and speed
    // STATE - private instance(member) variables
    [SerializeField] float _thrust = 1000.0f;
    [SerializeField] float _rotation = 100.0f;
    [SerializeField] AudioClip _mainEngine;

    Rigidbody _movementRigidBody;
    AudioSource _audio;

    void Start()
    {
        _movementRigidBody = GetComponent<Rigidbody>();
        _audio = GetComponent<AudioSource>();
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
            _movementRigidBody.AddRelativeForce(Vector3.up * _thrust * Time.deltaTime);
            if (!_audio.isPlaying)
            {
                _audio.PlayOneShot(_mainEngine);
            }
        }
        else
        {
            _audio.Stop();
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
        _movementRigidBody.freezeRotation = true;
        transform.Rotate(Vector3.forward * rotationThisFrame * Time.deltaTime);
        _movementRigidBody.freezeRotation = false;
    }


}
