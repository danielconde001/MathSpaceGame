using DG.Tweening;
using TMPro;
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

    
}
