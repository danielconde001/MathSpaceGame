using UnityEngine;

public class SpaceshipLookAt : MonoBehaviour
{
    [SerializeField] float rotationSpeed = 45f;
    Vector3 lookAtPos;

    void Update()
    {
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        lookAtPos = ray.GetPoint(100f);

        if (lookAtPos != null)
        {
            // Calculate the direction from the current object to the target
            Vector3 direction = (lookAtPos - transform.position).normalized;

            // Create the rotation needed to look in that direction
            Quaternion lookRotation = Quaternion.LookRotation(direction);

            // Smoothly rotate the object towards that rotation over time
            transform.localRotation = Quaternion.Slerp(transform.localRotation, lookRotation, Time.deltaTime * rotationSpeed);
        }
    }
}
