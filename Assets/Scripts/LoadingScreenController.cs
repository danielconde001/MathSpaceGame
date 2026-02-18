using System.Collections;
using UnityEngine;

using UnityEngine.SceneManagement;
using UnityEngine.UI;
using TMPro;


public class LoadingScreenController : MonoBehaviour
{
    public float minLoadingTime = 1f; // Minimum time to show loading screen
    public Image loadingBarImage;
    public float maxWidth = 1920f; // Max width when fully loaded
    public TextMeshProUGUI topicText;

    private RectTransform barRect;
    private string gameSceneName = "Level 1";

    // Mapping topics to scene names
    private readonly System.Collections.Generic.Dictionary<string, string> topicToScene = new System.Collections.Generic.Dictionary<string, string>()
    {
        {"Counting up to 100", "Level 1"},
        {"Number Patterns", "Level 2"},
        {"Comparing and Ordering Numbers", "Level 3"}
    };

    void Start()
    {
        if (loadingBarImage != null)
        {
            barRect = loadingBarImage.rectTransform;
            SetBarWidth(0);
        }
        // Set the topic text if assigned and determine scene
        string topic = PlayerPrefs.GetString("SelectedTopic", "");
        if (topicText != null)
        {
            topicText.text = string.IsNullOrEmpty(topic) ? "" : topic;
        }
        // Map topic to scene name, fallback to Level 1 if not found
        if (!string.IsNullOrEmpty(topic) && topicToScene.ContainsKey(topic))
        {
            gameSceneName = topicToScene[topic];
        }
        else
        {
            Debug.LogWarning($"SelectedTopic '{topic}' not found in topicToScene mapping. Defaulting to 'Level 1'.");
            gameSceneName = "Level 1";
        }
        StartCoroutine(LoadGameSceneAsync());
    }

    IEnumerator LoadGameSceneAsync()
    {
        float startTime = Time.time;
        AsyncOperation asyncLoad = SceneManager.LoadSceneAsync(gameSceneName);
        asyncLoad.allowSceneActivation = false;

        float barProgress = 0f;
        while (!asyncLoad.isDone)
        {
            float elapsed = Time.time - startTime;
            float sceneProgress = Mathf.Clamp01(asyncLoad.progress / 0.9f);
            float timeProgress = Mathf.Clamp01(elapsed / minLoadingTime);
            // The bar should fill according to the slower of the two: scene loading or min time
            barProgress = Mathf.Min(sceneProgress, timeProgress);
            if (loadingBarImage != null)
                SetBarWidth(barProgress * maxWidth);

            // Only allow scene activation when both are complete
            if (sceneProgress >= 1f && timeProgress >= 1f)
            {
                if (loadingBarImage != null)
                    SetBarWidth(maxWidth);
                asyncLoad.allowSceneActivation = true;
            }
            yield return null;
        }
    }

    void SetBarWidth(float width)
    {
        if (barRect != null)
        {
            barRect.sizeDelta = new Vector2(width, barRect.sizeDelta.y);
        }
    }
}
