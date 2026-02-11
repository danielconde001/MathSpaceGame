using UnityEngine;

public class OnBadRockDeath : MonoBehaviour
{
    public void OnDeath()
    {
        // deduct Player health
        Debug.Log("Bad!");
    }
}
