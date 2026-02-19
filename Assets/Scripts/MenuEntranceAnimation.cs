using UnityEngine;
using DG.Tweening;

public class MenuEntranceAnimation : MonoBehaviour
{
    [Header("Animation Settings")]
    public float startScale = 1.5f;
    public float animDuration = 0.4f;
    public Ease animEase = Ease.OutBack;

    private Vector3 originalScale;
    [Header("Final Scale (leave as 0,0,0 to use prefab scale)")]
    public Vector3 finalScale = Vector3.zero;

    public Vector3 GetOriginalScale() => originalScale;
    private Tweener entranceTween;

    void Awake()
    {
        // Store the prefab's original scale
        originalScale = transform.localScale;
        // If not set in Inspector, use prefab scale as final scale (for backward compatibility)
        if (finalScale == Vector3.zero)
            finalScale = originalScale;
    }

    void Start()
    {
        // Set to entrance start scale, but always animate back to originalScale
        transform.localScale = originalScale * startScale;
        PlayEntrance();
    }

    public void PlayEntrance()
    {
        // Kill any previous entrance tween to avoid conflicts
        if (entranceTween != null && entranceTween.IsActive())
            entranceTween.Kill();
        // Animate to the original prefab scale
        entranceTween = transform.DOScale(originalScale, animDuration).SetEase(animEase);
    }

    void OnDestroy()
    {
        if (entranceTween != null && entranceTween.IsActive())
            entranceTween.Kill();
        transform.DOKill();
    }
}
