using UnityEngine.Events;

public class ObstacleKillable : Killable
{
    public UnityEvent OnDeathEvent;

    public override void Kill()
    {
        base.Kill();

        // Explode

        OnDeathEvent.Invoke();

        //temp
        Destroy(gameObject);
    }
}
