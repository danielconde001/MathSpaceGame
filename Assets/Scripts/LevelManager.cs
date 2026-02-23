using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class LevelManager : MonoBehaviour
{
    private static LevelManager instance;
    public static LevelManager Instance
    {

        get
        {
            if (instance == null)
            {
                GameObject newGameObject = new GameObject("LevelManager");
                instance = newGameObject.AddComponent<LevelManager>();
            }
            return instance;
        }
    }

    [SerializeField] private List<LevelSection> collectSectionPrefabs; // change to list later
    [SerializeField] private List<LevelSection> combatSectionPrefabs; // change to list later
    [SerializeField] private LevelSection minigameSectionPrefab;
    [SerializeField] private LevelSection endSectionPrefab; // change to list later

    [SerializeField] private bool resetScoreOnLevelStart = true;

    List<LevelSection> remainingCollectSections = new List<LevelSection>();
    uint timing = 1;
    uint laps = 0;

    private List<LevelSection> currentSections = new List<LevelSection>();
    public List<LevelSection> GetCurrentSections()
    {
        return currentSections;
    }

    private uint levelState = 0; 
    // 0 = Normal
    // 1 = UI Related Minigames / Power Up Screen
    // 2 = Doing Minigame 1
    // 3 = Game Over Screen

    public uint LevelState
    {
        get => levelState;
        set => levelState = value;
    }

    private void Awake()
    {
        instance = this;

        if (currentSections.Count <= 0)
        {
            LevelSection section = FindAnyObjectByType<LevelSection>();
            currentSections.Add(section);
        }

        remainingCollectSections = collectSectionPrefabs;
    }

    private void Start()
    {
        if (resetScoreOnLevelStart == true)
        {
            ScoreManager.Instance?.ResetScore();
        }
    }

    public void SpawnNextSection(float p_offset = 0f)
    {
        Vector3 nextSectionPosition = (transform.forward * p_offset);
        LevelSection newObj = null;

        if (timing == 1)
        {
            newObj = Instantiate(combatSectionPrefabs[(int)laps], nextSectionPosition, Quaternion.identity);
        }
        else if (timing == 2)
        {
            newObj = Instantiate(minigameSectionPrefab, nextSectionPosition, Quaternion.identity);
            laps++;
        }
        else
        {
            if (laps >= 3)
            {
                // spawn last section
                newObj = Instantiate(endSectionPrefab, nextSectionPosition, Quaternion.identity);
            }
            else
            {
                int index = Random.Range(0, remainingCollectSections.Count);
                newObj = Instantiate(remainingCollectSections[index], nextSectionPosition, Quaternion.identity);
                remainingCollectSections.RemoveAt(index);
            }
        }

        currentSections.Add(newObj);

        timing++;

        if (timing > 2)
        {
            timing = 0;
        }
    }

    public void StopSectionsFromMoving()
    {
        for (int i = 0; i < currentSections.Count; i++)
        {
            currentSections[i].StopMovement();
        }
    }

    public void StartSectionsMovement()
    {
        for (int i = 0; i < currentSections.Count; i++)
        {
            currentSections[i].StartMovement();
        }
    }
}
