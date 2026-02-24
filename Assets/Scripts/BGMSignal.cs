using UnityEngine;

public class BGMSignal : MonoBehaviour
{
    [SerializeField] AudioClip bgm;
    [SerializeField] float volume = 0.6f;

    private void Start()
    {
        // time it
        AudioManager.Instance.PlayBGM(bgm, volume);
    }
}
