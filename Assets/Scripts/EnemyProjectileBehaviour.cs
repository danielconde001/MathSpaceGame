using UnityEngine;

public class EnemyProjectileBehaviour : ProjectileBehaviour
{
    protected override void OnTriggerEnter(Collider other)
    {
        Damageable hit;
        if (other.gameObject.TryGetComponent(out hit))
        {

        }

        Destroy(gameObject);
    }
}
