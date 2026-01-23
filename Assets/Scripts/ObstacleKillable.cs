using UnityEngine;

public class ObstacleKillable : Killable
{
    public override void Kill()
    {
        base.Kill();
        
        // Explode

        //temp
        Destroy(gameObject);
    }
}
