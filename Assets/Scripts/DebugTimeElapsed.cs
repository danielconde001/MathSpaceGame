using UnityEngine;

public class DebugTimeElapsed : MonoBehaviour
{

    float elapsedTime = 0;
    
    void Update()
    {
        elapsedTime += Time.deltaTime;
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            Debug.LogWarning(elapsedTime);
        }
    }
}
