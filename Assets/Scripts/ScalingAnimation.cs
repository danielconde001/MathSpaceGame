using UnityEngine;
using DG.Tweening;

public class ScalingAnimation : MonoBehaviour
{

    [Tooltip("Duration for scaling up or down (seconds)")]
    public float scaleDuration = 2f;

    [Tooltip("The scale to start from (scaled up)")]
    public float startScale = 1.5f;

    private Tweener scaleTween;

    void Awake()
    {
        // Set the initial scale to the scaled-up value
        transform.localScale = Vector3.one * startScale;
    }

    void Start()
    {
        StartScaling();
    }

    void StartScaling()
    {
        // Kill any previous tween
        if (scaleTween != null && scaleTween.IsActive())
            scaleTween.Kill();
        // Use DOTween to scale from startScale to 1 and back, non-stop
        scaleTween = transform.DOScale(Vector3.one, scaleDuration)
            .From(Vector3.one * startScale)
            .SetLoops(-1, LoopType.Yoyo);
    }

    void OnEnable()
    {
        StartScaling();
    }

    void OnDisable()
    {
        // Reset to default scale before killing tween
        transform.localScale = Vector3.one;
        if (scaleTween != null && scaleTween.IsActive())
            scaleTween.Kill();
    }

    void OnDestroy()
    {
        // Reset to default scale before killing tween
        transform.localScale = Vector3.one;
        if (scaleTween != null && scaleTween.IsActive())
            scaleTween.Kill();
    }
}
