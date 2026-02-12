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

    [Header("Power Up values")]
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
        if (Input.GetMouseButtonDown(1))
        {
            ShowScreen();
        }

        if (Input.GetMouseButtonDown(2))
        {
            HideScreen();
        }
    }

    public void ShowScreen()
    {
        panel.gameObject.SetActive(true);

        PauseManager.Instance.IsPaused = true;

        rectTransform.DOLocalMoveY(820, 0f, true);
        rectTransform.DOLocalMoveY(0, .7f);
    }

    public void HideScreen()
    {
        rectTransform.gameObject.transform.DOLocalMoveY(0, 0f, true);
        rectTransform.gameObject.transform.DOLocalMoveY(820, .7f).OnComplete
            (
                () => {
                    panel.gameObject.SetActive(false);
                    PauseManager.Instance.IsPaused = false;
                }   
            );
    }

    public void ReceivePowerUp(int p_powerUp)
    {
        switch (p_powerUp)
        {
            case 1:
                RecieveHealthBoost();
                break;
            case 2:
                RecieveDoubleBullets();
                break;
            case 4:
                RecieveFasterFireRate();
                break;
            case 8:
                RecieveHealPerHit();
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
}
