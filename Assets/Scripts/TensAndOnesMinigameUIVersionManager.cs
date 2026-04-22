using DG.Tweening;
using System.Linq;
using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class TensAndOnesMinigameUIVersionManager : MinigameManager
{
    public TensAndOnesButton tensButton;
    public TensAndOnesButton onesButton;
    public Button submitButton;
    public Image panel;
    public GameObject instructionObject;
    public TextMeshProUGUI instructionText;
    public GameObject anchorObject;
    public Transform hideTransform;
    public Transform showTransform;
    public float yOffset = 250f;
    public float animationDuration = 0.5f;

    private uint rounds = 0;
    private uint roundsPassed = 0;

    private int answerValue = 0;

    [SerializeField] bool useDebug = false;

    public override void InitializeMinigame(uint p_numberOfRounds = 7)
    {
        base.InitializeMinigame();

        LevelManager.Instance.LevelState = 1;

        UIActivationManager.Instance.DeactivateOtherUI();

        rounds = p_numberOfRounds;
        roundsPassed = 0;
        panel.enabled = true;
        submitButton.interactable = true;

        GenerateAndShowNumbers();

        submitButton.onClick.RemoveAllListeners();
        submitButton.onClick.AddListener(OnSubmit);
    }

    public override void EndMinigame()
    {
        base.EndMinigame();
        LevelManager.Instance.LevelState = 0;
        UIActivationManager.Instance.ActivateOtherUI();
        panel.enabled = false;
    }

    public void GenerateAndShowNumbers()
    {
        tensButton.Reset();
        onesButton.Reset();
        answerValue = Random.Range(10, 100);
        instructionText.text = "How do you make <color=#00FFFF>" + answerValue.ToString() + "</color>?";
        
        AnimateFields();
    }

    void AnimateFields()
    {
        anchorObject.transform.DOMove(showTransform.position, animationDuration);
    }

    void OnSubmit()
    {
        bool correct = true;

        int submitted = (tensButton.Value * 10) + onesButton.Value;
        correct = submitted == answerValue;

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
            // Add failure logic here
            FeedbackCanvas.Instance.ShowWrong();
            // Optionally re-enable if you want to allow retry on failure:
            submitButton.interactable = true;
        }
    }

    public System.Collections.IEnumerator SlideUpAndGenerate()
    {
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