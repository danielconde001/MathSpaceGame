using UnityEngine;
using DG.Tweening;

public class GameOverManager : MonoBehaviour
{
    private static GameOverManager instance;
    public static GameOverManager Instance
    {

        get
        {
            if (instance == null)
            {
                GameObject newGameObject = new GameObject("GameOverManager");
                instance = newGameObject.AddComponent<GameOverManager>();
            }
            return instance;
        }
    }

    [SerializeField] private UnityEngine.UI.Image panel;
    [SerializeField] private GameObject content;

    private void Awake()
    {
        instance = this;
    }
    public void ShowScreen()
    {
        panel.gameObject.SetActive(true);

        PauseManager.Instance.IsPaused = true;

        content.transform.DOLocalMoveY(1080, 0f, true);
        content.transform.DOLocalMoveY(0, .7f);
    }

    public void ExitGame()
    {
        UnityEngine.SceneManagement.SceneManager.LoadScene("MainMenu");
    }

    public void Respawn()
    {
        PlayerManager.Instance.GetPlayer().Revive();
        HideScreen(PowerUpManager.Instance.ShowScreen, true);
    }

    public void HideScreen(System.Action p_functionAfterHiding = null, bool p_pauseAfterHiding = false)
    {
        content.transform.DOLocalMoveY(0, 0f, true);
        content.transform.DOLocalMoveY(1080, .7f).OnComplete
            (
                () => 
                {
                    panel.gameObject.SetActive(false);

                    if (p_pauseAfterHiding == true)
                    {
                        PauseManager.Instance.IsPaused = true;
                    }
                    else
                    {
                        PauseManager.Instance.IsPaused = false;

                    }

                    p_functionAfterHiding?.Invoke();
                }
            );
    }
}
