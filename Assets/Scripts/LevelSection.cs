using UnityEngine;

[RequireComponent(typeof(MoveForward))]
public class LevelSection : MonoBehaviour
{
    [SerializeField] private float zPositionMax;

    MoveForward moveForward;

    private void Awake()
    {
        moveForward = GetComponent<MoveForward>();
    }

    private void Update()
    {
        if (transform.position.z <= zPositionMax)
        {
            RemoveFromLevel();
        }
    }

    private void RemoveFromLevel()
    {
        LevelManager.Instance.GetCurrentSections().Remove(this);
        Destroy(gameObject);
    }

    public void StopMovement()
    {
        moveForward.StopMoving();
    }

    public void StartMovement()
    {
        moveForward.StartMoving();
    }
}
