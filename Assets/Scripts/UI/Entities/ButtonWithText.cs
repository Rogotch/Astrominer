using TMPro;
using UnityEngine;
using UnityEngine.UI;

[RequireComponent(typeof(Button))]
public class ButtonWithText : MonoBehaviour
{
    [Header("Текст")]
    [SerializeField, Multiline]
    private string _buttonText = "Button";

    private TextMeshProUGUI _textComponent;

    void OnValidate()
    {
        SetButtonText();
    }
    void Awake()    
    {
        SetButtonText();
    }

    private void SetButtonText()
    {
        if (_textComponent == null)
            _textComponent = GetComponentInChildren<TextMeshProUGUI>();
            if (_textComponent != null)
                _textComponent.text = _buttonText;
    }
}
