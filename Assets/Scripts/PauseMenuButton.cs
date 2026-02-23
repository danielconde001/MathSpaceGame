using DG.Tweening;
using UnityEngine;
using UnityEngine.EventSystems;
using System.Collections.Generic;

public class PauseMenuButton : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler, IPointerDownHandler, IPointerUpHandler
{
    [SerializeField]
    private List<GameObject> textsToDisableOnHover = new List<GameObject>();

    [Header("Animation Settings")]
    public float hoverScale = 1.15f;
    public float clickScale = 0.95f;
    public float animDuration = 0.15f;
    public Ease animEase = Ease.OutBack;
    

    private Vector3 originalScale;
    private bool isPointerDown = false;

    private void Awake()
    {
        originalScale = transform.localScale;
    }

    public void OnPointerEnter(PointerEventData eventData)
    {
        if (!isPointerDown)
        {
            transform.DOScale(originalScale * hoverScale, animDuration).SetEase(animEase);

            if (textsToDisableOnHover.Count > 0)
            {
                for (int i = 0; i < textsToDisableOnHover.Count; i++)
                {
                    textsToDisableOnHover[i].SetActive(false);
                } 
            }
            AudioManager.Instance.PlayUIHoverSFX();
        }
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        if (!isPointerDown)
        {
            transform.DOScale(originalScale, animDuration).SetEase(animEase);

            if (textsToDisableOnHover.Count > 0)
            {
                for (int i = 0; i < textsToDisableOnHover.Count; i++)
                {
                    textsToDisableOnHover[i].SetActive(true);
                }
            }
        }
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
    }
}
