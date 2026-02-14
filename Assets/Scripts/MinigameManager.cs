using UnityEngine;

public class MinigameManager : MonoBehaviour
{
    public bool Initialized { get; protected set; }

    public virtual void InitializeMinigame(uint p_numberOfRounds = 7) 
    {
        Initialized = true;
    }

    public virtual void EndMinigame() 
    {
        Initialized = false;
    }
}
