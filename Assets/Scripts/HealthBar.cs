using UnityEngine;
using UnityEngine.UI;

public class HealthBar : MonoBehaviour
{
    [SerializeField] private Health health;
    [SerializeField] private Image backgroundImage;
    [SerializeField] private Image fillImage;
    [SerializeField] private float maxHealth = 100f;
    [SerializeField] private TMPro.TextMeshProUGUI healthText;
    [SerializeField] private Vector3 offset = new Vector3(0, 2f, 0);
    [SerializeField] private bool faceCamera = true;

    private Camera mainCamera;

    private void Awake()
    {
        mainCamera = Camera.main;
    }

    private void Start()
    {
        if (health == null)
        {
            if (PlayerManager.Instance.GetPlayer().PlayerHealthBar == this)
            {
                health = PlayerManager.Instance.GetPlayer().Health;
            }
            else
            {
                health = GetComponentInParent<Health>();
            }
        }

        maxHealth = health.value;

        if (PlayerManager.Instance.GetPlayer().PlayerHealthBar == this)
            UpdateHealthText();
    }

    private void Update()
    {
        if (PlayerManager.Instance.GetPlayer().PlayerHealthBar == this)
        {
            maxHealth = PlayerManager.Instance.GetPlayer().GetMaxHealth();
        }

        if (health.value >= maxHealth &&
            PlayerManager.Instance.GetPlayer().PlayerHealthBar != this)
        {
            fillImage.enabled = false;
            backgroundImage.enabled = false;
            return;
        }
        else
        {
            fillImage.enabled = true;
            backgroundImage.enabled = true;
        }

        if (health != null && fillImage != null)
        {
            UpdateHealthBar();
            if (PlayerManager.Instance.GetPlayer().PlayerHealthBar == this)
                UpdateHealthText();
        }

        // Make health bar face the camera
        if (faceCamera && mainCamera != null)
        {
            transform.rotation = Quaternion.LookRotation(transform.position - mainCamera.transform.position);
        }
    }

    private void UpdateHealthBar()
    {
        float fillAmount = (float)health.value / maxHealth;
        fillImage.fillAmount = Mathf.Clamp01(fillAmount);
    }

    private void UpdateHealthText()
    {
        if (healthText != null && health != null)
        {
            healthText.text = $"{Mathf.CeilToInt(health.value)}/{Mathf.CeilToInt(maxHealth)}";
        }
    }

    public void SetHealth(Health newHealth)
    {
        health = newHealth;
        if (health != null)
        {
            maxHealth = health.value;
            UpdateHealthBar();
            if (PlayerManager.Instance.GetPlayer().PlayerHealthBar == this)
                UpdateHealthText();
        }
    }
}
