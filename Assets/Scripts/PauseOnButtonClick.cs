using UnityEngine;
using UnityEngine.UI;

[RequireComponent(typeof(Button))]
public class PauseOnButtonClick : MonoBehaviour
{
    Button button;

    private void Awake()
    {
        button = GetComponent<Button>();
        button.onClick.AddListener(() => Pause()); 
    }

    private void Pause()
    {
        PauseManager.Instance.IsPaused = true;
    }
}
