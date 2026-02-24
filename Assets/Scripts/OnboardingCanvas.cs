using DG.Tweening;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using System.Collections.Generic;
using System.Linq;

public class OnboardingCanvas : MonoBehaviour
{
    [SerializeField] Image Content;
    [SerializeField] RectTransform centerTransform;
    [SerializeField] RectTransform outOfScreenTransform;
    [SerializeField] float moveDownSpeed = .3f;
    [SerializeField] float moveUpSpeed = .5f;

    [Header("DYKContent")]
    [SerializeField] GameObject DYKContent;
    [SerializeField] Image DYKheaderImage;
    [SerializeField] TextMeshProUGUI DYKHeaderText;
    [SerializeField] Button DYKYesBtn;
    [SerializeField] TextMeshProUGUI DYKYesBtnText;
    [SerializeField] Button DYKNoBtn;
    [SerializeField] TextMeshProUGUI DYKNoBtnText;


    [Header("AskMobileContent")]
    [SerializeField] GameObject AskMobileContent;
    [SerializeField] Image AskMobileHeaderImage;
    [SerializeField] TextMeshProUGUI AskMobileHeaderText;
    [SerializeField] Button AskMobileYesBtn;
    [SerializeField] TextMeshProUGUI AskMobileYesBtnText;
    [SerializeField] Button AskMobileNoBtn;
    [SerializeField] TextMeshProUGUI AskMobileNoBtnText;

    [Header("Debug")]
    [SerializeField] bool useDebug = false;

    ImageSwitcher helpGuide;
    List<DisableIfNotOnMobile> mobileUIs = new List<DisableIfNotOnMobile>();

    private void Awake()
    {
        helpGuide = FindAnyObjectByType<ImageSwitcher>();
        mobileUIs = FindObjectsByType<DisableIfNotOnMobile>
            (FindObjectsInactive.Include, FindObjectsSortMode.None).ToList();
    }

    private void Start()
    {
        if (OnboardingManager.Instance.knowsHowToPlay == false || useDebug)
            OpenDYKWindow();
    }

    public void OpenDYKWindow()
    {
        PauseManager.Instance.Pause();
        UIActivationManager.Instance.DeactivateOtherUI(gameObject);

        AudioManager.Instance.PlayUISwipeInSFX();

        Content.color = new Color(0, 0, 0, 0);
        Content.enabled = true;

        DYKheaderImage.color = new Color(1, 1, 1, 0);
        DYKHeaderText.color = new Color(1, 1, 1, 0);
        DYKYesBtn.image.color = new Color(1, 1, 1, 0);
        DYKYesBtnText.color = new Color(1, 1, 1, 0);
        DYKNoBtn.image.color = new Color(1, 1, 1, 0);
        DYKNoBtnText.color = new Color(1, 1, 1, 0);

        DYKContent.SetActive(true);
        DYKContent.transform.DOMove(centerTransform.position, moveDownSpeed);
        Content.DOColor(new Color(0, 0, 0, .5f), moveDownSpeed);
        DYKheaderImage.DOColor(Color.white, moveDownSpeed);
        DYKHeaderText.DOColor(Color.white, moveDownSpeed);
        DYKYesBtn.image.DOColor(Color.white, moveDownSpeed);
        DYKYesBtnText.DOColor(Color.white, moveDownSpeed);
        DYKNoBtn.image.DOColor(Color.white, moveDownSpeed);
        DYKNoBtnText.DOColor(Color.white, moveDownSpeed);
    }

    public void CloseDYKWindow()
    {
        AudioManager.Instance.PlayUISwipeOutSFX();

        DYKheaderImage.DOColor(new Color(1, 1, 1, 0), moveUpSpeed);
        DYKHeaderText.DOColor(new Color(1, 1, 1, 0), moveUpSpeed); ;
        DYKYesBtn.image.DOColor(new Color(1, 1, 1, 0), moveUpSpeed);
        DYKYesBtnText.DOColor(new Color(1, 1, 1, 0), moveUpSpeed);
        DYKNoBtn.image.DOColor(new Color(1, 1, 1, 0), moveUpSpeed);
        DYKNoBtnText.DOColor(new Color(1, 1, 1, 0), moveUpSpeed);
        DYKContent.transform.DOMove(outOfScreenTransform.position, moveUpSpeed)
            .OnComplete
            (
                () =>
                {
                    DYKContent.SetActive(false);
                    OpenAskWindow();
                }
            );
    }

    private void OpenAskWindow()
    {
        AudioManager.Instance.PlayUISwipeInSFX();

        AskMobileHeaderImage.color = new Color(1, 1, 1, 0);
        AskMobileHeaderText.color = new Color(1, 1, 1, 0);
        AskMobileYesBtn.image.color = new Color(1, 1, 1, 0);
        AskMobileYesBtnText.color = new Color(1, 1, 1, 0);
        AskMobileNoBtn.image.color = new Color(1, 1, 1, 0);
        AskMobileNoBtnText.color = new Color(1, 1, 1, 0);

        AskMobileContent.SetActive(true);

        AskMobileContent.transform.DOMove(centerTransform.position, moveDownSpeed);
        AskMobileHeaderImage.DOColor(Color.white, moveDownSpeed);
        AskMobileHeaderText.DOColor(Color.white, moveDownSpeed);
        AskMobileYesBtn.image.DOColor(Color.white, moveDownSpeed);
        AskMobileYesBtnText.DOColor(Color.white, moveDownSpeed);
        AskMobileNoBtn.image.DOColor(Color.white, moveDownSpeed);
        AskMobileNoBtnText.DOColor(Color.white, moveDownSpeed);
    }

    public void CloseAskWindow()
    {
        AudioManager.Instance.PlayUISwipeOutSFX();

        AskMobileHeaderImage.DOColor(Color.white, moveUpSpeed);
        AskMobileHeaderText.DOColor(Color.white, moveUpSpeed);
        AskMobileYesBtn.image.DOColor(Color.white, moveUpSpeed);
        AskMobileYesBtnText.DOColor(Color.white, moveUpSpeed);
        AskMobileNoBtn.image.DOColor(Color.white, moveUpSpeed);
        AskMobileNoBtnText.DOColor(Color.white, moveUpSpeed);
        Content.DOColor(new Color(0, 0, 0, 0f), moveUpSpeed);
        AskMobileContent.transform.DOMove(outOfScreenTransform.position, moveUpSpeed)
            .OnComplete
            (
                () => 
                {
                    Content.enabled = false;
                    AskMobileContent.SetActive(false);
                    EndOnboarding();
                }
            )
        ;
    }

    private void EndOnboarding()
    {
        PauseManager.Instance.Unpause();
        UIActivationManager.Instance.ActivateOtherUI(gameObject);

        if (OnboardingManager.Instance.knowsHowToPlay == false)
        {
            helpGuide.OpenGuide();
            OnboardingManager.Instance.knowsHowToPlay = true;
        }

        for (int i = 0; mobileUIs.Count > 0; i++)
        {
            mobileUIs[i].gameObject.SetActive(true);
            mobileUIs[i].ShowMobileControlsUI();
        }
    }

    public void OnDYKYesButtonPress()
    {
        OnboardingManager.Instance.knowsHowToPlay = true;
    }

    public void OnAskMobileYesButtonPress()
    {
        OnboardingManager.Instance.isUsingMobileDevice = true;
    }
}
