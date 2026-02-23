using UnityEngine;

public class DisableIfNotOnMobile : MonoBehaviour
{
    void Start()
    {
        if (Application.isMobilePlatform == false)
        {
            if (DeviceDetector.IsRunningOniPad()) return;

            gameObject.SetActive(false);
        }
    }    
}
