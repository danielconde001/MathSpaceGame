using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[Serializable]
public class Minigame
{
    public MinigameManager minigameManager;
    public uint Rounds;
}

public class MinigameSequencer : MonoBehaviour
{
    [SerializeField] private List<Minigame> minigames;
    public bool SequenceIsOngoing { get; private set; }

    public void StartSequence()
    {
        StartCoroutine(SequenceCoroutine());
    }

    private IEnumerator SequenceCoroutine()
    {
        SequenceIsOngoing = true;

        for (int i = 0; i < minigames.Count; ++i)
        {
            uint numberOfRounds = minigames[i].Rounds;
            minigames[i].minigameManager.InitializeMinigame(numberOfRounds);
            yield return new WaitUntil(() => minigames[i].minigameManager.Initialized == false);
        }

        SequenceIsOngoing = false;
    }
}
