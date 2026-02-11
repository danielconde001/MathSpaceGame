using UnityEngine;

public class EnemyKillable : Killable
{
    [SerializeField] private GameObject scorePopupPrefab;



    public override void Kill()
    {
        base.Kill();

        // spawn explosion


        // Spawn score popup using Score component
        if (scorePopupPrefab != null)
        {
            int scoreValue = 0;
            Score scoreComponent = GetComponent<Score>();
            if (scoreComponent != null)
                scoreValue = scoreComponent.value;

            Vector3 popupOffset = transform.up + -transform.right; // up and to the left
            GameObject popup = Instantiate(scorePopupPrefab, transform.position + popupOffset, Quaternion.identity);
            ScorePopup popupScript = popup.GetComponent<ScorePopup>();
            if (popupScript != null)
            {
                popupScript.Setup(scoreValue);
            }
            Destroy(popup, 0.5f);

            // Add score to ScoreManager
            if (scoreValue > 0 && ScoreManager.Instance != null)
            {
                ScoreManager.Instance.AddScore(scoreValue);
            }
        }

        Destroy(gameObject);
    }
}
