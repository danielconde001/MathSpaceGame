using UnityEngine;

public class OnboardingManager : MonoBehaviour
{
    private static OnboardingManager instance;
    public static OnboardingManager Instance
    {
        get
        {
            if (instance == null)
            {
                GameObject newGameObject = new GameObject("OnboardingManager");
                instance = newGameObject.GetComponent<OnboardingManager>();
            }
            return instance;
        }
    }

    private void Awake()
    {
        if (instance != null)
        {
            Destroy(gameObject);
        }
        else
        {
            instance = this;
        }

        DontDestroyOnLoad(gameObject);
    }

    public bool knowsHowToPlay = false;
    public bool isUsingMobileDevice = false;
}
