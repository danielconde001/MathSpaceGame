using UnityEngine;
using TMPro;

public class ScorePopup : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI popupText;
    [SerializeField] private float floatSpeed = 1f;
    [SerializeField] private float duration = 1f;

    private float timer;

    public void Setup(int value)
    {
        if (popupText != null)
            popupText.text = $"+{value}";
        timer = duration;
    }

    private void Update()
    {
        transform.position += Vector3.up * floatSpeed * Time.deltaTime;
        timer -= Time.deltaTime;
        if (timer <= 0f)
        {
            Destroy(gameObject);
        }
    }
}
