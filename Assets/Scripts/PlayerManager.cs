using UnityEngine;

public class PlayerManager : MonoBehaviour
{
    private static PlayerManager instance;
    public static PlayerManager Instance
    {
        
        get 
        {
            if (instance == null)
            {
                GameObject newGameObject = new GameObject("PlayerManager");
                instance = newGameObject.AddComponent<PlayerManager>();
            }
            return instance; 
        } 
    }

    [SerializeField] private PlayerScript m_Player;

    private void Awake()
    {
        instance = this;

        if (m_Player == null)
        {
            m_Player = FindFirstObjectByType<PlayerScript>();
        }
    }

    public PlayerScript GetPlayer()
    {
        return m_Player;
    }
}
