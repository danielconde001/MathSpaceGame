using UnityEngine;

public class PlayerScript : MonoBehaviour
{
    private SpaceshipAttack attack;
    private SpaceshipMovement movement;
    private Collider playerCollider;

    public Collider PlayerCollider { get => playerCollider; }

    private void Awake()
    {
        attack = GetComponent<SpaceshipAttack>();
        movement = GetComponent<SpaceshipMovement>();
        playerCollider = GetComponent<Collider>();
    }

    public int GetDamage()
    {
        return attack.GetDamage();
    }
}
