using System.Collections;
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
    [SerializeField] GameObject giantEnemyPrefab;

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
                StartCoroutine(CombatEvent2A());
                break;
            case 4:
                StartCoroutine(CombatEvent2B());
                break;
            case 5:
                StartCoroutine(CombatEvent2C());
                break;
            case 6:
                StartCoroutine(CombatEvent3A());
                break;
            case 7:
                StartCoroutine(CombatEvent3B());
                break;
            case 8:
                StartCoroutine(CombatEvent3C());
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

    private IEnumerator CombatEvent2A()
    {
        LevelManager.Instance.StopSectionsFromMoving();

        AudioManager.Instance.PlayEnemyAlarmSFX();

        DialogueManager.Instance.StartAutoDialogue("Swarm incoming!");

        float interval = 0.4f;
        float waveInterval = 0.8f;

        // WAVE 1
        AudioManager.Instance.PlayEnemyFlyInSFX();

        for (int i = 0; i < 10; i++)
        {
            SpawnChaseEnemy(0);
            
            waitForSeconds = interval;
            yield return new WaitWhile(() => waitForSeconds > 0f);
        }
        waitForSeconds = waveInterval;
        yield return new WaitWhile(() => waitForSeconds > 0f);

        // WAVE 2
        AudioManager.Instance.PlayEnemyFlyInSFX();

        for (int i = 0; i < 10; i++)
        {
            SpawnChaseEnemy(5);

            waitForSeconds = interval;
            yield return new WaitWhile(() => waitForSeconds > 0f);
        }
        waitForSeconds = waveInterval;
        yield return new WaitWhile(() => waitForSeconds > 0f);

        // WAVE 3
        AudioManager.Instance.PlayEnemyFlyInSFX();

        for (int i = 0; i < 10; i++)
        {
            SpawnChaseEnemy(4);

            waitForSeconds = interval;
            yield return new WaitWhile(() => waitForSeconds > 0f);
        }
        waitForSeconds = waveInterval;
        yield return new WaitWhile(() => waitForSeconds > 0f);

        // WAVE 4
        AudioManager.Instance.PlayEnemyFlyInSFX();

        for (int i = 0; i < 10; i++)
        {
            SpawnChaseEnemy(1);
            waitForSeconds = interval;
            yield return new WaitWhile(() => waitForSeconds > 0f);
        }
        waitForSeconds = waveInterval;
        yield return new WaitWhile(() => waitForSeconds > 0f);

        StationaryEnemyAI sEnemy1 = SpawnStationaryEnemy(2, 0);
        waitForSeconds = 0.5f;
        yield return new WaitWhile(() => waitForSeconds > 0f);

        StationaryEnemyAI sEnemy2 = SpawnStationaryEnemy(3, 3);

        yield return new WaitUntil(
            () => (
            sEnemy1 == null &&
            sEnemy2 == null)); // unitl they are dead

        LevelManager.Instance.StartSectionsMovement();
    }

    private IEnumerator CombatEvent2B()
    {
        LevelManager.Instance.StopSectionsFromMoving();

        AudioManager.Instance.PlayEnemyAlarmSFX();

        DialogueManager.Instance.StartAutoDialogue("Swarm incoming!");

        float interval = 0.4f;
        float waveInterval = 0f;

        // WAVE 1
        AudioManager.Instance.PlayEnemyFlyInSFX();

        for (int i = 0; i < 10; i++)
        {
            SpawnChaseEnemy(1);

            waitForSeconds = interval;
            yield return new WaitWhile(() => waitForSeconds > 0f);
        }
        waitForSeconds = waveInterval;
        yield return new WaitWhile(() => waitForSeconds > 0f);

        // WAVE 2
        AudioManager.Instance.PlayEnemyFlyInSFX();

        for (int i = 0; i < 10; i++)
        {
            SpawnChaseEnemy(4);

            waitForSeconds = interval;
            yield return new WaitWhile(() => waitForSeconds > 0f);
        }
        waitForSeconds = waveInterval;
        yield return new WaitWhile(() => waitForSeconds > 0f);

        // WAVE 3
        AudioManager.Instance.PlayEnemyFlyInSFX();

        for (int i = 0; i < 10; i++)
        {
            SpawnChaseEnemy(5);

            waitForSeconds = interval;
            yield return new WaitWhile(() => waitForSeconds > 0f);
        }
        waitForSeconds = waveInterval;
        yield return new WaitWhile(() => waitForSeconds > 0f);

        // WAVE 4
        AudioManager.Instance.PlayEnemyFlyInSFX();

        for (int i = 0; i < 10; i++)
        {
            SpawnChaseEnemy(0);
            waitForSeconds = interval;
            yield return new WaitWhile(() => waitForSeconds > 0f);
        }
        waitForSeconds = waveInterval;
        yield return new WaitWhile(() => waitForSeconds > 0f);

        AudioManager.Instance.PlayEnemyFlyInSFX();

        StationaryEnemyAI sEnemy1 = SpawnStationaryEnemy(2, 0);
        waitForSeconds = 0.5f;
        yield return new WaitWhile(() => waitForSeconds > 0f);

        StationaryEnemyAI sEnemy2 = SpawnStationaryEnemy(3, 3);
        waitForSeconds = 0.5f;
        yield return new WaitWhile(() => waitForSeconds > 0f);

        StationaryEnemyAI sEnemy3 = SpawnStationaryEnemy(2, 1);

        yield return new WaitUntil(
            () => (
            sEnemy1 == null &&
            sEnemy2 == null &&
            sEnemy3 == null)); // unitl they are dead

        LevelManager.Instance.StartSectionsMovement();
    }

    private IEnumerator CombatEvent2C()
    {
        LevelManager.Instance.StopSectionsFromMoving();

        AudioManager.Instance.PlayEnemyAlarmSFX();

        DialogueManager.Instance.StartAutoDialogue("Swarm incoming!");

        float interval = 0.4f;
        float waveInterval = 0f;

        // WAVE 1
        AudioManager.Instance.PlayEnemyFlyInSFX();

        for (int i = 0; i < 10; i++)
        {
            SpawnChaseEnemy(0);

            if (i == 9) SpawnChaseEnemy(4);

            waitForSeconds = interval;
            yield return new WaitWhile(() => waitForSeconds > 0f);
        }
        waitForSeconds = waveInterval;
        yield return new WaitWhile(() => waitForSeconds > 0f);

        // WAVE 2
        AudioManager.Instance.PlayEnemyFlyInSFX();

        for (int i = 0; i < 9; i++)
        {
            SpawnChaseEnemy(4);

            if (i == 8) SpawnChaseEnemy(1);

            waitForSeconds = interval;
            yield return new WaitWhile(() => waitForSeconds > 0f);
        }
        waitForSeconds = waveInterval;
        yield return new WaitWhile(() => waitForSeconds > 0f);

        // WAVE 3
        AudioManager.Instance.PlayEnemyFlyInSFX();

        for (int i = 0; i < 9; i++)
        {
            SpawnChaseEnemy(1);

            if (i == 8) SpawnChaseEnemy(5);

            waitForSeconds = interval;
            yield return new WaitWhile(() => waitForSeconds > 0f);
        }
        waitForSeconds = waveInterval;
        yield return new WaitWhile(() => waitForSeconds > 0f);

        // WAVE 4
        AudioManager.Instance.PlayEnemyFlyInSFX();

        for (int i = 0; i < 9; i++)
        {
            SpawnChaseEnemy(5);
            waitForSeconds = interval;
            yield return new WaitWhile(() => waitForSeconds > 0f);
        }
        waitForSeconds = waveInterval;
        yield return new WaitWhile(() => waitForSeconds > 0f);

        StationaryEnemyAI sEnemy1 = SpawnStationaryEnemy(2, 0);
        waitForSeconds = 0.5f;
        yield return new WaitWhile(() => waitForSeconds > 0f);

        StationaryEnemyAI sEnemy2 = SpawnStationaryEnemy(3, 3);
        waitForSeconds = 0.5f;
        yield return new WaitWhile(() => waitForSeconds > 0f);

        StationaryEnemyAI sEnemy3 = SpawnStationaryEnemy(2, 1);
        waitForSeconds = 0.5f;
        yield return new WaitWhile(() => waitForSeconds > 0f);

        StationaryEnemyAI sEnemy4 = SpawnStationaryEnemy(3, 2);

        yield return new WaitUntil(
            () => (
            sEnemy1 == null &&
            sEnemy2 == null &&
            sEnemy3 == null &&
            sEnemy4 == null)); // unitl they are dead

        LevelManager.Instance.StartSectionsMovement();
    }

    private IEnumerator CombatEvent3A()
    {
        LevelManager.Instance.StopSectionsFromMoving();

        AudioManager.Instance.PlayEnemyAlarmSFX();
        AudioManager.Instance.PlayEnemyFlyInSFX();

        DialogueManager.Instance.StartAutoDialogue("That's a BIG one!");

        GiantEnemyAI enemy = SpawnGiantEnemy(6);

        yield return new WaitUntil(() => (enemy == null));

        DialogueManager.Instance.StartAutoDialogue("Something tells me that's not the last one..");

        LevelManager.Instance.StartSectionsMovement();
    }

    private IEnumerator CombatEvent3B()
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

        StationaryEnemyAI enemy4 = SpawnStationaryEnemy(2, 0);
        waitForSeconds = 0.5f;
        yield return new WaitWhile(() => waitForSeconds > 0f);
        StationaryEnemyAI enemy5 = SpawnStationaryEnemy(3, 3);
        waitForSeconds = 0.5f;
        yield return new WaitWhile(() => waitForSeconds > 0f);
        StationaryEnemyAI enemy6 = SpawnStationaryEnemy(2, 2);
        yield return new WaitUntil(
            () => (
            enemy4 == null &&
            enemy5 == null &&
            enemy6 == null));

        DialogueManager.Instance.StartAutoDialogue("We're almost to the end!");

        LevelManager.Instance.StartSectionsMovement();
    }

    private IEnumerator CombatEvent3C()
    {
        LevelManager.Instance.StopSectionsFromMoving();

        AudioManager.Instance.PlayEnemyAlarmSFX();
        AudioManager.Instance.PlayEnemyFlyInSFX();

        StationaryEnemyAI enemy1 = SpawnStationaryEnemy(2, 0);
        waitForSeconds = 0.5f;
        yield return new WaitWhile(() => waitForSeconds > 0f);
        StationaryEnemyAI enemy2 = SpawnStationaryEnemy(3, 3);

        waitForSeconds = 3f;
        yield return new WaitWhile(() => waitForSeconds > 0f);

        AudioManager.Instance.PlayEnemyAlarmSFX();
        AudioManager.Instance.PlayEnemyFlyInSFX();

        GiantEnemyAI enemy3 = SpawnGiantEnemy(6);
        enemy3.health.value = 4000;
        enemy3.consecutiveFires = 12;
        enemy3.longCooldown = 4f;
        enemy3.fireRate = .1f;

        yield return new WaitUntil(() => 
        (
            enemy1 == null && 
            enemy2 == null && 
            enemy3 == null)
        );

        DialogueManager.Instance.StartAutoDialogue("WOOHOO! Nothing can stop you.");

        LevelManager.Instance.StartSectionsMovement();
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

    private ChaseEnemyAI SpawnChaseEnemy(int p_spawnPointIndex)
    {
        ChaseEnemyAI chaseEnemy = null;

        chaseEnemy =
            Instantiate
            (
                chaseEnemyPrefab,
                SpawnPointManager.Instance.GetSpawnPoints(p_spawnPointIndex).position,
                Quaternion.Euler(0, 90, 0)
            ).GetComponent<ChaseEnemyAI>();

        return chaseEnemy;
    }

    private GiantEnemyAI SpawnGiantEnemy(int p_spawnPointIndex)
    {
        GiantEnemyAI giantEnemy = null;

        giantEnemy =
            Instantiate
            (
                giantEnemyPrefab,
                SpawnPointManager.Instance.GetSpawnPoints(p_spawnPointIndex).position,
                Quaternion.Euler(0, 180, 0)
            ).GetComponent<GiantEnemyAI>();

        return giantEnemy;
    }
}
