using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;
using DG.Tweening;
using UnityEngine.UI;

public class PauseManager : MonoBehaviour
{
    private static PauseManager instance;
    public static PauseManager Instance
    {

        get
        {
            if (instance == null)
            {
                GameObject newGameObject = new GameObject("PauseManager");
                instance = newGameObject.AddComponent<PauseManager>();
            }
            return instance;
        }
    }

    [SerializeField] private GameObject Content;
    [SerializeField] private TextMeshProUGUI Header;
    [SerializeField] private Image Background;
    [SerializeField] private Image ContinueButton;
    [SerializeField] private Image RetryButton;
    [SerializeField] private Image ExitButton;
    [SerializeField] private float FadeInDuration;
    [SerializeField] private float FadeOutDuration;
    [SerializeField] private Button pauseButton;

    // Other UI
    PowerUpManager powerUpManager;
    GameObject mobileCanvas;
    GameOverManager gameOverCanvas;
    ScoreManager scoreCanvas;
    GameObject helpGuideCanvas;
    DialogueManager dialogBox;

    private void Start()
    {
        instance = this;

        powerUpManager = FindAnyObjectByType<PowerUpManager>();
        mobileCanvas = GameObject.Find("MobileCanvas");
        gameOverCanvas = FindAnyObjectByType<GameOverManager>();
        scoreCanvas = FindAnyObjectByType<ScoreManager>();
        helpGuideCanvas = GameObject.Find("HelpGuideCanvas");
        dialogBox = FindAnyObjectByType<DialogueManager>();
    }

    private bool isPaused = false;
    public bool IsPaused 
    { 
        get
        { 
            return isPaused;
        }
    }

    public void Pause()
    {
        isPaused = true;
    }

    public void Unpause()
    {
        isPaused = false;
    }

    public void ShowScreen()
    {
        pauseButton.gameObject.SetActive(false);
        Content.SetActive(true);

        AudioManager.Instance.PlayUIPauseButtonClickSFX();

        Pause();

        powerUpManager.gameObject.SetActive(false);
        mobileCanvas.SetActive(false);
        gameOverCanvas.gameObject.SetActive(false);
        scoreCanvas.gameObject.SetActive(false);
        helpGuideCanvas.SetActive(false);
        dialogBox.gameObject.SetActive(false);

        Background.DOColor(new Color(0, 0, 0, 0.5f) ,FadeInDuration);
        ContinueButton.DOColor(new Color(1, 1, 1, 1), FadeInDuration);
        RetryButton.DOColor(new Color(1, 1, 1, 1), FadeInDuration);
        ExitButton.DOColor(new Color(1, 1, 1, 1), FadeInDuration);
    }

    public void HideScreen()
    {
        ContinueButton.DOColor(new Color(1, 1, 1, 0), FadeOutDuration);
        RetryButton.DOColor(new Color(1, 1, 1, 0), FadeOutDuration);
        ExitButton.DOColor(new Color(1, 1, 1, 0), FadeOutDuration);
        Background.DOColor(new Color(0, 0, 0, 0), FadeOutDuration)
            .OnComplete( () => {
                Content.SetActive(false);
                pauseButton.gameObject.SetActive(true);
                AudioManager.Instance.PlayUIContinueButtonClickSFX();
                powerUpManager.gameObject.SetActive(true);
                mobileCanvas.SetActive(true);
                gameOverCanvas.gameObject.SetActive(true);
                scoreCanvas.gameObject.SetActive(true);
                helpGuideCanvas.SetActive(true);
                dialogBox.gameObject.SetActive(true);
                Unpause();
            });
    }

    public void Retry()
    {
        // transition

        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }

    public void Quit()
    {
        // transition

        SceneManager.LoadScene("MainMenu");
    }
}
