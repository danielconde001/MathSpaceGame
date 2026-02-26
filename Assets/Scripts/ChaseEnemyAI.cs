using UnityEngine;

public class ChaseEnemyAI : MonoBehaviour
{
    [SerializeField] bool skipFlyingIntro = false;
    [SerializeField] float rotationSpeed;
    [SerializeField] float moveSpeed;
    [SerializeField] Vector3 introSpotOffset;
    [SerializeField] float timeBeforeKilled = 8f;

    bool isNowFollowingPlayer = false;
    bool wentNearPlayer = false;
    bool wentNearIntroSpot = false;
    Vector3 introSpot;
    Transform target;
    EnemyKillable enemyKillable;
    float killTimer = 0;

    private void Awake()
    {
        enemyKillable = GetComponent<EnemyKillable>();
    }

    private void Start()
    {
        if (skipFlyingIntro == false)
        {
            introSpot = transform.position + introSpotOffset;
        }

        else if (skipFlyingIntro == true)
        {
            isNowFollowingPlayer = true;
        }

        target = PlayerManager.Instance.GetPlayer().transform;

        killTimer = timeBeforeKilled;
    }

    private void Update()
    {
        if (PauseManager.Instance.IsPaused == false)
        {
            killTimer -= Time.deltaTime;
        }

        if (killTimer <= 0)
        {
            enemyKillable.Kill();
        }
            
        if (isNowFollowingPlayer == false)
        {
            FlyingIntro();
        }

        else if (isNowFollowingPlayer == true)
        { 
            FollowPlayer(); 
        }
    }

    void FlyingIntro()
    {
        float distToIntroSpot = Vector3.Distance(transform.position, introSpot);

        if (distToIntroSpot < 1f && wentNearIntroSpot == false)
        {
            wentNearIntroSpot = true;
        }

        if (wentNearIntroSpot == false)
        {
            LookAtTarget(introSpot);
            MoveStraight();
        }
        else
        {
            isNowFollowingPlayer = true;
        }
    }

    void FollowPlayer()
    {
        float distToPlayer = Vector3.Distance(transform.position, target.position);

        if (distToPlayer < 5f && wentNearPlayer == false)
        {
            wentNearPlayer = true;
        }

        if (wentNearPlayer == false)
        {
            LookAtTarget(target.position);
            MoveStraight();
        }
        else
        {
            MoveStraight();
        }
    }

    void LookAtTarget(Vector3 p_target)
    {
        if (p_target != null)
        {
            // Calculate the direction from the current object to the target
            Vector3 direction = (p_target - transform.position).normalized;

            // Create the rotation needed to look in that direction
            Quaternion lookRotation = Quaternion.LookRotation(direction);

            // Smoothly rotate the object towards that rotation over time
            transform.rotation = Quaternion.Slerp(transform.rotation, lookRotation, Time.deltaTime * rotationSpeed);
        }
    }

    void MoveStraight()
    {
        if (PauseManager.Instance.IsPaused == false)
        {
            transform.position += transform.forward * moveSpeed * Time.deltaTime;
        }
    }
}
