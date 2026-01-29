using UnityEngine;
using UnityEngine.UI;
using TMPro;
using DG.Tweening;
using UnityEngine.EventSystems;

public class FillinMinigameManager : MonoBehaviour, IBeginDragHandler, IDragHandler, IEndDragHandler, IDropHandler
{
    public TMP_InputField inputField1;
    public TMP_InputField inputField2;
    public TMP_InputField inputField3;
    public TMP_InputField inputField4;
    public TMP_InputField inputField5;
    public Button submitButton;

    private int[] numbers = new int[5];
    private int[] blankIndices;
    private TMP_InputField[] fields;
    private TMP_InputField draggingField;
    private Vector3 dragStartPos;

    void Start()
    {
        InitializeMinigame();
    }

    public void InitializeMinigame()
    {
        fields = new TMP_InputField[] { inputField1, inputField2, inputField3, inputField4, inputField5 };
        GenerateAndShowNumbers();
        submitButton.onClick.RemoveAllListeners();
        submitButton.onClick.AddListener(OnSubmit);
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
        int start = Random.Range(10, 26);
        int step = Random.Range(1, 3);
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
        foreach (var field in fields)
        {
            field.transform.DOMoveY(field.transform.position.y - 500f, 2f);
        }
        submitButton.transform.DOMoveY(submitButton.transform.position.y - 500f, 2f);
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
            Debug.Log("Correct!");
            StartCoroutine(SlideUpAndGenerate());
        }
        else
        {
            Debug.Log("Incorrect!");
            // Optionally re-enable if you want to allow retry on failure:
            submitButton.interactable = true;
        }
    }

    System.Collections.IEnumerator SlideUpAndGenerate()
    {
        foreach (var field in fields)
        {
            field.transform.DOMoveY(field.transform.position.y + 500f, 2f);
        }
        submitButton.transform.DOMoveY(submitButton.transform.position.y + 500f, 2f);
        yield return new WaitForSeconds(2f);
        GenerateAndShowNumbers();
        submitButton.interactable = true;
    }
}