using UnityEngine;

public class BGMSignal : MonoBehaviour
{
    [SerializeField] AudioClip bgm;

    private void Start()
    {
        AudioManager.Instance.PlayBGM(bgm);
    }
}
