using UnityEngine;

public class MoveForward : MonoBehaviour
{
    [SerializeField] private float speed = 5f;
    private bool stopMoving = false;

    void Update()
    {
        if (stopMoving == false)
        {
            transform.position += transform.forward * speed * Time.deltaTime;
        }
    }

    public void StopMoving()
    {
        stopMoving = true;
    }

    public void StartMoving()
    {
        stopMoving = false;
    }
}
