using UnityEngine;

public class DistanceToPlayer : MonoBehaviour
{
    private float distance = 0;
    public float Distance
    { 
        get => distance;
    }

    private void Start()
    {
        PlayerVicinity.Instance.DistancesToPlayer.Add(this);
    }

    void Update()
    {
        distance = Vector3.Distance
            (
            PlayerManager.Instance.GetPlayer().transform.position,
            transform.position
            );

        if (transform.position.z < PlayerManager.Instance.GetPlayer().transform.position.z)
        {
            PlayerVicinity.Instance.DistancesToPlayer.Remove(this);
        }
    }
}
