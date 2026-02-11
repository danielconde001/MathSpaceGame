using UnityEngine;

public class StationaryEnemyAI : MonoBehaviour
{
    [SerializeField] float rotationSpeed;
    [SerializeField] float moveSpeed;
    [HideInInspector] public Transform respectiveSpot;

    [SerializeField] float minFireRate;
    [SerializeField] float maxFireRate;

    [SerializeField] Projectile projectilePrefab;

    Transform target;
    float cooldown = 0;
    bool isNowInSpot = false;

    void Start()
    {
        cooldown = Random.Range(minFireRate, maxFireRate);
        target = PlayerManager.Instance.GetPlayer().transform;
    }

    void Update()
    {
        if (isNowInSpot == false)
        {
            LookAtTarget(respectiveSpot.position);
            GoToSpot();
        }

        else if (isNowInSpot == true)
        {
            LookAtTarget(target.position);
            cooldown -= Time.deltaTime;
            if (cooldown <= 0)
            {
                Shoot();
            }
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

    void GoToSpot()
    {
        float distToSpot = Vector3.Distance(transform.position, respectiveSpot.position);

        MoveStraight();

        if (distToSpot < .15f)
        {
            isNowInSpot = true;
        }
    }

    void Shoot()
    {
        cooldown = Random.Range(minFireRate, maxFireRate);
        //Instantiate(projectilePrefab, );
    }

    void MoveStraight()
    {
        transform.position += transform.forward * moveSpeed * Time.deltaTime;
    }
}
