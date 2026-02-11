using UnityEngine;

public class EnemyProjectileBehaviour : ProjectileBehaviour
{
    public int damage;

    protected override void OnTriggerEnter(Collider other)
    {
        PlayerDamageable hit;
        if (other.gameObject.TryGetComponent(out hit))
        {   
            hit.TakeDamage(damage);
        }

        Destroy(gameObject);
    }
}
