using UnityEngine;

public class CombatEventTrigger : MonoBehaviour
{
    [SerializeField] int CombatEventID = -1;

    bool isTriggered = false;

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Player") && isTriggered == false)
        {
            TriggerCombatEvent(CombatEventID);
            isTriggered = true;
        }
    }

    private void TriggerCombatEvent(int p_eventID)
    {
        CombatEventManager.Instance.InitializeCombatEvent(p_eventID);
    }
}
