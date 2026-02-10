using UnityEngine;

public class MinigameTrigger : MonoBehaviour
{
    [SerializeField] MinigameManager minigame;

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player") == false)
        { 
            return;
        }

        minigame.InitializeMinigame();
    }
}
