using UnityEngine;

public class OnGoodRockDeath : MonoBehaviour
{
    [SerializeField] uint gemValue = 1;

    public void OnDeath()
    {
        // Do seomthing good
        LevelManager.Instance.CollectGems(gemValue);
    }
}
