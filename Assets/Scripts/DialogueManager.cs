using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine;
using TMPro;

[System.Serializable]
public class DialogueLine
{
    [TextArea(3, 10)]
    public string line;
}

[System.Serializable]
public class Dialogue
{
    public List<DialogueLine> dialogueLines = new List<DialogueLine>();
}
public class DialogueManager : MonoBehaviour
{
    public static DialogueManager Instance;

    public TextMeshProUGUI dialogueArea;
    public Button nextButton;

    private Queue<DialogueLine> lines;
    
    [HideInInspector] public bool isDialogueActive = false;

    public float typingSpeed = 0.2f;

    public Animator animator;

    private void Awake()
    {
        if (Instance == null)
            Instance = this;

        lines = new Queue<DialogueLine>();
    }

    public void StartDialogue(Dialogue dialogue)
    {
        isDialogueActive = true;
        animator.Play("show");
        if (nextButton != null)
            nextButton.gameObject.SetActive(true);
        lines.Clear();
        foreach (DialogueLine dialogueLine in dialogue.dialogueLines)
        {
            lines.Enqueue(dialogueLine);
        }
        DisplayNextDialogueLine();
    }

    public void StartAutoDialogue(string line, float duration = 3f)
    {
        Dialogue dialogue = new Dialogue();
        dialogue.dialogueLines = new List<DialogueLine>();
        DialogueLine dialogueLine = new DialogueLine();
        dialogueLine.line = line;
        dialogue.dialogueLines.Add(dialogueLine);
        StartDialogue(dialogue);

        Invoke("EndDialogue", duration);
    }

    public void DisplayNextDialogueLine()
    {
        if (lines.Count == 0)
        {
            EndDialogue();
            return;
        }

        DialogueLine currentLine = lines.Dequeue();

        StopAllCoroutines();

        StartCoroutine(TypeSentence(currentLine));
    }

    IEnumerator TypeSentence(DialogueLine dialogueLine)
    {
        dialogueArea.text = "";
        foreach (char letter in dialogueLine.line.ToCharArray())
        {
            dialogueArea.text += letter;
            yield return new WaitForSeconds(typingSpeed);
        }
    }

    void EndDialogue()
    {
        isDialogueActive = false;
        animator.Play("hide");
        if (nextButton != null)
            nextButton.gameObject.SetActive(false);
    }
}