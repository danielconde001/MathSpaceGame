using UnityEngine;

[RequireComponent(typeof(PlayerScript))]
public class PlayerKillable : Killable
{
    PlayerScript player;

    SpawnVFXOnDeath SpawnVFXOnDeath;

    private void Awake()
    {
        player = GetComponent<PlayerScript>();

        SpawnVFXOnDeath = GetComponent<SpawnVFXOnDeath>();
    }

    public override void Kill()
    {
        base.Kill();

        if (SpawnVFXOnDeath != null)
        {
            SpawnVFXOnDeath.SpawnVFX();
        }

        player.PlayerMesh.enabled = false;

        // explode player

        AudioManager.Instance.PlayPlayerDeathSFX();

        GameOverManager.Instance.ShowScreen();
    }
}
