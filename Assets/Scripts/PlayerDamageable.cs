using UnityEngine;
using System;
using System.Collections;

[RequireComponent(typeof(PlayerScript), typeof(PlayerKillable))]
public class PlayerDamageable : Damageable
{
    protected PlayerScript player;

    float invulTimer = 0;

    [SerializeField] bool useDebug = false;

    protected override void Awake()
    {
        base.Awake();
        player = GetComponent<PlayerScript>();
        killable = GetComponent<PlayerKillable>();
    }

    protected void Update()
    {
        if (invulTimer > 0 && PauseManager.Instance.IsPaused == false)
        {
            invulTimer -= Time.deltaTime;

            if (useDebug == true)
            {
                Debug.Log(invulTimer);
            }
        }
    }

    public override void TakeDamage(int p_damage)
    {
        if (player.IsVulnerable == true)
        {
            return;
        }

        base.TakeDamage(p_damage);

        // Shake Camera
    }

    public virtual void TakeDamageWithInvul(int p_damage, float p_invulDuration = .5f)
    {
        if (player.IsVulnerable == true)
        {
            return;
        }

        TakeDamage(p_damage);

        StartCoroutine(TemporarilyInvulnerable(p_invulDuration));
    }

    IEnumerator TemporarilyInvulnerable(float p_duration)
    {
        player.IsVulnerable = true;

        invulTimer = p_duration;

        yield return new WaitUntil
            ( 
                () => invulTimer <= 0 
            );

        player.IsVulnerable = false;
    }
}
