using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;

public class SceneFader : MonoBehaviour
{
    public Image fadeImage; // Assign a full-screen black UI Image
    public float fadeDuration = 1f;

    void Awake()
    {
        if (fadeImage != null)
        {
            Color c = fadeImage.color;
            c.a = 1f;
            fadeImage.color = c;
            fadeImage.gameObject.SetActive(true);
        }
    }

    public void FadeIn(System.Action onComplete = null)
    {
        if (fadeImage != null)
        {
            fadeImage.DOFade(0f, fadeDuration).OnComplete(() => {
                fadeImage.gameObject.SetActive(false);
                if (onComplete != null) onComplete();
            });
        }
        else { if (onComplete != null) onComplete(); }
    }

    public void FadeOut(System.Action onComplete = null)
    {
        if (fadeImage != null)
        {
            fadeImage.gameObject.SetActive(true);
            fadeImage.DOFade(1f, fadeDuration).OnComplete(() => {
                if (onComplete != null) onComplete();
            });
        }
        else { if (onComplete != null) onComplete(); }
    }
}
