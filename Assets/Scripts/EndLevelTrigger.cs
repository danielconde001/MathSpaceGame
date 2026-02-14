using UnityEngine;

public class EndLevelTrigger : MonoBehaviour
{
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

        DialogueManager.Instance.StartAutoDialogue("Congratulations! You did it. Now on to the next adventure..", 5f);

        // Spawn End Screen
    }
}
