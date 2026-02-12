using UnityEngine;

public class PauseManager : MonoBehaviour
{
    private static PauseManager instance;
    public static PauseManager Instance
    {

        get
        {
            if (instance == null)
            {
                GameObject newGameObject = new GameObject("PauseManager");
                instance = newGameObject.AddComponent<PauseManager>();
            }
            return instance;
        }
    }

    private void Awake()
    {
        instance = this;
    }

    public bool IsPaused = false;
}
