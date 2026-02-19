using UnityEngine;

public class MobileOnlyEnabled : MonoBehaviour
{
    void Awake()
    {
        if (Application.isMobilePlatform == false)
        {
            gameObject.SetActive(false);
        }
    }    
}
