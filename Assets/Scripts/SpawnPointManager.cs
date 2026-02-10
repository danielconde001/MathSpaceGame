using UnityEngine;
using System.Collections.Generic;

public class SpawnPointManager : MonoBehaviour
{
    private static SpawnPointManager instance;
    public static SpawnPointManager Instance
    {

        get
        {
            if (instance == null)
            {
                GameObject newGameObject = new GameObject("SpawnPointManager");
                instance = newGameObject.AddComponent<SpawnPointManager>();
            }
            return instance;
        }
    }

    [SerializeField] private List<Transform> spawnPoints;

    public Transform GetSpawnPoints(int p_index)
    {
        if (p_index < 0)
        {
            Debug.LogWarning("Spawn index set to -1. Nothing happens.");
            return null;
        }
        if (spawnPoints == null)
        {
            Debug.LogWarning("No spawn points are set.");
        }

        return spawnPoints[p_index];
    }

    private void Awake()
    {
        instance = this;
    }
}
