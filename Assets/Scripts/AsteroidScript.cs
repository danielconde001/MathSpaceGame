using UnityEngine;
using TMPro;

public class AsteroidScript : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI ValueText;
    [HideInInspector] public TensAndOnesMinigameManager manager;
    public bool isTens;
    private uint Value = 0;

    SpawnVFXOnDeath SpawnVFXOnDeath;

    private void Awake()
    {
        SpawnVFXOnDeath = GetComponent<SpawnVFXOnDeath>();
    }

    public void OnShot()
    {
        AddValue();
    }

    private void AddValue()
    {
        Value++;

        if (Value > 9) Value = 0;

        ValueText.text = Value.ToString();

        if (manager == null) Debug.LogError("Missing manager for this asteroid!");

        manager?.CheckValue();
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

    public void Kill()
    {
        if (SpawnVFXOnDeath != null)
        {
            SpawnVFXOnDeath.SpawnVFX();
        }

        gameObject.SetActive(false);
    }
}