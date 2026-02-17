using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class FeedbackAnimator : MonoBehaviour
{
    public Image correctImage; // Assign in Inspector
    public Image wrongImage;   // Assign in Inspector
    public float animationDuration = 0.5f;
    public float displayDuration = 1.0f;

    private Coroutine currentCoroutine;

    public void ShowCorrect()
    {
        if (currentCoroutine != null) StopCoroutine(currentCoroutine);
        currentCoroutine = StartCoroutine(AnimateImage(correctImage));
    }

    public void ShowWrong()
    {
        if (currentCoroutine != null) StopCoroutine(currentCoroutine);
        currentCoroutine = StartCoroutine(AnimateImage(wrongImage));
    }

    private IEnumerator AnimateImage(Image img)
    {
        // Hide both images first
        if (correctImage != null) correctImage.gameObject.SetActive(false);
        if (wrongImage != null) wrongImage.gameObject.SetActive(false);

        img.gameObject.SetActive(true);
        Color c = img.color;
        c.a = 0f;
        img.color = c;
        img.transform.localScale = Vector3.one * 0.7f; // Start smaller for pop

        // Fade and pop in
        float t = 0f;
        while (t < animationDuration)
        {
            t += Time.deltaTime;
            float progress = t / animationDuration;
            c.a = Mathf.Lerp(0f, 1f, progress);
            img.color = c;
            // Pop scale: overshoot and settle
            float scale = Mathf.Lerp(0.7f, 1.15f, progress); // Overshoot
            img.transform.localScale = Vector3.one * scale;
            yield return null;
        }
        c.a = 1f;
        img.color = c;
        img.transform.localScale = Vector3.one * 1.15f;

        // Quick settle to normal scale
        float settleTime = 0.12f;
        t = 0f;
        while (t < settleTime)
        {
            t += Time.deltaTime;
            float progress = t / settleTime;
            float scale = Mathf.Lerp(1.15f, 1f, progress);
            img.transform.localScale = Vector3.one * scale;
            yield return null;
        }
        img.transform.localScale = Vector3.one;

        // Wait
        yield return new WaitForSeconds(displayDuration);

        // Fade and pop out (shrink)
        t = 0f;
        Vector3 startScale = img.transform.localScale;
        while (t < animationDuration)
        {
            t += Time.deltaTime;
            float progress = t / animationDuration;
            c.a = Mathf.Lerp(1f, 0f, progress);
            img.color = c;
            float scale = Mathf.Lerp(1f, 0.7f, progress);
            img.transform.localScale = Vector3.one * scale;
            yield return null;
        }
        c.a = 0f;
        img.color = c;
        img.transform.localScale = Vector3.one;
        img.gameObject.SetActive(false);
    }
}
