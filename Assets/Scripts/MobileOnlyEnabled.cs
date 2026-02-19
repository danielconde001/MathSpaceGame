using UnityEngine;

public class MobileOnlyEnabled : MonoBehaviour
{
    void Start()
    {
        if (Application.isMobilePlatform == false)
        {
            gameObject.SetActive(false);
        }
    }    
}
