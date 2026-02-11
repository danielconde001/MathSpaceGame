using System.Collections;
using UnityEngine;
using UnityEngine.LightTransport;

public class CombatEventManager : MonoBehaviour
{
    private static CombatEventManager instance;
    public static CombatEventManager Instance
    {

        get
        {
            if (instance == null)
            {
                GameObject newGameObject = new GameObject("CombatEventManager");
                instance = newGameObject.AddComponent<CombatEventManager>();
            }
            return instance;
        }
    }

    [SerializeField] GameObject stationaryEnemyPrefab;
    [SerializeField] GameObject chaseEnemyPrefab;

    private void Awake()
    {
        instance = this;
    }

    public void InitializeCombatEvent(int p_eventID = -1)
    {
        switch (p_eventID)
        {
            case 0:
                StartCoroutine(CombatEvent1A());
                break;
            case 1:
                CombatEvent1B();
                break;
            case 2:
                CombatEvent1C();
                break;
            case 3:
                CombatEvent2A();
                break;
            case 4:
                CombatEvent2B();
                break;
            case 5:
                CombatEvent2C();
                break;
            case 6:
                CombatEvent3A();
                break;
            case 7:
                CombatEvent3B();
                break;
            case 8:
                CombatEvent3C();
                break;
            default:
                Debug.LogWarning("No Combat Event was triggered. No existing ID was set.");
                break;
        }
    }

    private IEnumerator CombatEvent1A()
    {
        LevelManager.Instance.StopSectionsFromMoving();

        StationaryEnemyAI enemy1 = SpawnStationaryEnemy(2, 0);
        yield return new WaitForSeconds(.5f);
        StationaryEnemyAI enemy2 = SpawnStationaryEnemy(3, 3);
        yield return new WaitUntil( () => (enemy1 == null && enemy2 == null)); // unitl they are dead

        StationaryEnemyAI enemy3 = SpawnStationaryEnemy(2, 0);
        yield return new WaitForSeconds(.5f);
        StationaryEnemyAI enemy4 = SpawnStationaryEnemy(3, 3);

        yield return new WaitUntil(() => (enemy3 == null && enemy4 == null)); // unitl they are dead

        LevelManager.Instance.StartSectionsMovement();
    }

    private void CombatEvent1B()
    {

    }

    private void CombatEvent1C()
    {

    }

    private void CombatEvent2A()
    {

    }

    private void CombatEvent2B()
    {

    }

    private void CombatEvent2C()
    {

    }

    private void CombatEvent3A()
    {

    }

    private void CombatEvent3B()
    {

    }

    private void CombatEvent3C()
    {

    }

    private StationaryEnemyAI SpawnStationaryEnemy(int p_spawnPointIndex, int p_enemySpotIndex)
    {
        StationaryEnemyAI stationaryEnemy = null;

        stationaryEnemy =
            Instantiate
            (
                stationaryEnemyPrefab,
                SpawnPointManager.Instance.GetSpawnPoints(p_spawnPointIndex).position,
                Quaternion.Euler(0, 90, 0)
            ).GetComponent<StationaryEnemyAI>();

        stationaryEnemy.respectiveSpot = EnemySpotManager.Instance.GetEnemySpots(p_enemySpotIndex);

        return stationaryEnemy;
    }
}
