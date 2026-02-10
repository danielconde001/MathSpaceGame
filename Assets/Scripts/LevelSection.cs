using UnityEngine;

[RequireComponent(typeof(MoveForward))]
public class LevelSection : MonoBehaviour
{
    [SerializeField] float secondsBeforeRemoval = 30f;
    MoveForward moveForward;

    private void Awake()
    {
        moveForward = GetComponent<MoveForward>();
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
