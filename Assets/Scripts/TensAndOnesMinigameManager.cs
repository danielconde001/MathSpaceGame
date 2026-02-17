using System.Collections;
using Unity.VisualScripting;
using UnityEngine;
using static UnityEngine.Rendering.DebugUI;

public class TensAndOnesMinigameManager : MinigameManager
{
    private TensAndOnesMinigameCanvas canvas;

    private uint StateID = 1;

    [Header("Asteroids")]
    [SerializeField] private AsteroidScript tens;
    [SerializeField] private AsteroidScript ones;
    
    private uint requiredValue = 0;
    private uint currentTensValue = 0;
    private uint currentOnesValue = 0;
    
    private uint rounds = 7;
    private uint roundsPassed = 0;

    private bool canRegisterBullet = true;
    public bool CanRegisterBullet { get => canRegisterBullet; }

    private void Awake()
    {
        canvas = FindAnyObjectByType<TensAndOnesMinigameCanvas>();
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
            FeedbackCanvas.Instance.ShowCorrect();
            StartCoroutine(GoToNextRound());
        }
    }

    IEnumerator GoToNextRound()
    {
        roundsPassed++;
        canRegisterBullet = false;

        yield return new WaitForSeconds(1f);

        canRegisterBullet = true;

        if (roundsPassed < rounds)
        {
            // do it again
            InitializeMinigame();
        }
        else
        {
            EndMinigame();
        }
    }

    public override void InitializeMinigame(uint p_numberOfRounds = 7)
    {
        base.InitializeMinigame(p_numberOfRounds);

        LevelManager.Instance.LevelState = StateID;

        rounds = p_numberOfRounds;

        int rnd = Random.Range(11, 100);
        requiredValue = System.Convert.ToUInt32(rnd);
        canvas?.ShowScreen(requiredValue);
        tens?.Reset();
        ones?.Reset();
    }

    public override void EndMinigame()
    {
        base.EndMinigame();

        LevelManager.Instance.LevelState = 0;

        tens.Kill();
        ones.Kill();

        canvas?.HideScreen();
    }
}
