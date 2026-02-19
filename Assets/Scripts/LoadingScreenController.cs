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
    public Image topicImage;
    public Sprite[] topicImages; // Assign 3 images in Inspector

    private RectTransform barRect;
    private string gameSceneName = "Level 1";

    // Mapping topics to scene names and image indices
    private readonly System.Collections.Generic.Dictionary<string, (string scene, int imageIndex)> topicToScene = new System.Collections.Generic.Dictionary<string, (string, int)>()
    {
        {"Counting up to 100", ("Level 1", 0)},
        {"Number Patterns", ("Level 2", 1)},
        {"Comparing and Ordering Numbers", ("Level 3", 2)}
    };

    void Start()
    {
        if (loadingBarImage != null)
        {
            barRect = loadingBarImage.rectTransform;
            SetBarWidth(0);
        }
        // Set the topic image if assigned and determine scene
        string topic = PlayerPrefs.GetString("SelectedTopic", "");
        int topicImageIndex = 0;
        if (!string.IsNullOrEmpty(topic) && topicToScene.ContainsKey(topic))
        {
            var mapping = topicToScene[topic];
            gameSceneName = mapping.scene;
            topicImageIndex = mapping.imageIndex;
        }
        else
        {
            Debug.LogWarning($"SelectedTopic '{topic}' not found in topicToScene mapping. Defaulting to 'Level 1'.");
            gameSceneName = "Level 1";
            topicImageIndex = 0;
        }
        if (topicImage != null && topicImages != null && topicImages.Length > topicImageIndex)
        {
            topicImage.sprite = topicImages[topicImageIndex];
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
