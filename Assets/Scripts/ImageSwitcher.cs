using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

public class ImageSwitcher : MonoBehaviour
{
    public Image targetImage;         // The UI Image to display sprites
    public Sprite[] sprites;          // The sprites to cycle through
    public Button backButton;         // Assign a Back button
    public Button nextButton;         // Assign a Next button

    public GameObject Canvas; // The canvas to disable at the end
    public GameObject playButton;     // The play button to enable at the end
    public GameObject eduGuideButton; // The edu guide button to enable at the end
    public Button closeButton;        // The close button to close and reset the guide

    private int currentIndex = 0;

    [SerializeField] bool usedInMainMenu = false;

    void Start()
    {
        UpdateImage();
        if (backButton != null)
            backButton.onClick.AddListener(ShowPrevious);
        if (nextButton != null)
            nextButton.onClick.AddListener(ShowNext);
        if (closeButton != null)
            closeButton.onClick.AddListener(CloseGuide);
    }

    public void OpenGuide()
    {
        if (Canvas != null)
            Canvas.SetActive(true);

        if (LevelManager.Instance.LevelState == 0 && usedInMainMenu == false)
        {
            PauseManager.Instance.Pause();
        }

        eduGuideButton.SetActive(false);

        if (usedInMainMenu == false)
            UIActivationManager.Instance?.DeactivateOtherUI(gameObject);
    }

    public void CloseGuide()
    {
        if (Canvas != null)
            Canvas.SetActive(false);
        if (eduGuideButton != null)
            eduGuideButton.SetActive(true);

        if (LevelManager.Instance.LevelState == 0 && usedInMainMenu == false)
        {
            PauseManager.Instance.Unpause();
        }

        currentIndex = 0;
        UpdateImage();

        if (usedInMainMenu == false)
            UIActivationManager.Instance?.ActivateOtherUI(gameObject);
    }

    public void ShowPrevious()
    {
        if (currentIndex > 0)
        {
            currentIndex--;
            UpdateImage();
        }
    }

    public void ShowNext()
    {
        if (currentIndex < sprites.Length - 1)
        {
            currentIndex++;
            UpdateImage();
        }
        else if (currentIndex == sprites.Length - 1)
        {
            // Last sprite, so hide tutorial and show buttons
            if (playButton != null)
                playButton.SetActive(true);
            // Optionally disable nextButton after tutorial is finished
            if (nextButton != null)
                nextButton.interactable = false;
            // Reset image to index 0
            currentIndex = 0;

            UpdateImage();

            CloseGuide();
        }
    }

    private void UpdateImage()
    {
        if (sprites != null && sprites.Length > 0 && targetImage != null)
        {
            targetImage.sprite = sprites[currentIndex];
        }
        if (backButton != null)
            backButton.interactable = currentIndex > 0;
        // Keep nextButton interactable until tutorial is finished
        if (nextButton != null)
            nextButton.interactable = true;
    }

}
