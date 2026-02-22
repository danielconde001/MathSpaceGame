using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class InputFieldClickHandler : MonoBehaviour, IPointerDownHandler
{
    public void OnPointerDown(PointerEventData eventData)
    {
        var inputField = GetComponent<InputField>();
        if (inputField != null && inputField.interactable)
        {
            inputField.ActivateInputField();
            KeypadManager.Instance.ShowKeypad(inputField);
            Debug.Log($"InputField clicked: {inputField.name}, Keypad shown.");
        }
    }
}
