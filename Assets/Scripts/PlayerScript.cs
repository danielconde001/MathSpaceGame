using UnityEngine;

public class PlayerScript : MonoBehaviour
{
    private SpaceshipAttack attack;
    private SpaceshipMovement movement;
    private Collider playerCollider;
    private PlayerHealth health;
    private int maxHealth;

    public SpaceshipAttack GetAttackScript() { return attack; }
    public SpaceshipMovement GetMovementScript() { return movement; }
    public Collider PlayerCollider { get => playerCollider; }
    public PlayerHealth Health { get => health; }

    private void Awake()
    {
        attack = GetComponent<SpaceshipAttack>();
        movement = GetComponent<SpaceshipMovement>();
        playerCollider = GetComponent<Collider>();
        health = GetComponent<PlayerHealth>();
        maxHealth = Health.value;
    }

    public int GetDamage()
    {
        return attack.GetDamage();
    }

    public int GetMaxHealth()
    {
        return maxHealth;
    }
    public void AddMaxHealth(int p_addedValue)
    {
        maxHealth += p_addedValue;
    }
}
