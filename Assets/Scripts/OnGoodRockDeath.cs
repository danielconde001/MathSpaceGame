using UnityEngine;

public class OnGoodRockDeath : MonoBehaviour
{
    [SerializeField] GameObject scorePopup;

    SpawnVFXOnDeath SpawnVFXOnDeath;

    private void Awake()
    {
        SpawnVFXOnDeath = GetComponent<SpawnVFXOnDeath>();
    }

    public void OnDeath()
    {
        int scoreValue = 0;

        Score scoreComponent = GetComponent<Score>();
        if (scoreComponent != null)
            scoreValue = scoreComponent.value;

        if (scoreValue > 0 && ScoreManager.Instance != null)
        {
            ScoreManager.Instance.AddScore(scoreValue);
        }

        AudioManager.Instance.PlaySpaceCrystalDeathSFX(transform.position);

        // Pop up command - Starting line
        Vector3 popupOffset = transform.up + -transform.right; // up and to the left
        GameObject popup = Instantiate(scorePopup, transform.position + popupOffset, Quaternion.identity);
        ScorePopup popupScript = popup.GetComponent<ScorePopup>();
        if (popupScript != null)
        {
            popupScript.Setup((int)scoreValue);
        }
        Destroy(popup, 0.5f);
        // Pop up command - Ending line

        if (SpawnVFXOnDeath != null)
        {
            SpawnVFXOnDeath.SpawnVFX(transform.parent);
        }
    }
}
