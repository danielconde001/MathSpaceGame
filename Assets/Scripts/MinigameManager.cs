using UnityEngine;

public class MinigameManager : MonoBehaviour
{
    public enum MinigameState
    {
        None,
        StationaryShooting,
        UIMinigame,
        Count
    }

    private static MinigameManager instance;
    public static MinigameManager Instance
    {

        get
        {
            if (instance == null)
            {
                GameObject newGameObject = new GameObject("MinigameManager");
                instance = newGameObject.AddComponent<MinigameManager>();
            }
            return instance;
        }
    }

    [SerializeField] private MinigameState state = MinigameState.None;
    [SerializeField] private MinigameCanvas canvas;

    [Header("Asteroids")]
    [SerializeField] private AsteroidScript tens;
    [SerializeField] private AsteroidScript ones;

    private uint requiredValue = 0;
    private uint currentTensValue = 0;
    private uint currentOnesValue = 0;

    public MinigameState State
    {
        get => state;
        set => state = value;
    }

    private void Awake()
    {
        instance = this;
    }

    public void Start()
    {
        StartMinigame();
    }

    private void StartMinigame()
    {
        int rnd = Random.Range(11, 100);
        requiredValue = System.Convert.ToUInt32(rnd);
        canvas.ShowScreen(requiredValue);
        tens.Reset();
        ones.Reset();
    }

    public void CheckValue(uint p_newValue, bool isTens)
    {
        
        if (isTens) currentTensValue = (p_newValue * 10);
        else currentOnesValue = p_newValue;
        
        if ((currentTensValue + currentOnesValue) == requiredValue)
        {
            // do it again
            StartMinigame();
        }
    }
}
