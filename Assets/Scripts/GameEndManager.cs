using UnityEngine;
using UnityEngine.SceneManagement;

public class GameEndManager : MonoBehaviour
{
    private bool gameEnded = false;
    public TMPro.TextMeshProUGUI countdownText; // Assign in Inspector
    public int testScore = 100;
    public float countdownSeconds = 5f;

    private void Start()
    {
        StartCoroutine(EndGameCountdown(testScore, countdownSeconds));
    }

    private System.Collections.IEnumerator EndGameCountdown(int playerScore, float seconds)
    {
        float timer = seconds;
        while (timer > 0)
        {
            if (countdownText != null)
                countdownText.text = $"Game Ending ({Mathf.CeilToInt(timer)}s)";
            yield return null;
            timer -= Time.deltaTime;
        }
        if (countdownText != null)
            countdownText.text = "";
        EndGame(playerScore);
    }

    // Call this method to end the game and load the end screen, passing the player's score
    public void EndGame(int playerScore)
    {
        if (!gameEnded)
        {
            gameEnded = true;
            Debug.Log($"[GameEndManager] Setting PlayerScore: {playerScore}");
            PlayerPrefs.SetInt("PlayerScore", playerScore);
            SceneManager.LoadScene("EndScreen");
        }
    }
}
