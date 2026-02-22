using UnityEngine;
using UnityEngine.UI;

public class KeypadManager : MonoBehaviour
{
    public static KeypadManager Instance;
    public GameObject keypadPanel; // Assign Panel in Inspector
    private InputField activeInputField;

    void Awake()
    {
        Instance = this;
        keypadPanel.SetActive(false);
    }

    public void ShowKeypad(InputField inputField)
    {
        activeInputField = inputField;
        keypadPanel.SetActive(true);
    }

    public void HideKeypad()
    {
        keypadPanel.SetActive(false);
        activeInputField = null;
    }

    public void AppendToInput(string value)
    {
        Debug.Log($"AppendToInput called. Active field: {activeInputField?.name}, value: {value}");
        if (activeInputField != null)
        {
            string newText = activeInputField.text + value;
            if (newText.Length > 2)
                newText = newText.Substring(0, 2);
            activeInputField.text = newText;
            activeInputField.ActivateInputField(); // Restore focus
        }
    }

    public void DeleteLast()
    {
        Debug.Log($"DeleteLast called. Active field: {activeInputField?.name}");
        if (activeInputField != null && activeInputField.text.Length > 0)
        {
            activeInputField.text = activeInputField.text.Substring(0, activeInputField.text.Length - 1);
            activeInputField.ActivateInputField(); // Restore focus and update visuals
        }
    }
}
