using UnityEngine;
using System;
using System.Collections;

[RequireComponent(typeof(PlayerScript), typeof(PlayerKillable))]
public class PlayerDamageable : Damageable
{
    protected PlayerScript player;

    float invulTimer = 0;

    [SerializeField] bool useDebug = false;

    CameraShake camShake;

    protected override void Awake()
    {
        base.Awake();
        player = GetComponent<PlayerScript>();
        killable = GetComponent<PlayerKillable>();
        camShake = Camera.main.GetComponent<CameraShake>();
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

        camShake.TriggerShake();
    }

    public virtual void TakeDamageWithInvul(int p_damage, float p_invulDuration = 2f)
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

        while (invulTimer > 0)
        {
            player.PlayerMesh.enabled = true;
            yield return new WaitForSeconds(0.1f);
            player.PlayerMesh.enabled = false;
            yield return new WaitForSeconds(0.1f);
        }

        player.PlayerMesh.enabled = true;

        player.IsVulnerable = false;
    }
}
