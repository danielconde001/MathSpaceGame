using System.Collections.Generic;
using UnityEngine;

public class DialogueTrigger : MonoBehaviour
{
    [TextArea(3, 10)]
    public List<string> dialogueLines = new List<string>();

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            TriggerDialogue();
        }
    }

    public void TriggerDialogue()
    {
        Dialogue dialogue = new Dialogue();
        foreach (var line in dialogueLines)
        {
            dialogue.dialogueLines.Add(new DialogueLine { line = line });
        }
        DialogueManager.Instance.StartDialogue(dialogue);
    }
}
