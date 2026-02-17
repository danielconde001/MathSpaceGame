using UnityEngine;

public class PlayerScript : MonoBehaviour
{
    [SerializeField] private MeshRenderer playerMesh;
    [SerializeField] private AudioSource shootAudioSource;
    [SerializeField] private AudioSource spaceshipAmbienceAudioSource;

    private SpaceshipAttack attack;
    private SpaceshipMovement movement;
    private Collider playerCollider;
    private PlayerHealth health;
    private HealthBar playerHealthBar;
    private PlayerDamageable damageable;
    private int maxHealth;
    private bool isInvulnerable = false;
    private uint playerLevel = 1;
    private uint maxLevel = 4;

    public SpaceshipAttack GetAttackScript() { return attack; }
    public SpaceshipMovement GetMovementScript() { return movement; }
    public Collider PlayerCollider { get => playerCollider; }
    public PlayerHealth Health { get => health; }
    public HealthBar PlayerHealthBar { get => playerHealthBar; }
    public PlayerDamageable Damageable { get => damageable; }
    public AudioSource ShootAudioSource { get => shootAudioSource; }
    public AudioSource SpaceshipAmbienceAudioSource { get => spaceshipAmbienceAudioSource; }
    public MeshRenderer PlayerMesh { get => playerMesh; }
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
        playerHealthBar = GetPlayerHealthBar();

        if (shootAudioSource == null)
        {
            shootAudioSource 
                = transform.Find("ShootAudioSource").GetComponent<AudioSource>();
        }

        if (spaceshipAmbienceAudioSource == null)
        {
            spaceshipAmbienceAudioSource 
                = transform.Find("SpaceshipAmbienceAudioSource").GetComponent<AudioSource>();
        }

        if (playerMesh == null)
        {
            playerMesh 
                = transform.Find("MeshParent").GetChild(0).gameObject.GetComponent<MeshRenderer>();
        }
    }

    private void Start()
    {
        AudioManager.Instance.PlaySpaceshipAmbienceSFX();
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

        PlayerMesh.enabled = true;
    }

    public void LevelUpPlayer()
    {
        if (playerLevel >= maxLevel)
        {
            return;
        }

        if (playerLevel <= 0)
        {
            Debug.LogWarning("Player's Level is currently valued at 0 or less, which is invalid! Player should be set to 1 by default!");
            Debug.LogWarning("Player's Level is now set to 1.");
            playerLevel = 1;
        }

        playerLevel++;
    }

    private HealthBar GetPlayerHealthBar()
    {
        HealthBar healthBar = GameObject.Find("PlayerHealthCanvas")?.GetComponent<HealthBar>();
        
        if (healthBar == null)
        {
            GameObject playerHealthCanvas 
                = Instantiate(Resources.Load<GameObject>("PlayerHealthCanvas"));
            healthBar = playerHealthCanvas.GetComponent<HealthBar>(); 
        }

        return healthBar;
    }
}
