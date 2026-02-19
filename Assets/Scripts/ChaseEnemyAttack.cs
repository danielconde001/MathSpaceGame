using UnityEngine;

public class ChaseEnemyAttack : MonoBehaviour
{
    public int damage;
    ChaseEnemyAI owner;
    EnemyKillable enemyKillable;
    SpawnVFXOnDeath SpawnVFXOnDeath;

    private void Awake()
    {
        owner = GetComponentInParent<ChaseEnemyAI>();
        enemyKillable = owner.GetComponent<EnemyKillable>();
    }

    private void OnCollisionEnter(Collision other)
    {
        PlayerDamageable hit;
        if (other.gameObject.TryGetComponent(out hit))
        {
            hit.TakeDamageWithInvul(damage);
            enemyKillable.Kill();
        }
    }
}
