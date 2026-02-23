using UnityEngine;
using UnityEngine.EventSystems;

public class SpaceshipLookAt : MonoBehaviour
{
    [SerializeField] private float rotationSpeed = 45f;
    public float RotationSpeed { get => rotationSpeed; }
    private Vector3 lookAtPos;

    [SerializeField] bool testingForMobile; // Enable only when you are testing as if you're on mobile

    void Update()
    {
        if (PauseManager.Instance.IsPaused == true)
        {
            return;
        }

        if (testingForMobile == true)
        {
            if (LevelManager.Instance.LevelState != 2)
            {
                return;
            }
        }

        else if (Application.isMobilePlatform && LevelManager.Instance.LevelState != 2)
        {
            return;
        }

        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        lookAtPos = ray.GetPoint(100f);

        LookAtPosition(lookAtPos);
    }

    public void LookAtPosition(Vector3 p_lookAtPos)
    {
        if (p_lookAtPos != null)
        {
            // Calculate the direction from the current object to the target
            Vector3 direction = (p_lookAtPos - transform.position).normalized;

            // Create the rotation needed to look in that direction
            Quaternion lookRotation = Quaternion.LookRotation(direction);

            // Smoothly rotate the object towards that rotation over time
            transform.localRotation = Quaternion.Slerp(transform.localRotation, lookRotation, Time.deltaTime * rotationSpeed);
        }
    }
}
