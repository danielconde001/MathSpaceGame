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
    [SerializeField] uint consecutiveFires;
    [SerializeField] float longCooldown;
    [SerializeField] float fireRate;

    AudioSource audioSource;

    uint currentFires = 0;
    float cooldown = 0;
    bool isNowInSpot = false;

    private void Awake()
    {
        audioSource = GetComponent<AudioSource>();
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

    void Shoot(int p_index = 0)
    {
        AudioManager.Instance.PlayMissileLaunchSFX(audioSource);

        MissileBehaviour launchedMissile
            = Instantiate(missilePrefab, bulletSpawns[p_index].position, transform.rotation);

        currentFires++;

        if (currentFires >= consecutiveFires)
        {
            consecutiveFires = 0;
            cooldown = longCooldown;
        }
        else
        {
            cooldown = fireRate;
        }
    }
}
