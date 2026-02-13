using UnityEngine;

public class PlayerScript : MonoBehaviour
{
    [SerializeField] private GameObject playerMesh;

    private SpaceshipAttack attack;
    private SpaceshipMovement movement;
    private Collider playerCollider;
    private PlayerHealth health;
    private PlayerDamageable damageable;
    private int maxHealth;
    private bool isInvulnerable = false;
    private uint playerLevel = 1;
    private uint maxLevel = 4;

    public SpaceshipAttack GetAttackScript() { return attack; }
    public SpaceshipMovement GetMovementScript() { return movement; }
    public Collider PlayerCollider { get => playerCollider; }
    public PlayerHealth Health { get => health; }
    public PlayerDamageable Damageable { get => damageable; }
    public GameObject PlayerMesh { get => playerMesh; }
    public bool IsVulnerable 
    { 
        get => isInvulnerable; 
        set => isInvulnerable = value; 
    }
    public uint PlayerLevel 
    { 
        get => playerLevel; 
    }

    private void Awake()
    {
        attack = GetComponent<SpaceshipAttack>();
        movement = GetComponent<SpaceshipMovement>();
        playerCollider = GetComponent<Collider>();
        health = GetComponent<PlayerHealth>();
        maxHealth = Health.value;
        damageable = GetComponent<PlayerDamageable>();

        if (playerMesh == null)
        {
            playerMesh = transform.GetChild(3).GetChild(0).gameObject;
        }
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

    public void Revive()
    {
        health.value = maxHealth;

        // Spawn Revive VFX

        PlayerMesh.SetActive(true);
    }

    public void LevelUpPlayer()
    {
        if (playerLevel >= maxLevel)
        {
            return;
        }

        playerLevel++;
    }
}
