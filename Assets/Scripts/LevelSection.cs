using UnityEngine;
using System.Collections.Generic;
using System.Linq;

[RequireComponent(typeof(MoveForward))]
public class LevelSection : MonoBehaviour
{
    [SerializeField] private float zPositionMax;

    MoveForward moveForward;

    List<DistanceToPlayer> distanceToPlayers = new List<DistanceToPlayer>();

    private void Awake()
    {
        moveForward = GetComponent<MoveForward>();
        distanceToPlayers = GetComponentsInChildren<DistanceToPlayer>().ToList();
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
        for (int i = 0; i < distanceToPlayers.Count; i++)
        {
            PlayerVicinity.Instance.DistancesToPlayer.Remove(distanceToPlayers[i]);
        }
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
