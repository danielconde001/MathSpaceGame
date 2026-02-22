using DG.Tweening;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class FillinMinigameManager : MinigameManager {
 
    public int minNumber = 10;
    public int maxNumber = 30;
    public int minStep = 1;
    public int maxStep = 2;
    public InputField inputField1;
    public InputField inputField2;
    public InputField inputField3;
    public InputField inputField4;
    public InputField inputField5;
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
    private InputField[] fields;
    private InputField draggingField;
    private Vector3 dragStartPos;

    private uint rounds = 0;
    private uint roundsPassed = 0;

    [SerializeField] bool useDebug = false;

    // FOR TESTING PURPOSES ONLY
    // void Start()
    // {
    //     InitializeMinigame(3); // Start with 3 rounds for testing
    // }
    public override void InitializeMinigame(uint p_numberOfRounds = 7)
    {
        base.InitializeMinigame();

        LevelManager.Instance.LevelState = 1;

        rounds = p_numberOfRounds;
        roundsPassed = 0;
        panel.enabled = true;
        submitButton.interactable = true;

        fields = new InputField[] { inputField1, inputField2, inputField3, inputField4, inputField5 };
        GenerateAndShowNumbers();
        submitButton.onClick.RemoveAllListeners();
        submitButton.onClick.AddListener(OnSubmit);
    }

    public override void EndMinigame()
    {
        base.EndMinigame();
        LevelManager.Instance.LevelState = 0;
        panel.enabled = false;
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
            FeedbackCanvas.Instance.ShowCorrect();
            roundsPassed++;
            StartCoroutine(SlideUpAndGenerate());
        }
        else
        {
            if (useDebug) Debug.Log("Incorrect!");
            FeedbackCanvas.Instance.ShowWrong();
            // Optionally re-enable if you want to allow retry on failure:
            submitButton.interactable = true;
        }
        KeypadManager.Instance.HideKeypad(); // Hide custom keypad
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