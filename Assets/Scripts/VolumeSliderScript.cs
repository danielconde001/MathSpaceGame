using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.UI;

[RequireComponent(typeof(Slider))]
public class VolumeSliderScript : MonoBehaviour
{
    Slider slider;

    private void Awake()
    {
        slider = GetComponent<Slider>();
    }

    private void Start()
    {
        slider.value = AudioManager.Instance.CurrentVolume;
        slider.onValueChanged.AddListener(AudioManager.Instance.SetVolume);
    }
}
