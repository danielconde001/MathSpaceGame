using UnityEngine;

public class MinigameManager : MonoBehaviour
{
    public bool Initialized { get; protected set; }

    public virtual void InitializeMinigame(uint p_numberOfRounds = 7) 
    {
        LevelManager.Instance.LevelState = 2;
        Initialized = true;
    }

    public virtual void EndMinigame() 
    {
        LevelManager.Instance.LevelState = 0;
        Initialized = false;
    }
}
