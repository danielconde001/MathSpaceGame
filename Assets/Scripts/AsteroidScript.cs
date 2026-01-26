using UnityEngine;
using TMPro;

public class AsteroidScript : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI ValueText;
    public bool isTens;
    private uint Value = 0;

    public void OnShot()
    {
        AddValue();
    }

    private void AddValue()
    {
        Value++;
        ValueText.text = Value.ToString();

        MinigameManager.Instance.CheckValue(Value, isTens);
    }

    public void Reset()
    {
        Value = 0;
        ValueText.text = Value.ToString();
    }

    public uint GetValue()
    {
       return Value; 
    }
}