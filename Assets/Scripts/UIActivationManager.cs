using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class UIActivationManager : MonoBehaviour
{
    private static UIActivationManager instance;
    public static UIActivationManager Instance
    {

        get
        {
            if (instance == null)
            {
                GameObject newGameObject = new GameObject("UIActivationManager");
                instance = newGameObject.AddComponent<UIActivationManager>();
            }
            return instance;
        }
    }

    Canvas powerUpManager;
    Canvas mobileCanvas;
    Canvas gameOverCanvas;
    Canvas scoreCanvas;
    Canvas helpGuideCanvas;
    Canvas dialogBox;
    Canvas playerHealthCanvas;
    Canvas pauseManager;
    Canvas onboardingCanvas;

    private void Start()
    {
        SearchForReferences();
    }

    private void SearchForReferences()
    {
        powerUpManager = FindAnyObjectByType<PowerUpManager>(FindObjectsInactive.Include)?.gameObject.GetComponent<Canvas>();
        mobileCanvas = GameObject.Find("MobileCanvas")?.GetComponent<Canvas>();
        gameOverCanvas = FindAnyObjectByType<GameOverManager>(FindObjectsInactive.Include)?.gameObject.GetComponent<Canvas>();
        scoreCanvas = FindAnyObjectByType<ScoreManager>(FindObjectsInactive.Include)?.gameObject.GetComponent<Canvas>();
        helpGuideCanvas = GameObject.Find("HelpGuideCanvas")?.GetComponent<Canvas>();
        dialogBox = FindAnyObjectByType<DialogueManager>(FindObjectsInactive.Include)?.gameObject.GetComponent<Canvas>();
        playerHealthCanvas = PlayerManager.Instance.GetPlayer()?.PlayerHealthBar.gameObject.GetComponent<Canvas>();
        pauseManager = FindAnyObjectByType<PauseManager>(FindObjectsInactive.Include)?.gameObject.GetComponent<Canvas>();
        onboardingCanvas = FindAnyObjectByType<OnboardingCanvas>(FindObjectsInactive.Include)?.gameObject.GetComponent<Canvas>();
    }

    public void DeactivateOtherUI(GameObject p_activator = null)
    {
        if (powerUpManager == null)
            SearchForReferences();
        else if (powerUpManager.gameObject != p_activator)
            powerUpManager.enabled = false;

        if (mobileCanvas == null)
            SearchForReferences();
        else if (mobileCanvas.gameObject != p_activator)
            mobileCanvas.enabled = false;

        if (gameOverCanvas == null)
            SearchForReferences();
        else if (gameOverCanvas.gameObject != p_activator)
            gameOverCanvas.enabled = false;

        if (scoreCanvas == null)
            SearchForReferences();
        else if (scoreCanvas.gameObject != p_activator)
            scoreCanvas.enabled = false;

        if (helpGuideCanvas == null)
            SearchForReferences();
        else if (helpGuideCanvas.gameObject != p_activator)
            helpGuideCanvas.enabled = false;

        if (dialogBox == null)
            SearchForReferences();
        else if (dialogBox.gameObject != p_activator)
            dialogBox.enabled = false;

        if (playerHealthCanvas == null)
            SearchForReferences();
        else if (playerHealthCanvas.gameObject != p_activator)
            playerHealthCanvas.enabled = false;

        if (pauseManager == null)
            SearchForReferences();
        else if (pauseManager.gameObject != p_activator)
            pauseManager.enabled = false;

        if (onboardingCanvas == null)
            SearchForReferences();
        else if (onboardingCanvas.gameObject != p_activator)
            onboardingCanvas.enabled = false;
    }

    public void ActivateOtherUI(GameObject p_activator = null)
    {
        if (powerUpManager == null)
            SearchForReferences();
        else if (powerUpManager.gameObject != p_activator)
            powerUpManager.enabled = true;

        if (mobileCanvas == null)
            SearchForReferences();
        else if (mobileCanvas.gameObject != p_activator)
            mobileCanvas.enabled = true;

        if (gameOverCanvas == null)
            SearchForReferences();
        else if (gameOverCanvas.gameObject != p_activator)
            gameOverCanvas.enabled = true;

        if (scoreCanvas == null)
            SearchForReferences();
        else if (scoreCanvas.gameObject != p_activator)
            scoreCanvas.enabled = true;

        if (helpGuideCanvas == null)
            SearchForReferences();
        else if (helpGuideCanvas.gameObject != p_activator)
            helpGuideCanvas.enabled = true;

        if (dialogBox == null)
            SearchForReferences();
        else if (dialogBox.gameObject != p_activator)
            dialogBox.enabled = true;

        if (playerHealthCanvas == null)
            SearchForReferences();
        else if (playerHealthCanvas.gameObject != p_activator)
            playerHealthCanvas.enabled = true;

        if (pauseManager == null)
            SearchForReferences();
        else if (pauseManager.gameObject != p_activator)
            pauseManager.enabled = true;

        if (onboardingCanvas == null)
            SearchForReferences();
        else if (onboardingCanvas.gameObject != p_activator)
            onboardingCanvas.enabled = true;
    }
}
