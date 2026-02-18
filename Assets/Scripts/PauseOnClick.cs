using UnityEngine;
using UnityEngine.UI;

[RequireComponent(typeof(Button))]
public class PauseOnClick : MonoBehaviour
{
    Button button;

    [SerializeField] bool PausesOnClick = true;

    private void Awake()
    {
        button = GetComponent<Button>();
        button.onClick.AddListener(() => Pause()); 
    }

    private void Pause()
    {
        PauseManager.Instance.IsPaused = PausesOnClick;
    }
}
