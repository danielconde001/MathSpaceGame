using UnityEngine;

public class EnemyKillable : Killable
{
    public override void Kill()
    {
        base.Kill();

        // spawn explosion

        Destroy(gameObject);
    }
}
