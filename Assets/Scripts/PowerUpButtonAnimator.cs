using UnityEngine;
using UnityEngine.EventSystems;
using DG.Tweening;
using UnityEngine.UI;

[RequireComponent(typeof(Button))]
public class PowerUpButtonAnimator : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler, IPointerDownHandler, IPointerUpHandler
{
    [Header("Animation Settings")]
    public float hoverScale = 1.15f;
    public float clickScale = 0.95f;
    public float animDuration = 0.15f;
    public Ease animEase = Ease.OutBack;

    private Vector3 originalScale;
    private bool isPointerDown = false;

    PowerUpButton powerUpButton;

    void Awake()
    {
        originalScale = transform.localScale;
        powerUpButton = GetComponent<PowerUpButton>();
    }

    public void OnPointerEnter(PointerEventData eventData)
    {
        if (!isPointerDown)
        {
            transform.DOScale(originalScale * hoverScale, animDuration).SetEase(animEase);
                AudioManager.Instance.PlayUIHoverSFX();
        }
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        if (!isPointerDown)
            transform.DOScale(originalScale, animDuration).SetEase(animEase);
    }

    public void OnPointerDown(PointerEventData eventData)
    {
        isPointerDown = true;
        transform.DOScale(originalScale * clickScale, animDuration * 0.7f).SetEase(Ease.InOutQuad);
    }

    public void OnPointerUp(PointerEventData eventData)
    {
        isPointerDown = false;
        transform.DOScale(originalScale * hoverScale, animDuration).SetEase(animEase);
        if (powerUpButton.Button.interactable == false)
        {
            AudioManager.Instance.PlayUIInactiveButtonSFX();
        }
    }
}
