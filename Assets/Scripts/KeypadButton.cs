using UnityEngine;

public class KeypadButton : MonoBehaviour
{
    public string keyValue; // Set in Inspector ("1", "2", ..., "Delete")

    public void OnButtonPressed()
    {
        Debug.Log($"Keypad button pressed: {keyValue}");
        if (keyValue == "Delete")
            KeypadManager.Instance.DeleteLast();
        else
            KeypadManager.Instance.AppendToInput(keyValue);
    }
}
