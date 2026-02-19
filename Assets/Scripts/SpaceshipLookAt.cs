using UnityEngine;
using UnityEngine.EventSystems;

public class SpaceshipLookAt : MonoBehaviour
{
    [SerializeField] float rotationSpeed = 45f;
    private Vector3 lookAtPos;

    void Update()
    {
        if (PauseManager.Instance.IsPaused == true)
        {
            return;
        }

        if (Application.isMobilePlatform && LevelManager.Instance.LevelState != 1)
        {
            return;
        }

        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        lookAtPos = ray.GetPoint(100f);

        LookAtPositionSlerp(lookAtPos);
    }

    private void LookAtPositionSlerp(Vector3 p_lookAtPos)
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

    public void LookAtPositionFast(Vector3 p_lookAtPos)
    {
        if (p_lookAtPos != null)
        {
            Vector3 direction = (p_lookAtPos - transform.position).normalized;
            Quaternion lookRotation = Quaternion.LookRotation(direction);
            transform.localRotation = Quaternion.Slerp(transform.localRotation, lookRotation, Time.deltaTime * rotationSpeed*3);
        }
    }
}
