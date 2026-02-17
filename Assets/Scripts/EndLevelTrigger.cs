using System.Collections;
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

        StartCoroutine(EndLevelCoroutine());
    }

    IEnumerator EndLevelCoroutine()
    {
        DialogueManager.Instance.StartAutoDialogue("Congratulations! You did it. Now on to the next adventure..", 5f);

        LevelManager.Instance.StopSectionsFromMoving();

        yield return new WaitForSeconds(5.5f);

        int score =
            PlayerManager.Instance.GetPlayer().PlayerDeaths == 0 ? 100 :
            PlayerManager.Instance.GetPlayer().PlayerDeaths == 1 ? 50 : 0;

        GameEndManager.Instance.EndGame(score);
    }
}
