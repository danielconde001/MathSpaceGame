using UnityEngine;

public class TensAndOnesMinigameManager : MinigameManager
{
    private MinigameCanvas canvas;

    private uint StateID = 1;

    [Header("Asteroids")]
    [SerializeField] private AsteroidScript tens;
    [SerializeField] private AsteroidScript ones;
    [SerializeField] private uint numberOfQuestions = 7;

    private uint requiredValue = 0;
    private uint currentTensValue = 0;
    private uint currentOnesValue = 0;
    
    private uint questionsAnswered = 0;

    private void Awake()
    {
        canvas = FindAnyObjectByType<MinigameCanvas>();
    }

    public void Start()
    {
        tens.manager = this;
        ones.manager = this;
    }
    public void CheckValue()
    {
        currentTensValue = tens.GetValue() * 10;
        currentOnesValue = ones.GetValue();

        if ((currentTensValue + currentOnesValue) == requiredValue)
        {
            questionsAnswered++;

            if (questionsAnswered < numberOfQuestions)
            {
                // do it again
                InitializeMinigame();
            }
            else
            {
                EndMinigame();
            }
        }
    }

    public override void InitializeMinigame()
    {
        LevelManager.Instance.LevelState = StateID;

        LevelManager.Instance.StopSectionsFromMoving();
        int rnd = Random.Range(11, 100);
        requiredValue = System.Convert.ToUInt32(rnd);
        canvas?.ShowScreen(requiredValue);
        tens?.Reset();
        ones?.Reset();
    }

    public override void EndMinigame()
    {
        LevelManager.Instance.LevelState = 0;

        tens.gameObject.SetActive(false);
        ones.gameObject.SetActive(false);

        canvas?.HideScreen();

        LevelManager.Instance.StartSectionsMovement();
    }

}
