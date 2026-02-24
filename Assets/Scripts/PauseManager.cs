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

    private void Awake()
    {
        instance = this;
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

        UIActivationManager.Instance.DeactivateOtherUI(gameObject);

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
                UIActivationManager.Instance.ActivateOtherUI(gameObject);
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
