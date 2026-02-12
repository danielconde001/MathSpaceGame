using UnityEngine;

public class OnBadRockDeath : MonoBehaviour
{
    [SerializeField] int damageOnDeath;

    SpawnVFXOnDeath SpawnVFXOnDeath;

    private void Awake()
    {
        SpawnVFXOnDeath = GetComponent<SpawnVFXOnDeath>();
    }

    public void OnDeath()
    {
        PlayerManager.Instance.GetPlayer().Damageable.TakeDamage(damageOnDeath);

        if (SpawnVFXOnDeath != null)
        {
            SpawnVFXOnDeath.SpawnVFX();
        }
    }
}
