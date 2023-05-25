using UnityEngine;

public class Enemy : MonoBehaviour
{
    [SerializeField] GameObject deathVfx;
    [SerializeField] GameObject hitVfx;
    [SerializeField] int scorePerHit = 10;
    [SerializeField] int hitPoints = 2;
    ScoreBoard scoreBoard;
    Rigidbody _rigBody;
    GameObject _parentGameObject;
    private void Start()
    {
        scoreBoard = FindObjectOfType<ScoreBoard>();
        _parentGameObject = GameObject.FindWithTag("SpawnAtRuntime");
        AddRigidBody();
    }
    private void AddRigidBody()
    {
        _rigBody = gameObject.AddComponent<Rigidbody>();
        _rigBody.useGravity = false;
    }
    private void OnParticleCollision(GameObject other)
    {
        ProcessHit();
        if (hitPoints < 1)
        {
            KillEnemy();
        }
    }
    private void ProcessHit()
    {
        GameObject vfx = Instantiate(hitVfx, transform.position, Quaternion.identity);
        vfx.transform.parent = _parentGameObject.transform;
        hitPoints--;
        scoreBoard.IncreaseScore(scorePerHit);
    }
    private void KillEnemy()
    {
        GameObject vfx = Instantiate(deathVfx, transform.position, Quaternion.identity);
        vfx.transform.parent = _parentGameObject.transform;
        Destroy(gameObject);
    }
}
