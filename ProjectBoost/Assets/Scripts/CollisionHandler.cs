using System;
using UnityEngine;
using UnityEngine.SceneManagement;
public class CollisionHandler : MonoBehaviour
{
    [SerializeField] float _levelLoadDelay = 1.0f;
    [SerializeField] AudioClip _finishedLevel;
    [SerializeField] AudioClip _crash;
    [SerializeField] ParticleSystem _finishedParticles;
    [SerializeField] ParticleSystem _crashParticles;

    AudioSource _audio;
    bool _isTransitioning = false; // represents state between collided and collision
    void Start()
    {
        _audio = GetComponent<AudioSource>();
    }

    private void OnCollisionEnter(Collision other)
    {
        if (_isTransitioning)
            return;

        switch (other.gameObject.tag)
        {
            case "Fuel":
                Debug.Log("Collided with fuel");
                break;
            case "Friendly":
                Debug.Log("Collided with friendly");
                break;
            case "Finish":
                StartSuccessSequence();
                break;
            default:
                Debug.Log("Hit default statement");
                StartCrashSequence();
                break;
        }
    }

    void StartSuccessSequence()
    {
        _isTransitioning = true;
        _audio.Stop();
        // play success sfx 
        _audio.PlayOneShot(_finishedLevel);
        // play particle finish effect
        _finishedParticles.Play();
        GetComponent<Movement>().enabled = false;
        Invoke("LoadNextLevel", _levelLoadDelay);

    }

    void StartCrashSequence()
    {
        _isTransitioning = true;
        _audio.Stop();
        // play crash sfx
        _audio.PlayOneShot(_crash);
        // play particle crash effect
        _crashParticles.Play();
        GetComponent<Movement>().enabled = false;
        Invoke("ReloadLevel", _levelLoadDelay);

    }
    void LoadNextLevel()
    {

        int currentSceneIndex = SceneManager.GetActiveScene().buildIndex;
        int nextSceneIndex = currentSceneIndex + 1;
        if (nextSceneIndex == SceneManager.sceneCountInBuildSettings)
        {
            nextSceneIndex = 0;
        }
        SceneManager.LoadScene(nextSceneIndex);
    }

    void ReloadLevel()
    {
        // reload the scene
        int currentSceneIndex = SceneManager.GetActiveScene().buildIndex;
        SceneManager.LoadScene(currentSceneIndex);
    }
}
