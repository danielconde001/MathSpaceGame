using UnityEngine;

public class BackgroundSpawner : MonoBehaviour
{
    [SerializeField] private GameObject nextAreaPrefab;
    [SerializeField] private float spawnOffset = 299.5f;

    private float timeElapsed = 0f;

    private void OnTriggerEnter(Collider col)
    {
        if (col.gameObject.CompareTag("Player"))
        {
            SpawnNextArea();
        }
    }

    private void SpawnNextArea()
    {
        Vector3 nextAreaPosition = (transform.forward * spawnOffset);
        Instantiate(nextAreaPrefab, nextAreaPosition, Quaternion.identity);
    }
}
