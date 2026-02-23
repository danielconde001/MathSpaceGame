using UnityEngine;
using TMPro;

public class ScoreManager : MonoBehaviour
{
    public static ScoreManager Instance { get; private set; }
    public int CurrentScore { get; private set; }

    [SerializeField] private TextMeshProUGUI scoreText; // Assign in Inspector
    [SerializeField] private XPBar xpBar;

    private void Awake()
    {
        if (Instance != null && Instance != this)
        {
            Destroy(gameObject);
            return;
        }

        Instance = this;

        if (xpBar == null)
        {
            xpBar = GetComponent<XPBar>();
        }
    }

    private void Start()
    {
        UpdateScoreUI();
    }

    public void ResetScore()
    {
        CurrentScore = 0;
        UpdateScoreUI();
    }

    public void AddScore(int value)
    {
        CurrentScore += value;
        UpdateScoreUI();
    }

    public void UpdateScoreUI()
    {
        uint currentMilestone = PowerUpManager.Instance.GetCurrentMilestone();

        if (scoreText != null)
        {
            if (PlayerManager.Instance.GetPlayer().PlayerLevel >= 4) // at max level
            {
                scoreText.text = "MAX LEVEL!";
            }
            else
            {
                scoreText.text = $"{CurrentScore}" + "/" + currentMilestone;
            }
        }

        if (xpBar == null)
        {
            Debug.LogWarning("Please attach XPBar component to " + this.gameObject.name, this);
        }

        xpBar?.UpdateXPBar();

        PowerUpManager.Instance.CheckMilestone();
    }
}
