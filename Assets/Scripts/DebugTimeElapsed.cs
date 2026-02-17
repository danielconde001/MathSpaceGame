using UnityEngine;

public class DebugTimeElapsed : MonoBehaviour
{
    private static DebugTimeElapsed instance;
    public static DebugTimeElapsed Instance
    {
        get
        {
            if (instance == null)
            {
                GameObject newGameObject = new GameObject("DebugTimeElapsed");
                instance = newGameObject.AddComponent<DebugTimeElapsed>();
            }
            return instance;
        }
    }

    float totalTimeElapsed = 0;
    float timeElapsedOnClock = 0;
    bool clockIsRunning = false;

    private void Awake()
    {
        instance = this;

        DontDestroyOnLoad(gameObject);
    }

    void Update()
    {
        totalTimeElapsed += Time.deltaTime;

        if (clockIsRunning == true)
        {
            timeElapsedOnClock += Time.deltaTime;
        }
    }

    public void runClock()
    {
        clockIsRunning = true;
    }

    public void showTimeElapsedOnClock()
    {
        Debug.LogWarning(timeElapsedOnClock);
    }

    public void stopClock()
    {
        clockIsRunning = false;
    }

    public void resetClock()
    {
        timeElapsedOnClock = 0;
    }

    public void showTotalTimeElapsed()
    {
        Debug.Log(totalTimeElapsed);
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            Debug.LogWarning(totalTimeElapsed);
        }
    }
}
