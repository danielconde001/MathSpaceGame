
using UnityEngine;
using DG.Tweening;

public class ScrollBackground : MonoBehaviour
{

    public RectTransform targetRect;
    public Vector2 scrollDirection = new Vector2(-1, 1); // Up and left
    public float scrollSpeed = 10f;
    private Vector2 startPosition;
    private float width;
    private float height;

    void Start()
    {
        if (targetRect == null)
            targetRect = GetComponent<RectTransform>();
        startPosition = targetRect.anchoredPosition;
        width = targetRect.rect.width;
        height = targetRect.rect.height;
        StartScrolling();
    }

    private Tweener scrollTween;

    void StartScrolling()
    {
        if (targetRect == null)
            return;

        Vector2 endPosition = startPosition + new Vector2(scrollDirection.x * width, scrollDirection.y * height);
        float distance = (endPosition - startPosition).magnitude;
        float duration = distance / scrollSpeed;
        targetRect.anchoredPosition = startPosition;

        // Kill any previous tween
        if (scrollTween != null && scrollTween.IsActive())
            scrollTween.Kill();

        scrollTween = targetRect.DOAnchorPos(endPosition, duration)
            .SetEase(Ease.Linear)
            .OnUpdate(() =>
            {
                if (targetRect == null)
                    return;
                // Check if we've passed halfway
                float progress = (targetRect.anchoredPosition - startPosition).magnitude / distance;
                if (progress >= 0.3f)
                {
                    if (scrollTween != null && scrollTween.IsActive())
                        scrollTween.Kill();
                    // Reset position to start before starting again
                    targetRect.anchoredPosition = startPosition;
                    StartScrolling();
                }
            });
    }

    void OnDisable()
    {
        if (scrollTween != null && scrollTween.IsActive())
        {
            scrollTween.Kill();
        }
    }

        void OnEnable()
    {
        // Restart scrolling when the object is enabled again
        if (targetRect == null)
            targetRect = GetComponent<RectTransform>();
        StartScrolling();
    }
    
    void OnDestroy()
    {
        if (scrollTween != null && scrollTween.IsActive())
        {
            scrollTween.Kill();
        }
    }
}
