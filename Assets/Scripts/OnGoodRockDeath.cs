using UnityEngine;

public class OnGoodRockDeath : MonoBehaviour
{
    [SerializeField] uint gemValue = 1;
    [SerializeField] GameObject scorePopup;

    public void OnDeath()
    {
        // Do seomthing good
        LevelManager.Instance.CollectGems(gemValue);
        Vector3 popupOffset = transform.up + -transform.right; // up and to the left
        GameObject popup = Instantiate(scorePopup, transform.position + popupOffset, Quaternion.identity);
        ScorePopup popupScript = popup.GetComponent<ScorePopup>();
        if (popupScript != null)
        {
            popupScript.Setup((int)gemValue);
        }
        Destroy(popup, 0.5f);
    }
}
