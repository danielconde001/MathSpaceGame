using UnityEngine;

public class SpawnVFXOnDeath : MonoBehaviour
{
    [SerializeField] GameObject VisualEffect;

    public void SpawnVFX()
    {
        if (VisualEffect != null)
        {
            Instantiate(VisualEffect, transform.position, Quaternion.identity);
        }
        else
        {
            Debug.LogWarning("Visual Effect is missing!", this);
        }
    }
}
