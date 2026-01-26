using UnityEngine;

public class GameManager : MonoBehaviour
{
    public enum GameState
    {
        Normal,
        Minigame,
        UIMinigame,
        Paused,
        Count
    }

    private static GameManager instance;
    public static GameManager Instance
    {

        get
        {
            if (instance == null)
            {
                GameObject newGameObject = new GameObject("GameManager");
                instance = newGameObject.AddComponent<GameManager>();
            }
            return instance;
        }
    }

    private GameState state;
    public GameState State 
    { 
        get => state; 
        set => state = value; 
    }

    private void Awake()
    {
        instance = this;
    }

}
