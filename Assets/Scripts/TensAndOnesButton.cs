using TMPro;
using UnityEngine;

public class TensAndOnesButton : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI text;
    private int value = 0;
    public int Value { get { return value; } }

    private void Awake()
    {
        value = 0;
    }

    public void Reset()
    {
        value = 0;
        text.text = value.ToString();
    }

    public void IncreaseValue()
    {
        value += 1;

        if (value > 9) 
        {
            value = 0;
        }

        text.text = value.ToString();
    }
}
