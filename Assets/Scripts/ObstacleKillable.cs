using UnityEngine.Events;

public class ObstacleKillable : Killable
{
    public UnityEvent OnDeathEvent;
    DistanceToPlayer distanceToPlayer;

    private void Awake()
    {
        distanceToPlayer = GetComponent<DistanceToPlayer>();
    }

    public override void Kill()
    {
        base.Kill();

        OnDeathEvent.Invoke();

        PlayerVicinity.Instance.DistancesToPlayer.Remove(distanceToPlayer);

        Destroy(gameObject);
    }
}
