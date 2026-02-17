using UnityEngine;

public class OnGoodRockDeath : MonoBehaviour
{
    [SerializeField] uint gemValue = 1;
    [SerializeField] GameObject scorePopup;

    SpawnVFXOnDeath SpawnVFXOnDeath;

    private void Awake()
    {
        SpawnVFXOnDeath = GetComponent<SpawnVFXOnDeath>();
    }

    public void OnDeath()
    {
        LevelManager.Instance.CollectPoints(gemValue);

        AudioManager.Instance.PlaySpaceCrystalDeathSFX(transform.position);

        // Pop up command - Starting line
        Vector3 popupOffset = transform.up + -transform.right; // up and to the left
        GameObject popup = Instantiate(scorePopup, transform.position + popupOffset, Quaternion.identity);
        ScorePopup popupScript = popup.GetComponent<ScorePopup>();
        if (popupScript != null)
        {
            popupScript.Setup((int)gemValue);
        }
        Destroy(popup, 0.5f);
        // Pop up command - Ending line

        if (SpawnVFXOnDeath != null)
        {
            SpawnVFXOnDeath.SpawnVFX(transform.parent);
        }
    }
}
