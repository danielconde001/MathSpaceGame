using DG.Tweening;
using System.Collections;
using Unity.VisualScripting;
using UnityEngine;

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
    [SerializeField] private MinigameSequencer minigameSequencer;

    private void Awake()
    {
        instance = this;

        if (minigameSequencer == null)
        {
            Debug.LogWarning("Minigame Sequencer has no reference!", this);
        }
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
        HideScreen(PlayMinigameForRespawn);
    }

    private void PlayMinigameForRespawn()
    {
        StartCoroutine(PlayMinigameCoroutine());
    }

    IEnumerator PlayMinigameCoroutine()
    {
        PauseManager.Instance.IsPaused = true;

        if (minigameSequencer == null)
        {
            Debug.LogError("Missing Minigame Sequencer!", this);
        }

        minigameSequencer?.StartSequence();

        yield return new WaitUntil( () => minigameSequencer?.SequenceIsOngoing == false );

        if (PowerUpManager.Instance.HasAllPowerUps() == true)
        {
            PauseManager.Instance.IsPaused = false;
            yield break;
        }

        PowerUpManager.Instance.ShowScreen();
    }

    public void HideScreen(System.Action p_functionAfterHiding = null)
    {
        content.transform.DOLocalMoveY(0, 0f, true);
        content.transform.DOLocalMoveY(1080, .7f).OnComplete
            (
                () => 
                {
                    panel.gameObject.SetActive(false);
                    p_functionAfterHiding?.Invoke();
                }
            );
    }
}
