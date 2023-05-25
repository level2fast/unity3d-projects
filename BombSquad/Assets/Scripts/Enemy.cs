using UnityEngine;

public class Enemy : MonoBehaviour
{
    [SerializeField] GameObject deathVfx;
    [SerializeField] GameObject hitVfx;
    [SerializeField] Transform parent;
    [SerializeField] int scorePerHit = 10;
    [SerializeField] int hitPoints = 2;
    ScoreBoard scoreBoard;

    private void Start()
    {
        scoreBoard = FindObjectOfType<ScoreBoard>();
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
        vfx.transform.parent = parent;
        hitPoints--;
        scoreBoard.IncreaseScore(scorePerHit);
    }
    private void KillEnemy()
    {
        GameObject vfx = Instantiate(deathVfx, transform.position, Quaternion.identity);
        vfx.transform.parent = parent;
        Destroy(gameObject);
    }
}
