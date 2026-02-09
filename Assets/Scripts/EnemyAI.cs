using UnityEngine;

public class EnemyAI : MonoBehaviour
{
    bool wentNearPlayer = false;
    Transform target;
    [SerializeField] float rotationSpeed;
    [SerializeField] float moveSpeed;

    private void Start()
    {
        target = PlayerManager.Instance.GetPlayer().transform;
    }


    private void Update()
    {
        float distToPlayer = Vector3.Distance(transform.position, target.position);

        if (distToPlayer < 5f && wentNearPlayer == false) 
        {
            wentNearPlayer = true;
        }

        if (wentNearPlayer == false)
        {
            LookAtPlayer();
            MoveStraight();
        }
        else
        {
            MoveStraight();
        }
    }

    void LookAtPlayer()
    {
        if (target != null)
        {
            // Calculate the direction from the current object to the target
            Vector3 direction = (target.position - transform.position).normalized;

            // Create the rotation needed to look in that direction
            Quaternion lookRotation = Quaternion.LookRotation(direction);

            // Smoothly rotate the object towards that rotation over time
            transform.rotation = Quaternion.Slerp(transform.rotation, lookRotation, Time.deltaTime * rotationSpeed);
        }
    }

    void MoveStraight()
    {
        transform.position += transform.forward * moveSpeed * Time.deltaTime;
    }
}
