using UnityEngine;
using DG.Tweening;

public class ScalingAnimation : MonoBehaviour
{

    [Tooltip("Duration for scaling up or down (seconds)")]
    public float scaleDuration = 2f;

    private Tweener scaleTween;

    void Start()
    {
        StartScaling();
    }

    void StartScaling()
    {
        // Kill any previous tween
        if (scaleTween != null && scaleTween.IsActive())
            scaleTween.Kill();
        // Use DOTween to scale to 1.5 and back to 1 non-stop
        scaleTween = transform.DOScale(1.5f, scaleDuration)
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
