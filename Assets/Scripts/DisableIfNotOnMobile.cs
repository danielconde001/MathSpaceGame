using UnityEngine;

public class DisableIfNotOnMobile : MonoBehaviour
{
    void Start()
    {
        if (Application.isMobilePlatform == false)
        {
            gameObject.SetActive(false);
        }
    }    
}
