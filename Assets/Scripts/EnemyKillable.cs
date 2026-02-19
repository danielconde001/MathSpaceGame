using Unity.Collections.LowLevel.Unsafe;
using UnityEngine;

public class EnemyKillable : Killable
{
    [SerializeField] private GameObject scorePopupPrefab;
    [SerializeField] private bool includeCamShake = false;

    SpawnVFXOnDeath SpawnVFXOnDeath;
    CameraShake camShake;
    DistanceToPlayer distanceToPlayer;

    private void Awake()
    {
        SpawnVFXOnDeath = GetComponent<SpawnVFXOnDeath>();
        camShake = Camera.main.GetComponent<CameraShake>();
        distanceToPlayer = GetComponent<DistanceToPlayer>();
    }

    public override void Kill()
    {
        base.Kill();

        AudioManager.Instance.PlayEnemyDeathSFX();

        // Spawn score popup using Score component
        if (scorePopupPrefab != null)
        {
            int scoreValue = 0;
            Score scoreComponent = GetComponent<Score>();
            if (scoreComponent != null)
                scoreValue = scoreComponent.value;

            LevelManager.Instance.CollectPoints((uint)scoreValue);

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

        if (SpawnVFXOnDeath != null)
        {
            SpawnVFXOnDeath.SpawnVFX();
        }

        if (includeCamShake == true)
        {
            camShake.TriggerShake(false);
        }

        PlayerVicinity.Instance.DistancesToPlayer.Remove(distanceToPlayer);

        Destroy(gameObject);
    }
}
