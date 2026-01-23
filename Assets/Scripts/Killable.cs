using UnityEngine;

public class Killable : MonoBehaviour
{
    protected bool isKilled = false;

    public bool IsKilled()
    {
        return isKilled;
    }

    public virtual void Kill()
    {
        isKilled = true;
    }
}
