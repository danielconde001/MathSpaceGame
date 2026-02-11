using System.Collections.Generic;
using UnityEngine;

public class EnemySpotManager : MonoBehaviour
{
    private static EnemySpotManager instance;
    public static EnemySpotManager Instance
    {

        get
        {
            if (instance == null)
            {
                GameObject newGameObject = new GameObject("EnemySpotManager");
                instance = newGameObject.AddComponent<EnemySpotManager>();
            }
            return instance;
        }
    }

    [SerializeField] private List<Transform> enemySpots;

    public Transform GetEnemySpots(int p_index)
    {
        if (p_index < 0)
        {
            Debug.LogWarning("Enemy Spot index set to -1. Nothing happens.");
            return null;
        }
        if (enemySpots == null)
        {
            Debug.LogWarning("No Enemy Spots are set.");
        }

        return enemySpots[p_index];
    }

    private void Awake()
    {
        instance = this;
    }
}
