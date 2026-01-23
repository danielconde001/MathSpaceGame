using UnityEngine;

[RequireComponent(typeof(PlayerScript))]
public class PlayerKillable : Killable
{
    PlayerScript player;

    private void Awake()
    {
        player = GetComponent<PlayerScript>();
    }

    public override void Kill()
    {
        base.Kill();

        print("You dead boi!");

        // explode player
        // game over screen
    }
}
