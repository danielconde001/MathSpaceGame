using UnityEngine;
using System;
using System.Collections;

[RequireComponent(typeof(PlayerScript), typeof(PlayerKillable))]
public class PlayerDamageable : Damageable
{
    protected PlayerScript player;

    [SerializeField] float invulDuration = 4f;

    protected override void Awake()
    {
        base.Awake();
        player = GetComponent<PlayerScript>();
        killable = GetComponent<PlayerKillable>();
    }

    public override void TakeDamage(int p_damage)
    {
        base.TakeDamage(p_damage);

        if (killable.IsKilled() == false)
        {
            // Shake camera

            // player becomes in vlunerable for a while
            StartCoroutine(TemporarilyInvulnerable());
        }
    }

    IEnumerator TemporarilyInvulnerable()
    {
        player.PlayerCollider.enabled = false;
        yield return new WaitForSeconds(invulDuration);

        // Become transparent

        player.PlayerCollider.enabled = true;
    }
}
