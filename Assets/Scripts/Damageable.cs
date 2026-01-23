using UnityEngine;

[RequireComponent(typeof(Health), typeof(Killable))]
public class Damageable : MonoBehaviour
{
    Health health;
    Killable killable;

    protected virtual void Awake()
    {
        health = GetComponent<Health>();
        killable = GetComponent<Killable>();
    }

    public virtual void TakeDamage(int p_damage)
    {
        health.value -= p_damage;
        if (health.value <= 0)
        {
            killable.Kill();
        }
    }
}
