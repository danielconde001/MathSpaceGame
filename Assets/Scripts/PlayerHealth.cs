using UnityEngine;

[RequireComponent(typeof(PlayerScript))]
public class PlayerHealth : Health
{
    PlayerScript player;

    private void Awake()
    {
        player = GetComponent<PlayerScript>();
    }

    public void AddHealth(int p_addedHealth)
    {
        value += p_addedHealth;

        if (value >= player.GetMaxHealth())
        {
            value = player.GetMaxHealth();
        }
    }
}
