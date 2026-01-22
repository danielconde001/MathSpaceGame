using UnityEngine;
using DG.Tweening;

public class TestScript : MonoBehaviour
{
    public Transform playerTransform;
 public void moveButtonTest()
 {
    playerTransform.DORotate(new Vector3(0, 180, 0), 1f).SetLoops(-1, LoopType.Restart).SetEase(Ease.Linear);
 }
}
