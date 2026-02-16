using UnityEngine;

[RequireComponent(typeof(AudioSource))]
public class RandomPitchOnAwake : MonoBehaviour
{
    AudioSource audioSource;

    [SerializeField] float minPitch, maxPitch;

    private void Awake()
    {
        audioSource = GetComponent<AudioSource>();
        audioSource.pitch = Random.Range(minPitch, maxPitch);
    }
}
