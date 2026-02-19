using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;
using DG.Tweening;

public class NumberImageAnimator : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler, IPointerDownHandler, IPointerUpHandler, IBeginDragHandler, IEndDragHandler
{
    [Header("Animation Settings")]
    public float hoverScale = 1.1f;
    public float clickScale = 0.95f;
    public float dragScale = 1.15f;
    public float animDuration = 0.15f;
    public Ease animEase = Ease.OutBack;
    public Color hoverColor = new Color(1f, 0.95f, 0.7f, 1f);
    public Color dragColor = new Color(0.8f, 0.9f, 1f, 1f);
    public Color clickColor = new Color(0.9f, 0.9f, 0.9f, 1f);

    private Vector3 originalScale;
    private Color originalColor;
    private Image image;
    private bool isPointerDown = false;
    private bool isDragging = false;

    void Awake()
    {
        originalScale = transform.localScale;
        image = GetComponent<Image>();
        if (image != null)
            originalColor = image.color;
    }

    public void OnPointerEnter(PointerEventData eventData)
    {
        if (isDragging) return;
        if (!isPointerDown)
        {
            transform.DOScale(originalScale * hoverScale, animDuration).SetEase(animEase);
            if (image != null)
                image.DOColor(hoverColor, animDuration);
            AudioManager.Instance.PlayUIHoverSFX();
        }
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        if (isDragging) return;
        if (!isPointerDown)
        {
            transform.DOScale(originalScale, animDuration).SetEase(animEase);
            if (image != null)
                image.DOColor(originalColor, animDuration);
        }
    }

    public void OnPointerDown(PointerEventData eventData)
    {
        isPointerDown = true;
        transform.DOScale(originalScale * clickScale, animDuration * 0.7f).SetEase(Ease.InOutQuad);
        if (image != null)
            image.DOColor(clickColor, animDuration * 0.7f);
            AudioManager.Instance.PlayUIClickButtonSFX();
    }

    public void OnPointerUp(PointerEventData eventData)
    {
        isPointerDown = false;
        if (!isDragging)
        {
            transform.DOScale(originalScale * hoverScale, animDuration).SetEase(animEase);
            if (image != null)
                image.DOColor(hoverColor, animDuration);
        }
    }

    public void OnBeginDrag(PointerEventData eventData)
    {
        isDragging = true;
        transform.DOScale(originalScale * dragScale, animDuration).SetEase(animEase);
        if (image != null)
            image.DOColor(dragColor, animDuration);
    }

    public void OnEndDrag(PointerEventData eventData)
    {
        isDragging = false;
        transform.DOScale(originalScale, animDuration).SetEase(animEase);
        if (image != null)
            image.DOColor(originalColor, animDuration);
        AudioManager.Instance.PlayUIDropButtonSFX();
    }
}
