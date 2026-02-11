using UnityEngine;

public class CombatEventTrigger : MonoBehaviour
{
    [SerializeField] int CombatEventID = -1;

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            TriggerCombatEvent(CombatEventID);
        }
    }

    private void TriggerCombatEvent(int p_eventID)
    {
        CombatEventManager.Instance.InitializeCombatEvent(p_eventID);
    }

    
}
