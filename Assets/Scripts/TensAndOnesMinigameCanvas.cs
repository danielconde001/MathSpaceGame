using DG.Tweening;
using System.Drawing;
using TMPro;
using UnityEngine;

public class TensAndOnesMinigameCanvas : MonoBehaviour
{
    [Header("Canvas Settings")]
    [SerializeField] private UnityEngine.UI.Image panel;
    [SerializeField] private TextMeshProUGUI text;

    public void ShowScreen(uint p_value = 0)
    {
        text.text = "How do you make <color=#00FFFF>" +  p_value.ToString() + "</color>?";
        panel.gameObject.transform.DOLocalMoveY(820, 0f, true);
        panel.gameObject.transform.DOLocalMoveY(520, 1f);
    }

    public void HideScreen()
    {
        panel.gameObject.transform.DOLocalMoveY(520, 0f, true);
        panel.gameObject.transform.DOLocalMoveY(820, 1f);
    }
}
