using System.Collections;
using Unity.VisualScripting;
using UnityEngine;
using static UnityEngine.Rendering.DebugUI;

public class TensAndOnesMinigameManager : MinigameManager
{
    private TensAndOnesMinigameCanvas canvas;

    [Header("Asteroids")]
    [SerializeField] private AsteroidScript tens;
    [SerializeField] private AsteroidScript ones;
    
    private uint requiredValue = 0;
    private uint currentTensValue = 0;
    private uint currentOnesValue = 0;
    
    private uint rounds = 7;
    private uint roundsPassed = 0;

    private bool canRegisterBullet = false;
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

        LevelManager.Instance.LevelState = 2;

        canRegisterBullet = true;

        rounds = p_numberOfRounds;

        int rnd = Random.Range(11, 100);
        requiredValue = System.Convert.ToUInt32(rnd);
        canvas?.ShowScreen(requiredValue);
        tens?.Reset();
        ones?.Reset();
    }

    public override void EndMinigame()
    {
        StartCoroutine(EndMinigameCoroutine());
    }

    IEnumerator EndMinigameCoroutine()
    {
        LevelManager.Instance.LevelState = 0;

        tens.Kill();
        ones.Kill();

        canvas?.HideScreen();

        yield return new WaitForSeconds(1f);

        base.EndMinigame();
    }
}
