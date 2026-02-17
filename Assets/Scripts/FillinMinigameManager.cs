using DG.Tweening;
using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class FillinMinigameManager : MinigameManager, IBeginDragHandler, IDragHandler, IEndDragHandler, IDropHandler{
    
    public int minNumber = 10;
    public int maxNumber = 30;
    public int minStep = 1;
    public int maxStep = 2;
    public TMP_InputField inputField1;
    public TMP_InputField inputField2;
    public TMP_InputField inputField3;
    public TMP_InputField inputField4;
    public TMP_InputField inputField5;
    public UnityEngine.UI.Button submitButton;
    public Image panel;
    public GameObject instructionObject;
    public GameObject anchorObject;
    public Transform hideTransform;
    public Transform showTransform;
    public float yOffset = 375f;
    public float animationDuration = 2f;
    

    private int[] numbers = new int[5];
    private int[] blankIndices;
    private TMP_InputField[] fields;
    private TMP_InputField draggingField;
    private Vector3 dragStartPos;

    private uint rounds = 0;
    private uint roundsPassed = 0;

    [SerializeField] bool useDebug = false;

    //private void Start()
    //{
    //    // For testing, start the minigame immediately
    //    InitializeMinigame(3);
    //}

    public override void InitializeMinigame(uint p_numberOfRounds = 7)
    {
        base.InitializeMinigame();

        rounds = p_numberOfRounds;
        roundsPassed = 0;
        panel.enabled = true;
        submitButton.interactable = true;

        fields = new TMP_InputField[] { inputField1, inputField2, inputField3, inputField4, inputField5 };
        GenerateAndShowNumbers();
        submitButton.onClick.RemoveAllListeners();
        submitButton.onClick.AddListener(OnSubmit);
    }

    public override void EndMinigame()
    {
        base.EndMinigame();
        panel.enabled = false;
    }

    // Drag and Drop Implementation
    public void OnBeginDrag(PointerEventData eventData)
    {
        draggingField = eventData.pointerDrag?.GetComponent<TMP_InputField>();
        if (draggingField != null)
            dragStartPos = draggingField.transform.position;
    }

    public void OnDrag(PointerEventData eventData)
    {
        if (draggingField != null)
            draggingField.transform.position = eventData.position;
    }

    public void OnEndDrag(PointerEventData eventData)
    {
        if (draggingField != null)
            draggingField.transform.position = dragStartPos;
        draggingField = null;
    }

    public void OnDrop(PointerEventData eventData)
    {
        var droppedField = eventData.pointerDrag?.GetComponent<TMP_InputField>();
        var targetField = eventData.pointerEnter?.GetComponent<TMP_InputField>();
        if (droppedField != null && targetField != null && droppedField != targetField)
        {
            // Swap text and interactable state
            string tempText = droppedField.text;
            bool tempInteract = droppedField.interactable;
            droppedField.text = targetField.text;
            droppedField.interactable = targetField.interactable;
            targetField.text = tempText;
            targetField.interactable = tempInteract;
        }
    }

    public void GenerateAndShowNumbers()
    {
        AnimateFields();
        int maxStart = maxNumber - 4 * maxStep; // ensure last number fits in range for max step
        int start = Random.Range(minNumber, maxStart + 1);
        int step = Random.Range(minStep, maxStep + 1);
        for (int i = 0; i < 5; i++)
            numbers[i] = start + i * step;
        int blanks = 2;
        blankIndices = new int[blanks];
        System.Collections.Generic.List<int> indices = new System.Collections.Generic.List<int> { 0, 1, 2, 3, 4 };
        for (int i = 0; i < blanks; i++)
        {
            int idx = indices[Random.Range(0, indices.Count)];
            blankIndices[i] = idx;
            indices.Remove(idx);
        }
        for (int i = 0; i < 5; i++)
        {
            if (System.Array.IndexOf(blankIndices, i) >= 0)
            {
                fields[i].text = "";
                fields[i].interactable = true;
            }
            else
            {
                fields[i].text = numbers[i].ToString();
                fields[i].interactable = false;
            }
        }
    }

    void AnimateFields()
    {
        //foreach (var field in fields)
        //{
        //    field.transform.DOMoveY(field.transform.position.y - yOffset, animationDuration);
        //}
        //submitButton.transform.DOMoveY(submitButton.transform.position.y - yOffset, animationDuration);
        //instructionObject.transform.DOMoveY(instructionObject.transform.position.y - yOffset, animationDuration);

        anchorObject.transform.DOMove(showTransform.position, animationDuration);
    }

    void OnSubmit()
    {
        bool correct = true;
        foreach (int idx in blankIndices)
        {
            int val;
            if (!int.TryParse(fields[idx].text, out val) || val != numbers[idx])
            {
                correct = false;
                break;
            }
        }
        submitButton.interactable = false;
        if (correct)
        {
            if (useDebug) Debug.Log("Correct!");
            roundsPassed++;
            StartCoroutine(SlideUpAndGenerate());
        }
        else
        {
            if (useDebug) Debug.Log("Incorrect!");
            // Optionally re-enable if you want to allow retry on failure:
            submitButton.interactable = true;
        }
    }

    System.Collections.IEnumerator SlideUpAndGenerate()
    {
        //foreach (var field in fields)
        //{
        //    field.transform.DOMoveY(field.transform.position.y + yOffset, animationDuration);
        //}
        //submitButton.transform.DOMoveY(submitButton.transform.position.y + yOffset, animationDuration);
        //instructionObject.transform.DOMoveY(instructionObject.transform.position.y + yOffset, animationDuration);

        anchorObject.transform.DOMove(hideTransform.position, animationDuration);

        yield return new WaitForSeconds(animationDuration);

        if (roundsPassed >= rounds)
        {
            EndMinigame();
            yield break;
        }

        GenerateAndShowNumbers();
        submitButton.interactable = true;
    }
}