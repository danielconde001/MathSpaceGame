using System.Collections;
using UnityEngine;

public class CameraShake : MonoBehaviour
{
    [SerializeField] float shakeDuration = 1f;
    [SerializeField] float shakeMagnitude = 2f;
    [SerializeField] float dampingSpeed = 1f;

    [SerializeField] bool useDebug = false;

    private void Update()
    {
        if (useDebug == true)
        {
            if (Input.GetKeyDown(KeyCode.O))
            {
                TriggerShake();
            }
        }
    }

    public void TriggerShake()
    {
        StartCoroutine(Shake());
    }

    IEnumerator Shake()
    {
        float elapsedTime = 0f;
        Vector3 initialPosition = transform.localPosition;

        Time.timeScale = 0;

        while (elapsedTime < shakeDuration)
        {
            float magnitude = shakeMagnitude * Mathf.Exp(-dampingSpeed * elapsedTime);
            float xOffset = Random.Range(-1f, 1f) * magnitude;
            float yOffset = Random.Range(-1f, 1f) * magnitude;

            transform.localPosition = 
                new Vector3(
                    initialPosition.x + xOffset,
                    initialPosition.y + yOffset,
                    initialPosition.z);

            elapsedTime += Time.unscaledDeltaTime;

            yield return null;
        }

        Time.timeScale = 1;

        transform.localPosition = initialPosition;
    }
}
