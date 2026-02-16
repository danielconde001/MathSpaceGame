using UnityEngine;

using UnityEngine.UI;
using TMPro;

public class EndScreenManager : MonoBehaviour
{
    [Header("UI References")]
    public Button HomeButton;
    public Image Star1;
    public Image Star2;
    public Image Star3;
    [Header("Newton Images")]
    public Image NewtonImage;
    public Sprite[] NewtonSprites; // 0: 1 star, 1: 2 stars, 2: 3 stars
    public TextMeshProUGUI NewtonText;

    private void Start()
    {
        int score = PlayerPrefs.GetInt("PlayerScore", 0);
        Debug.Log($"[EndScreenManager] Loaded PlayerScore: {score}");
        SetNewtonText(score);
        SetNewtonImage(score);
        HomeButton.onClick.AddListener(OnHomeButtonPressed);
        StartCoroutine(AnimateStars(score));
    }

    private System.Collections.IEnumerator AnimateStars(int score)
    {
        // Hide all stars initially
        Star1.gameObject.SetActive(false);
        Star2.gameObject.SetActive(false);
        Star3.gameObject.SetActive(false);

        int starsToShow = 1;
        if (score > 75)
            starsToShow = 3;
        else if (score > 30)
            starsToShow = 2;

        float delay = 0.5f;
        if (starsToShow >= 1)
        {
            yield return StartCoroutine(PopStar(Star1));
            yield return new WaitForSeconds(delay);
        }
        if (starsToShow >= 2)
        {
            yield return StartCoroutine(PopStar(Star2));
            yield return new WaitForSeconds(delay);
        }
        if (starsToShow == 3)
        {
            yield return StartCoroutine(PopStar(Star3));
        }
    }

    private System.Collections.IEnumerator PopStar(Image star)
    {
        star.gameObject.SetActive(true);
        RectTransform rt = star.GetComponent<RectTransform>();
        if (rt == null) yield break;
        Vector3 originalScale = rt.localScale;
        rt.localScale = Vector3.zero;
        float popTime = 0.15f;
        float elapsed = 0f;
        while (elapsed < popTime)
        {
            float t = elapsed / popTime;
            // Ease out back for pop effect
            float scale = 1.2f * Mathf.Sin(t * Mathf.PI * 0.5f);
            rt.localScale = new Vector3(scale, scale, 1f);
            elapsed += Time.unscaledDeltaTime;
            yield return null;
        }
        rt.localScale = originalScale;
    }

    private void OnHomeButtonPressed()
    {
        UnityEngine.SceneManagement.SceneManager.LoadScene("MainMenu");
    }

    private void SetNewtonImage(int score)
    {
        if (NewtonSprites == null || NewtonSprites.Length < 3 || NewtonImage == null)
            return;

        // Set sprite
        if (score > 75)
        {
            NewtonImage.sprite = NewtonSprites[2]; // 3 stars
        }
        else if (score > 30)
        {
            NewtonImage.sprite = NewtonSprites[1]; // 2 stars
        }
        else
        {
            NewtonImage.sprite = NewtonSprites[0]; // 1 star
        }

        // Keep size uniform
        // Set the rectTransform size to the default (set in Inspector)
        RectTransform rt = NewtonImage.GetComponent<RectTransform>();
        if (rt != null)
        {
            // Replace these values with your default size if needed
            rt.sizeDelta = new Vector2(567, 693); // Example default size
        }
    }

    private void SetNewtonText(int score)
    {
        if (score > 75)
        {
            NewtonText.text = "You did amazing!";
        }
        else if (score > 30)
        {
            NewtonText.text = "Good job!";
        }
        else
        {
            NewtonText.text = "Not bad.";
        }
    }

}
