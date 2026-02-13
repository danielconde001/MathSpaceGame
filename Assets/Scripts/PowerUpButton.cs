using UnityEngine;

public class PowerUpButton : MonoBehaviour
{
    [SerializeField] int Value; 
    UnityEngine.UI.Button button;

    private void Awake()
    {
        button = GetComponent<UnityEngine.UI.Button>();
    }

    private void OnEnable()
    {
        bool hasPowerUp;

        switch (Value)
        {
            case 1:
                hasPowerUp = PowerUpManager.Instance.HasHealthBoost();
                button.interactable = !hasPowerUp;
                break;
            case 2:
                hasPowerUp = PowerUpManager.Instance.HasDoubleBullets();
                button.interactable = !hasPowerUp;
                break;
            case 4:
                hasPowerUp = PowerUpManager.Instance.HasFasterFireRate();
                button.interactable = !hasPowerUp;
                break;
            case 8:
                hasPowerUp = PowerUpManager.Instance.HasHealPerHit();
                button.interactable = !hasPowerUp;
                break;

        }
    }
}
