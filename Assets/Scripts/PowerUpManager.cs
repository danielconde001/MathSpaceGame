using DG.Tweening;
using UnityEngine;

public class PowerUpManager : MonoBehaviour
{
    [HideInInspector]
    public int powerUpsReceived = 0; // 1 - Health Boost, 2 - Double Damage, 4 - Faster Fire Rate, 8 - Heal per Hit

    private static PowerUpManager instance;
    public static PowerUpManager Instance
    {
        get
        {
            if (instance == null)
            {
                GameObject newGameObject = new GameObject("PowerUpManager");
                instance = newGameObject.AddComponent<PowerUpManager>();
            }
            return instance;
        }
    }

    [SerializeField] private UnityEngine.UI.Image panel;
    [SerializeField] private RectTransform rectTransform;

    [Header("Milestones")]
    [SerializeField] int[] milestones = new int[3];

    [Header("Power Up Values")]
    [SerializeField] int healthBonus;
    [SerializeField] float upgradedFireRate;

    [Header("Debug")]
    [SerializeField] bool useDebug = false;

    private void Awake()
    {
        instance = this;
    }

    private void Update()
    {
        if (useDebug == true)
        {
            if (Input.GetKeyDown(KeyCode.O))
            {
                ShowScreen();
            }

            if (Input.GetKeyDown(KeyCode.P))
            {
                HideScreen();
            }
        }
    }

    public void CheckMilestone()
    {
        uint currentPlayerLevel = PlayerManager.Instance.GetPlayer().PlayerLevel;
        if (ScoreManager.Instance.CurrentScore >= milestones[0] &&
            ScoreManager.Instance.CurrentScore < milestones[1] &&
            currentPlayerLevel == 1)
        {
            ShowScreen();
        }
        else if (ScoreManager.Instance.CurrentScore >= milestones[1] &&
            ScoreManager.Instance.CurrentScore < milestones[2] &&
            currentPlayerLevel == 2)
        {
            ShowScreen();
        }
        else if (ScoreManager.Instance.CurrentScore >= milestones[2] &&
            currentPlayerLevel == 3)
        {
            ShowScreen();
        }
    }

    public uint GetCurrentMilestone()
    {
        uint currentPlayerLevel = PlayerManager.Instance.GetPlayer().PlayerLevel;
        switch (currentPlayerLevel)
        {
            case 0:
                PlayerManager.Instance.GetPlayer().LevelUpPlayer();
                return (uint)milestones[0];
            case 1:
                return (uint)milestones[0];
            case 2:
                return (uint)milestones[1];
            case 3:
                return (uint)milestones[2];
            default:
                // Max level
                return (uint)milestones[2];
        }
    }

    public void ShowScreen()
    {
        panel.gameObject.SetActive(true);

        PauseManager.Instance.Pause();

        LevelManager.Instance.LevelState = 1;

        UIActivationManager.Instance.DeactivateOtherUI(gameObject);

        AudioManager.Instance.PlayLevelUpSFX();

        rectTransform.DOLocalMoveY(820, 0f, true);
        rectTransform.DOLocalMoveY(0, .7f);
    }

    public void HideScreen()
    {
        rectTransform.gameObject.transform.DOLocalMoveY(0, 0f, true);
        rectTransform.gameObject.transform.DOLocalMoveY(820, .7f).OnComplete
            (
                () => 
                {
                    panel.gameObject.SetActive(false);
                    LevelManager.Instance.LevelState = 0;
                    UIActivationManager.Instance.ActivateOtherUI(gameObject);
                    PauseManager.Instance.Unpause();
                }   
            );
    }

    public void ReceivePowerUp(int p_powerUp)
    {
        switch (p_powerUp)
        {
            case 1:
                RecieveHealthBoost();
                PlayerManager.Instance.GetPlayer().LevelUpPlayer();
                ScoreManager.Instance.UpdateScoreUI();
                AudioManager.Instance.PlayReceivePowerUpSFX();
                break;
            case 2:
                RecieveDoubleBullets();
                PlayerManager.Instance.GetPlayer().LevelUpPlayer();
                ScoreManager.Instance.UpdateScoreUI();
                AudioManager.Instance.PlayReceivePowerUpSFX();
                break;
            case 4:
                RecieveFasterFireRate();
                PlayerManager.Instance.GetPlayer().LevelUpPlayer();
                ScoreManager.Instance.UpdateScoreUI();
                AudioManager.Instance.PlayReceivePowerUpSFX();
                break;
            case 8:
                RecieveHealPerHit();
                PlayerManager.Instance.GetPlayer().LevelUpPlayer();
                ScoreManager.Instance.UpdateScoreUI();
                AudioManager.Instance.PlayReceivePowerUpSFX();
                break;
            default:
                Debug.LogWarning("No power up for that index exists.");
                return;
        }
    }

    void RecieveHealthBoost(int p_powerUp = 1)
    {
        if (HasHealthBoost() == true)
        {
            if (useDebug) Debug.Log("Power Up already recieved!");
            return;
        }

        if (useDebug) Debug.Log("Recieved Health Boost!");
        PlayerManager.Instance.GetPlayer().AddMaxHealth(healthBonus);
        PlayerManager.Instance.GetPlayer().Health.value
            = PlayerManager.Instance.GetPlayer().GetMaxHealth();
        powerUpsReceived += p_powerUp;
    }
    void RecieveDoubleBullets(int p_powerUp = 2)
    {
        if (HasDoubleBullets() == true)
        {
            if (useDebug) Debug.Log("Power Up already recieved!");
            return;
        }

        if (useDebug) Debug.Log("Recieved Double Bullets!");
        powerUpsReceived += p_powerUp;
    }

    void RecieveFasterFireRate(int p_powerUp = 4)
    {
        if (HasFasterFireRate() == true)
        {
            if (useDebug) Debug.Log("Power Up already recieved!");
            return;
        }

        if (useDebug) Debug.Log("Recieved Faster Fire Rate");
        PlayerManager.Instance.GetPlayer().GetAttackScript().SetFireRate(upgradedFireRate);
        powerUpsReceived += p_powerUp;
    }

    void RecieveHealPerHit(int p_powerUp = 8)
    {
        if (HasHealPerHit() == true)
        {
            if (useDebug) Debug.Log("Power Up already recieved!");
            return;
        }

        if (useDebug) Debug.Log("Recieved Heal Per Hit!");
        powerUpsReceived += p_powerUp;
    }

    public bool HasHealthBoost()
    {
        return (powerUpsReceived & 1) == 1;
    }

    public bool HasDoubleBullets()
    {
        return (powerUpsReceived & 2) == 2;
    }

    public bool HasFasterFireRate()
    {
        return (powerUpsReceived & 4) == 4;
    }

    public bool HasHealPerHit()
    {
        return (powerUpsReceived & 8) == 8;
    }

    public bool HasAllPowerUps()
    {
        return HasHealthBoost() && HasDoubleBullets() && HasFasterFireRate() && HasHealPerHit();
    }
}
