using UnityEngine;
using DG.Tweening;

public class PauseHeaderAnimator : MonoBehaviour
{
    [SerializeField] float YOffset = -15f;
    [SerializeField] float Duration = 1f;
    Vector3 initialPosition;

    private void Awake()
    {
        initialPosition = transform.localPosition;
    }

    private void OnEnable()
    {
        transform.DOLocalMoveY(initialPosition.y - YOffset, Duration).
            SetLoops(-1, LoopType.Yoyo).
            SetEase(Ease.Linear);
    }
}
