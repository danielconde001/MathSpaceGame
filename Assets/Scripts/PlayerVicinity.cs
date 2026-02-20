using UnityEngine;
using System.Collections.Generic;
using System.Linq;

public class PlayerVicinity : MonoBehaviour
{
    private static PlayerVicinity instance;
    public static PlayerVicinity Instance 
    {
        get
        {
            if (instance == null)
            {
                instance = PlayerManager.Instance?.GetPlayer()?.
                    gameObject?.GetComponentInChildren<PlayerVicinity>();
            }
            return instance;
        }
    }

    public List<DistanceToPlayer> DistancesToPlayer = new List<DistanceToPlayer>();

    public bool ContainsTransform(Transform p_transform)
    {
        for (int i = 0; i < DistancesToPlayer.Count; i++)
        {
            if (DistancesToPlayer[i].transform == p_transform) return true;
        }

        return false;
    }

    public Transform GetNearestTransform()
    {
        if (DistancesToPlayer.Count > 0)
        {
            Transform nearestTransform = DistancesToPlayer[0].transform;
            float nearest = DistancesToPlayer[0].Distance;

            for (int i = 1; i < DistancesToPlayer.Count; i++)
            {
                if (DistancesToPlayer[i].Distance < nearest)
                {
                    nearest = DistancesToPlayer[i].Distance;
                    nearestTransform = DistancesToPlayer[i].transform;
                }
            }

            return nearestTransform;
        }

        else
        {
            return null;
        }
    }

    public float GetNearestDistance()
    {
        if (DistancesToPlayer.Count > 0)
        {
            float nearest = DistancesToPlayer[0].Distance;

            for (int i = 1; i < DistancesToPlayer.Count; i++)
            {
                if (DistancesToPlayer[i].Distance < nearest)
                {
                    nearest = DistancesToPlayer[i].Distance;
                }
            }

            return nearest;
        }

        else
        {
            return 0f;
        }
    }
}
