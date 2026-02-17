using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class FeedbackCanvas : MonoBehaviour
{
    private static FeedbackCanvas instance;
    public static FeedbackCanvas Instance
    {
        get
        {
            if (instance == null)
            {
                GameObject newGameObject = Instantiate(Resources.Load<GameObject>("Minigames/FeedbackCanvas"));
                instance = newGameObject.GetComponent<FeedbackCanvas>();
            }
            return instance;
        }
    }

    public Image correctImage; // Assign in Inspector
    public Image wrongImage;   // Assign in Inspector
    public float animationDuration = 0.1f;
    public float displayDuration = 0.4f;

    private Coroutine currentCoroutine;

    private void Awake()
    {
        instance = this;
    }

    public void ShowCorrect()
    {
        if (currentCoroutine != null) StopCoroutine(currentCoroutine);
        currentCoroutine = StartCoroutine(AnimateImage(correctImage));
        AudioManager.Instance.PlayCorrectSFX();
    }

    public void ShowWrong()
    {
        if (currentCoroutine != null) StopCoroutine(currentCoroutine);
        currentCoroutine = StartCoroutine(AnimateImage(wrongImage));
        AudioManager.Instance.PlayIncorrectSFX();
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
