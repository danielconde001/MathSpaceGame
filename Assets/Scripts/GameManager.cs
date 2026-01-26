using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance;
    public string SelectedTopic;

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }
    }

    public void SelectTopicAndLoad(string topicName)
    {
        SelectedTopic = topicName;
        PlayerPrefs.SetString("SelectedTopic", topicName); // Save for persistence
        SceneManager.LoadScene("LoadingScreen"); // Load the loading screen scene first
    }

    public string GetSelectedTopic()
    {
        // Try to get from PlayerPrefs if not set
        if (string.IsNullOrEmpty(SelectedTopic))
        {
            SelectedTopic = PlayerPrefs.GetString("SelectedTopic", "");
        }
        return SelectedTopic;
    }
}
