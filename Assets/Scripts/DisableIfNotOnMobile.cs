using UnityEngine;

public class DisableIfNotOnMobile : MonoBehaviour
{
    void Start()
    {
        ShowMobileControlsUI();
    }

    public void ShowMobileControlsUI()
    {
        if (Application.isMobilePlatform) return;
        else
        {
            if (OnboardingManager.Instance.isUsingMobileDevice == false)
            {
                gameObject.SetActive(false);
            }
        }
    }
}
