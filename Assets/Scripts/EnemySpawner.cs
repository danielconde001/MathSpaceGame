using System.Collections;
using UnityEngine;

public class EnemySpawner : MonoBehaviour
{
    [SerializeField] bool isTimedTrigger = false;
    [SerializeField] int spawnPointIndex = -1;
    [SerializeField] ChaseEnemyAI enemyPrefab;
    [SerializeField] float timeBeforeSpawn;
    [SerializeField] float spawnRate = 0.3f;
    [SerializeField] int numberOfSpawns = 7;

    bool spawnHasStarted = false;
    float timer;

    private void Start()
    {
        timer = timeBeforeSpawn;
    }

    private void Update()
    {
        if (isTimedTrigger && spawnHasStarted == false)
        {
            timer -= Time.deltaTime;
            if (timer <= 0)
            {
                Spawn();
            }
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player") == false)
        {
            return;
        }

        if (spawnHasStarted == false && isTimedTrigger == false)
        {
            Debug.Log("Triggered");
            Spawn();
        }
    }

    void Spawn()
    {
        spawnHasStarted = true;

        StartCoroutine(SpawnCoroutine());
    }

    IEnumerator SpawnCoroutine()
    {
        Transform spawnTransform = SpawnPointManager.Instance.GetSpawnPoints(spawnPointIndex);

        for (int i = 0; i < numberOfSpawns; i++)
        {
            ChaseEnemyAI eai = Instantiate(enemyPrefab, spawnTransform.position, Quaternion.identity);
            eai.transform.SetParent(null);
            yield return new WaitForSeconds(spawnRate);
        }
    }
    
}
