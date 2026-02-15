using UnityEngine;

public class MinigameSequencerTrigger : MonoBehaviour
{
    [SerializeField] MinigameSequencer sequencer;

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

        sequencer.StartSequence();
    }
}
