using UnityEngine;
using UnityEngine.SceneManagement;
public class CollisionHandler : MonoBehaviour
{
    [SerializeField] float _loadDelay = 1.0f;
    [SerializeField] ParticleSystem _explosionVfx;
    public Transform[] _transfromObj;

    private void OnTriggerEnter(Collider other)
    {
        StartCrashSequence();
    }

    private void StartCrashSequence()
    {

        _transfromObj = GetComponentsInChildren<Transform>();

        foreach (Transform transform in _transfromObj)
        {
            if (transform.name.Contains("Collider") && transform.name != "Collider")
            {
                transform.gameObject.GetComponent<BoxCollider>().enabled = false;
            }
        }

        _explosionVfx.Play();
        GetComponent<MeshRenderer>().enabled = false;
        GetComponent<PlayerControls>().enabled = false;
        Invoke("ReloadLevel", _loadDelay);
    }
    void ReloadLevel()
    {
        int currentSceneIndex = SceneManager.GetActiveScene().buildIndex;
        SceneManager.LoadScene(currentSceneIndex);
    }


}
