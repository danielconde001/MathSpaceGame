using UnityEngine;

public class SpaceshipMovement : MonoBehaviour
{
    [SerializeField] private float moveSpeed = 5f;

    [SerializeField] private float xLimit = 5f;
    [SerializeField] private float yLimit = 5f;

    void Update()
    {
        float rightInput = Input.GetAxis("Horizontal");
        float upInput = Input.GetAxis("Vertical");

        if (transform.localPosition.x <= -xLimit) 
            transform.localPosition = new Vector3(-xLimit, transform.localPosition.y, transform.localPosition.z);
        if (transform.localPosition.x >= xLimit)
            transform.localPosition = new Vector3(xLimit, transform.localPosition.y, transform.localPosition.z);
        if (transform.localPosition.y <= -yLimit)
            transform.localPosition = new Vector3(transform.localPosition.x, -yLimit, transform.localPosition.z);
        if (transform.localPosition.y >= yLimit)
            transform.localPosition = new Vector3(transform.localPosition.x, yLimit, transform.localPosition.z);

        Vector3 move = Vector3.ClampMagnitude(new Vector3(rightInput, upInput, 0), 1) * moveSpeed * Time.deltaTime;
        transform.localPosition += move;
        
        
    }
}
