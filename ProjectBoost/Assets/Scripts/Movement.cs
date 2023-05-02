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

    [SerializeField] ParticleSystem _mainBoosterParticles;
    [SerializeField] ParticleSystem _leftBoosterParticles;
    [SerializeField] ParticleSystem _rightBoosterParticles;

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

    void ProcessThrust()
    {
        if (Input.GetKey(KeyCode.Space))
        {
            PlayAudio();
            MoveRocket();
            StartMainThruster();
        }
        else if (Input.GetKey(KeyCode.A))
        {
            PlayAudio();
            MoveRocket();
            StartRightThruster();
        }
        else if (Input.GetKey(KeyCode.D))
        {
            PlayAudio();
            MoveRocket();
            StartLeftThruster();
        }
        else
        {
            StopThrusting();
        }
    }

    private void StartLeftThruster()
    {
        if (!_leftBoosterParticles.isPlaying)
        {
            _leftBoosterParticles.Play();
        }
    }

    private void StartRightThruster()
    {
        if (!_rightBoosterParticles.isPlaying)
        {
            _rightBoosterParticles.Play();
        }
    }

    private void StartMainThruster()
    {
        if (!_mainBoosterParticles.isPlaying)
        {
            _mainBoosterParticles.Play();
        }
    }

    private void StopThrusting()
    {
        _audio.Stop();
        _mainBoosterParticles.Stop();
        _leftBoosterParticles.Stop();
        _rightBoosterParticles.Stop();
    }

    void PlayAudio()
    {
        if (!_audio.isPlaying)
        {
            _audio.PlayOneShot(_mainEngine);
        }
    }
    void MoveRocket()
    {
        _movementRigidBody.AddRelativeForce(Vector3.up * _thrust * Time.deltaTime);
    }

}
