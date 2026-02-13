using UnityEngine;

public class MinigameTrigger : MonoBehaviour
{
    [SerializeField] MinigameManager minigame;

    bool isTriggered = false;

    private void OnTriggerEnter(Collider other)
    {
        if (isTriggered == true)
        {
            return;
        }

        if (other.CompareTag("Player") == false)
        { 
            return;
        }

        isTriggered = true;

        minigame.InitializeMinigame();
    }
}
