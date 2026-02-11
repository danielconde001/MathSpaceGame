using UnityEngine;
using UnityEngine.UI;

public class HealthBar : MonoBehaviour
{
    [SerializeField] private Health health;
    [SerializeField] private Image fillImage;
    [SerializeField] private float maxHealth = 100f;
    [SerializeField] private Vector3 offset = new Vector3(0, 2f, 0);
    [SerializeField] private bool faceCamera = true;

    private Camera mainCamera;

    private void Awake()
    {
        mainCamera = Camera.main;

        if (health == null)
        {
            health = GetComponentInParent<Health>();
        }

        if (fillImage != null && health != null)
        {
            maxHealth = health.value;
            UpdateHealthBar();
        }
    }

    private void Update()
    {
        if (health != null && fillImage != null)
        {
            UpdateHealthBar();
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

    public void SetHealth(Health newHealth)
    {
        health = newHealth;
        if (health != null)
        {
            maxHealth = health.value;
            UpdateHealthBar();
        }
    }
}
