using UnityEngine;
using UnityEngine.EventSystems;
using DG.Tweening;
using UnityEngine.UI;

[RequireComponent(typeof(Button))]
public class MenuButtonsAnimator : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler, IPointerDownHandler, IPointerUpHandler
{
    [Header("Animation Settings")]
    public float hoverScale = 1.2f;
    public float clickScale = 0.95f;
    public float animDuration = 0.15f;
    public Ease animEase = Ease.OutBack;

    [Header("Image Swap Settings")]
    public Image targetImage; // Optional reference, auto-assigned if not set
    public Sprite hoverSprite; // Sprite to use on hover
    public bool doHoverAnimation = false;
    private Sprite originalSprite;

    private Vector3 originalScale;
    private bool isPointerDown = false;

    PowerUpButton powerUpButton;

    void Awake()
    {
        powerUpButton = GetComponent<PowerUpButton>();
        if (targetImage == null)
        {
            targetImage = GetComponent<Image>();
        }
        if (targetImage != null)
        {
            originalSprite = targetImage.sprite;
        }
    }

    void Start()
    {
        // Use the finalScale from MenuEntranceAnimation if present, otherwise use current localScale
        var menuEntrance = GetComponent<MenuEntranceAnimation>();
        if (menuEntrance != null)
        {
            originalScale = menuEntrance.GetOriginalScale();
        }
        else
        {
            originalScale = transform.localScale;
        }
    }

    public void OnPointerEnter(PointerEventData eventData)
    {
        if (!isPointerDown)
        {
            if (targetImage != null && hoverSprite != null && targetImage.sprite != null)
            {
                targetImage.sprite = hoverSprite;
                if (doHoverAnimation)
                    transform.DOScale(originalScale * hoverScale, animDuration).SetEase(animEase);
            }
            else
            {
                transform.DOScale(originalScale * hoverScale, animDuration).SetEase(animEase);
            }
            AudioManager.Instance.PlayUIHoverSFX();
        }
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        if (!isPointerDown)
        {
            if (targetImage != null && hoverSprite != null && targetImage.sprite == hoverSprite)
            {
                targetImage.sprite = originalSprite;
                if (doHoverAnimation)
                    transform.DOScale(originalScale, animDuration).SetEase(animEase);
            }
            else
            {
                transform.DOScale(originalScale, animDuration).SetEase(animEase);
            }
        }
    }

    public void OnPointerDown(PointerEventData eventData)
    {
        isPointerDown = true;
        transform.DOScale(originalScale * clickScale, animDuration * 0.7f).SetEase(Ease.InOutQuad);
        AudioManager.Instance.PlayUIClickButtonSFX();
    }

    public void OnPointerUp(PointerEventData eventData)
    {
        isPointerDown = false;
        transform.DOScale(originalScale * hoverScale, animDuration).SetEase(animEase);
        if (powerUpButton != null && powerUpButton.Button != null && powerUpButton.Button.interactable == false)
        {
            AudioManager.Instance.PlayUIHoverSFX();
        }
    }
    
    void OnDestroy()
    {
        // Kill any tweens on this transform to prevent DOTween errors if destroyed mid-animation
        transform.DOKill();
    }
}
