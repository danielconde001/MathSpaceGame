using UnityEngine;
using UnityEngine.UI;

public class XPBar : MonoBehaviour
{
    [SerializeField] private Image backgroundImage;
    [SerializeField] private Image fillImage;

    public void UpdateXPBar()
    {
        uint currentValue = (uint)ScoreManager.Instance.CurrentScore;
        uint maxValue = PowerUpManager.Instance.GetCurrentMilestone();

        float fillAmount;

        if (PlayerManager.Instance.GetPlayer().PlayerLevel >= 4) // at max level
        {
            fillAmount = maxValue / maxValue;
        }
        else
        {
            fillAmount = (float)currentValue / maxValue;
        }

        fillImage.fillAmount = Mathf.Clamp01(fillAmount);
    }
}
