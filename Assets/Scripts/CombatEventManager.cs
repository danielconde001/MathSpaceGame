using System.Collections;
using System.Collections.Generic;
using UnityEngine;

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

    float waitForSeconds = 0f;

    private void Awake()
    {
        instance = this;
    }

    private void Update()
    {
        if (PauseManager.Instance.IsPaused == true)
        {
            return;
        }

        if (waitForSeconds > 0f)
        {
            waitForSeconds -= Time.deltaTime;
        }
    }

    public void InitializeCombatEvent(int p_eventID = -1)
    {
        switch (p_eventID)
        {
            case 0:
                StartCoroutine(CombatEvent1A());
                break;
            case 1:
                StartCoroutine(CombatEvent1B());
                break;
            case 2:
                StartCoroutine(CombatEvent1C());
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

        AudioManager.Instance.PlayEnemyAlarmSFX();
        AudioManager.Instance.PlayEnemyFlyInSFX();

        DialogueManager.Instance.StartAutoDialogue("Get ready! Enemy incoming.");

        StationaryEnemyAI enemy1 = SpawnStationaryEnemy(2, 0);
        yield return new WaitUntil( 
            () => (
            enemy1 == null)); // unitl they are dead

        AudioManager.Instance.PlayEnemyFlyInSFX();

        DialogueManager.Instance.StartAutoDialogue("There's two of them! Watch your back.");

        StationaryEnemyAI enemy2 = SpawnStationaryEnemy(2, 0);
        waitForSeconds = 0.5f;
        yield return new WaitWhile( () => waitForSeconds > 0f);
        StationaryEnemyAI enemy3 = SpawnStationaryEnemy(3, 3);

        yield return new WaitUntil(
            () => (
            enemy2 == null && 
            enemy3 == null)); // unitl they are dead

        DialogueManager.Instance.StartAutoDialogue("You took 'em down! Well Done.");

        LevelManager.Instance.StartSectionsMovement();
    }

    private IEnumerator CombatEvent1B()
    {
        LevelManager.Instance.StopSectionsFromMoving();

        AudioManager.Instance.PlayEnemyAlarmSFX();
        AudioManager.Instance.PlayEnemyFlyInSFX();

        DialogueManager.Instance.StartAutoDialogue("Here comes more of them!");

        StationaryEnemyAI enemy1 = SpawnStationaryEnemy(2, 0);
        waitForSeconds = 0.5f;
        yield return new WaitWhile(() => waitForSeconds > 0f);
        StationaryEnemyAI enemy2 = SpawnStationaryEnemy(3, 3);
        yield return new WaitUntil(
            () => (
            enemy1 == null && 
            enemy2 == null)); // unitl they are dead

        AudioManager.Instance.PlayEnemyFlyInSFX();

        DialogueManager.Instance.StartAutoDialogue("There's 3 of them now! Careful.");

        StationaryEnemyAI enemy3 = SpawnStationaryEnemy(2, 0);
        waitForSeconds = 0.5f;
        yield return new WaitWhile(() => waitForSeconds > 0f);
        StationaryEnemyAI enemy4 = SpawnStationaryEnemy(3, 3);
        waitForSeconds = 0.5f;
        yield return new WaitWhile(() => waitForSeconds > 0f);
        StationaryEnemyAI enemy5 = SpawnStationaryEnemy(2, 1);

        yield return new WaitUntil(
            () => (
            enemy3 == null && 
            enemy4 == null && 
            enemy5 == null)); // unitl they are dead

        DialogueManager.Instance.StartAutoDialogue("Awesome! You're getting the hang of it.");

        LevelManager.Instance.StartSectionsMovement();
    }

    private IEnumerator CombatEvent1C()
    {
        LevelManager.Instance.StopSectionsFromMoving();

        AudioManager.Instance.PlayEnemyAlarmSFX();
        AudioManager.Instance.PlayEnemyFlyInSFX();

        StationaryEnemyAI enemy1 = SpawnStationaryEnemy(2, 0);
        waitForSeconds = 0.5f;
        yield return new WaitWhile(() => waitForSeconds > 0f);
        StationaryEnemyAI enemy2 = SpawnStationaryEnemy(3, 3);
        waitForSeconds = 0.5f;
        yield return new WaitWhile(() => waitForSeconds > 0f);
        StationaryEnemyAI enemy3 = SpawnStationaryEnemy(2, 1);
        yield return new WaitUntil(
            () => (
            enemy1 == null && 
            enemy2 == null && 
            enemy3 == null)); // unitl they are dead

        AudioManager.Instance.PlayEnemyFlyInSFX();

        DialogueManager.Instance.StartAutoDialogue("That's a lot ships...");

        StationaryEnemyAI enemy4 = SpawnStationaryEnemy(2, 0);
        waitForSeconds = 0.5f;
        yield return new WaitWhile(() => waitForSeconds > 0f);
        StationaryEnemyAI enemy5 = SpawnStationaryEnemy(3, 3);
        waitForSeconds = 0.5f;
        yield return new WaitWhile(() => waitForSeconds > 0f);
        StationaryEnemyAI enemy6 = SpawnStationaryEnemy(2, 1);
        waitForSeconds = 0.5f;
        yield return new WaitWhile(() => waitForSeconds > 0f);
        StationaryEnemyAI enemy7 = SpawnStationaryEnemy(2, 2);

        yield return new WaitUntil(
            () => (
            enemy4 == null && 
            enemy5 == null && 
            enemy6 == null && 
            enemy7 == null)); // unitl they are dead

        DialogueManager.Instance.StartAutoDialogue("You are awesome! They stood no chance.");

        LevelManager.Instance.StartSectionsMovement();
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
