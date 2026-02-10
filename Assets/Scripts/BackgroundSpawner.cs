using UnityEngine;

public class BackgroundSpawner : MonoBehaviour
{
    [SerializeField] private float ZOffset = 299.5f;

    private void OnTriggerEnter(Collider col)
    {
        if (col.gameObject.CompareTag("Player"))
        {
            SpawnNextArea();
        }
    }

    private void SpawnNextArea()
    {
        LevelManager.Instance.SpawnNextSection(ZOffset);
    }
}
