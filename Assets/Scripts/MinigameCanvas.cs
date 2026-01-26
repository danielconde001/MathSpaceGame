using DG.Tweening;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using static System.Net.Mime.MediaTypeNames;

public class MinigameCanvas : MonoBehaviour
{
    [Header("Minigame Canvas Settings")]
    [SerializeField] private UnityEngine.UI.Image panel;
    [SerializeField] private TextMeshProUGUI text;

    public void ShowScreen(uint p_value = 0)
    {
        text.text = "How do you make " + p_value.ToString() + "?";
        panel.gameObject.transform.DOLocalMoveY(820, 0f, true);
        panel.gameObject.transform.DOLocalMoveY(520, 1f);
    }
}
