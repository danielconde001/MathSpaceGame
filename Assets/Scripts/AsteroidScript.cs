using UnityEngine;
using TMPro;

public class AsteroidScript : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI ValueText;
    [HideInInspector] public TensAndOnesMinigameManager manager;
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

        if (manager == null) Debug.LogError("Missing manager for this asteroid!");

        manager?.CheckValue(Value, isTens);
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