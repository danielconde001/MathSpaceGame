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

    [SerializeField] private LevelSection nextSectionPrefab; // change to list later
    [SerializeField] private LevelSection combatSectionPrefab; // change to list later

    uint sectionsSpawned = 1;

    [SerializeField] private List<LevelSection> currentSections = new List<LevelSection>();
    [HideInInspector] public List<LevelSection> GetCurrentSections()
    {
        return currentSections;
    }

    private uint levelState = 0; // 0 = normal, 1 = minigame 1
    public uint LevelState
    {
        get => levelState;
        set => levelState = value;
    }

    //private uint gemsCollected = 0;
    public void CollectGems(uint p_value)
    {
        //gemsCollected += p_value;
        ScoreManager.Instance.AddScore((int)p_value);
    }

    private void Awake()
    {
        instance = this;
    }

    public void SpawnNextSection(float p_offset = 0f)
    {
        Vector3 nextSectionPosition = (transform.forward * p_offset);
        LevelSection newObj = null;

        if (((sectionsSpawned + 1) % 2) == 0)
        {
            newObj = Instantiate(combatSectionPrefab, nextSectionPosition, Quaternion.identity);
        }
        else
        {
            newObj = Instantiate(nextSectionPrefab, nextSectionPosition, Quaternion.identity);
        }

        currentSections.Add(newObj);

        sectionsSpawned++;
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
