using DG.Tweening;
using UnityEngine;

public class GiantEnemyAI : MonoBehaviour
{
    [SerializeField] float rotationSpeed;
    [SerializeField] float moveSpeed;
    [SerializeField] int damage;
    [HideInInspector] public Transform respectiveSpot;

    [SerializeField] MissileBehaviour missilePrefab;
    [SerializeField] Transform[] bulletSpawns;
    public uint consecutiveFires;
    public float longCooldown;
    public float fireRate;
    public Health health;

    AudioSource audioSource;

    uint currentFires = 0;
    float cooldown = 0;
    bool isNowInSpot = false;
    int spawnIndex = 0;

    private void Awake()
    {
        audioSource = GetComponent<AudioSource>();
        health = GetComponent<Health>();
    }

    void Start()
    {
        respectiveSpot = EnemySpotManager.Instance.GetEnemySpots(4);
        GoToSpot();
    }

    void Update()
    {
        if (PauseManager.Instance.IsPaused == true)
        {
            return;
        }

        else if (isNowInSpot == true)
        {
            if (PauseManager.Instance.IsPaused == false)
            {
                cooldown -= Time.deltaTime;
            }

            if (cooldown <= 0)
            {
                Shoot();
            }
        }
    }

    void GoToSpot()
    {
        transform.DOMove(respectiveSpot.position, 5f)
            .OnComplete(
            () =>
            {
                isNowInSpot = true;
            });
    }

    void Shoot()
    {
        AudioManager.Instance.PlayMissileLaunchSFX(audioSource);

        MissileBehaviour launchedMissile
            = Instantiate(missilePrefab, bulletSpawns[spawnIndex].position, transform.rotation);

        spawnIndex++;

        if (spawnIndex >= bulletSpawns.Length) spawnIndex = 0;
        
        currentFires++;

        if (currentFires >= consecutiveFires)
        {
            currentFires = 0;
            cooldown = longCooldown;
        }
        else
        {
            cooldown = fireRate;
        }
    }
}
