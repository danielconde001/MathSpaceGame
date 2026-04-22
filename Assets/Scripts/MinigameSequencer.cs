using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static UnityEngine.Rendering.DebugUI;

[Serializable]
public class Minigame
{
    public MinigameType Type;
    public uint Rounds;
}

public enum MinigameType
{
    TensAndOnes,
    Arranging,
    FillIn,
    TensAndOnesUIVersion,
    Count
}

public class MinigameSequencer : MonoBehaviour
{
    [SerializeField] private List<Minigame> Minigames;
    public bool SequenceIsOngoing { get; private set; }

    TensAndOnesMinigameManager tensAndOnesMinigame;
    TensAndOnesMinigameUIVersionManager tensAndOnesUIVersionMinigame;
    ArrangingMinigameManager arrangingMinigame;
    FillinMinigameManager fillinMinigame;

    [SerializeField] bool stopSectionsOnSequenceStart = true;
    [SerializeField] bool moveSectionsOnSequenceEnd = true;

    private void Awake()
    {
        if (FindAnyObjectByType<TensAndOnesMinigameManager>() == false)
        {
            GameObject taoMngGameObj = Resources.Load<GameObject>("Minigames/TensAndOneMinigameCanvas");
            tensAndOnesMinigame = Instantiate(taoMngGameObj).GetComponent<TensAndOnesMinigameManager>();
        }

        if (FindAnyObjectByType<TensAndOnesMinigameUIVersionManager>() == false)
        {
            GameObject taoMngUiVerGameObj = Resources.Load<GameObject>("Minigames/TensAndOneMinigameCanvas (UI Version)");
            tensAndOnesUIVersionMinigame = Instantiate(taoMngUiVerGameObj).GetComponent<TensAndOnesMinigameUIVersionManager>();
        }

        if (FindAnyObjectByType<ArrangingMinigameManager>() == false)
        {
            GameObject arrangingMngGameObj = Resources.Load<GameObject>("Minigames/ArrangingMinigameCanvas");
            arrangingMinigame = Instantiate(arrangingMngGameObj).GetComponent<ArrangingMinigameManager>();
        }

        if (FindAnyObjectByType<FillinMinigameManager>() == false)
        {
            GameObject fillinMngGameObj = Resources.Load<GameObject>("Minigames/FillinMinigameCanvas");
            fillinMinigame = Instantiate(fillinMngGameObj).GetComponent<FillinMinigameManager>();
        }
    }

    public void StartSequence()
    {
        StartCoroutine(SequenceCoroutine());
    }

    private IEnumerator SequenceCoroutine()
    {
        SequenceIsOngoing = true;

        if (stopSectionsOnSequenceStart == true)
        {
            LevelManager.Instance.StopSectionsFromMoving();
        }

        List<MinigameManager> minigamesLocalList = new List<MinigameManager>();

        for (int i = 0; i < Minigames.Count; i++)
        {
            switch (Minigames[i].Type)
            {
                case MinigameType.TensAndOnes:
                    {
                        if (tensAndOnesMinigame == null)
                        {
                            tensAndOnesMinigame = FindAnyObjectByType<TensAndOnesMinigameManager>();
                        }
                        minigamesLocalList.Add(tensAndOnesMinigame);
                        break;
                    }

                case MinigameType.Arranging:
                    {
                        if (arrangingMinigame == null)
                        {
                            arrangingMinigame = FindAnyObjectByType<ArrangingMinigameManager>();
                        }
                        minigamesLocalList.Add(arrangingMinigame);
                        break;
                    }

                case MinigameType.FillIn:
                    {
                        if (fillinMinigame == null)
                        {
                            fillinMinigame = FindAnyObjectByType<FillinMinigameManager>();
                        }
                        minigamesLocalList.Add(fillinMinigame);
                        break;
                    }
                case MinigameType.TensAndOnesUIVersion:
                    {
                        if (tensAndOnesUIVersionMinigame == null)
                        {
                            tensAndOnesUIVersionMinigame = FindAnyObjectByType<TensAndOnesMinigameUIVersionManager>();
                        }
                        minigamesLocalList.Add(tensAndOnesUIVersionMinigame);
                        break;
                    }

                default:
                    Debug.LogWarning("No valid Minigame Type assigned to Index: " + i, this);
                    break;
            }
        }

        for (int i = 0; i < minigamesLocalList.Count; i++)
        {
            uint numberOfRounds = Minigames[i].Rounds;
            minigamesLocalList[i].InitializeMinigame(numberOfRounds);

            yield return new WaitUntil(() => minigamesLocalList[i].Initialized == false);
        }

        if (moveSectionsOnSequenceEnd)
        {
            LevelManager.Instance.StartSectionsMovement();
        }

        SequenceIsOngoing = false;
    }
}
