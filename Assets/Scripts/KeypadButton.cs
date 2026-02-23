using UnityEngine;

public class KeypadButton : MonoBehaviour
{
    public string keyValue; // Set in Inspector ("1", "2", ..., "Delete")

    [SerializeField] bool useDebug = false;

    public void OnButtonPressed()
    {
        if (useDebug) 
            Debug.Log($"Keypad button pressed: {keyValue}");

        if (keyValue == "Delete")
            KeypadManager.Instance.DeleteLast();
        else
            KeypadManager.Instance.AppendToInput(keyValue);
    }
}
