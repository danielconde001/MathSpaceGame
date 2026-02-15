using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

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
    Count
}

public class MinigameSequencer : MonoBehaviour
{
    [SerializeField] private List<Minigame> minigames;
    public bool SequenceIsOngoing { get; private set; }

    TensAndOnesMinigameManager tensAndOnesMinigame;
    ArrangingMinigameManager arrangingMinigame;
    FillinMinigameManager fillinMinigame;


    private void Awake()
    {
        tensAndOnesMinigame = FindAnyObjectByType<TensAndOnesMinigameManager>();
        arrangingMinigame = FindAnyObjectByType<ArrangingMinigameManager>();
        fillinMinigame = FindAnyObjectByType<FillinMinigameManager>();
    }
    public void StartSequence()
    {
        StartCoroutine(SequenceCoroutine());
    }

    private IEnumerator SequenceCoroutine()
    {
        SequenceIsOngoing = true;

        for (int i = 0; i < minigames.Count; ++i)
        {
            MinigameManager minigame;

            if (minigames[i].Type == MinigameType.TensAndOnes)
            {
                minigame = tensAndOnesMinigame;
            }
            else if (minigames[i].Type == MinigameType.Arranging)
            {
                minigame = arrangingMinigame;
            }
            else
            {
                minigame = fillinMinigame;
            }

            uint numberOfRounds = minigames[i].Rounds;
            minigame.InitializeMinigame(numberOfRounds);
            yield return new WaitUntil(() => minigame.Initialized == false);
        }

        SequenceIsOngoing = false;
    }
}
