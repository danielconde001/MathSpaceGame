using UnityEngine;

public class SpawnVFXOnDeath : MonoBehaviour
{
    [SerializeField] GameObject VisualEffect;
    [SerializeField] Vector3 offset;
    [SerializeField] float offsetTowardsCamera = 3;
    [SerializeField] float scaleMultiplier = 2;

    public void SpawnVFX(Transform p_targetParent = null)
    {
        if (VisualEffect != null)
        {
            Vector3 directionToCamera = (Camera.main.transform.position - transform.position).normalized;
            
            GameObject obj 
                = Instantiate
                (
                    VisualEffect, 
                    transform.position + (directionToCamera * offsetTowardsCamera) + offset, 
                    Quaternion.identity
                );

            obj.transform.rotation = Quaternion.LookRotation(obj.transform.position - Camera.main.transform.position);
            obj.transform.localScale = transform.localScale * scaleMultiplier;

            if (p_targetParent != null)
            {
                obj.transform.SetParent(p_targetParent);
            }
        }
        else
        {
            Debug.LogWarning("Visual Effect is missing!", this);
        }
    }
}
