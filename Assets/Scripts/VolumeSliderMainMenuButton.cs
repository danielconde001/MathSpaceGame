using UnityEngine;
using DG.Tweening;

public class VolumeSliderMainMenuButton : MonoBehaviour
{
    [SerializeField] private GameObject sliderObj;

    private bool isShowing = false;

    public void OnClick()
    {
        if (isShowing)
            DoExit();
        else
        {
            DoEntrance();
        }
    }

    private void DoEntrance()
    {
        sliderObj.SetActive(true);
        AudioManager.Instance.PlayUIPopSFX();
        sliderObj.transform.DOScale(Vector3.zero, 0f);
        sliderObj.transform.DOScale(Vector3.one, .5f).SetEase(Ease.OutBack);

        isShowing = true;
    }

    private void DoExit()
    {
        AudioManager.Instance.PlayUIReversePopSFX();
        sliderObj.transform.DOScale(Vector3.zero, .2f)
            .OnComplete( () =>
            {
                sliderObj.SetActive(false);
                isShowing = false;
            });
    }
}
