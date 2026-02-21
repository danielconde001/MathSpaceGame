using UnityEngine;
using System.Collections.Generic;

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

    [SerializeField] float range = 50f;
    [SerializeField] bool useDebug = false; 

    public List<DistanceToPlayer> DistancesToPlayer = new List<DistanceToPlayer>();


    public bool ContainsTransform(Transform p_transform)
    {
        for (int i = 0; i < DistancesToPlayer.Count; i++)
        {
            if (DistancesToPlayer[i].transform == p_transform) return true;
        }

        return false;
    }

    public DistanceToPlayer GetNearest()
    {
        if (DistancesToPlayer.Count > 0)
        {
            DistanceToPlayer nearest = DistancesToPlayer[0];

            for (int i = 1; i < DistancesToPlayer.Count; i++)
            {
                if (DistancesToPlayer[i].Distance < nearest.Distance)
                {
                    nearest = DistancesToPlayer[i];
                }
            }

            if (nearest.Distance > range)
            {
                return null;
            }
            else
            {
                if (useDebug == true)
                {
                    Debug.Log
                    (
                        nearest.GetComponent<DistanceToPlayer>().Distance,
                        nearest.gameObject
                    );
                }

                return nearest;
            }
        }

        else
        {
            return null;
        }
    }
}
