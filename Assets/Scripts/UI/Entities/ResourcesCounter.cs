using UnityEngine;
using UnityEngine.UI;
using TMPro;
using System;

public class ResourcesCounter : MonoBehaviour
{
    #region Variables
        [Header("Objects")]
        [SerializeField]
        private Image icon_object;
        [SerializeField]
        private TextMeshProUGUI text_object;
    
        [Header("Values")]
        [SerializeField]
        private Sprite icon;
        [SerializeField]
        private string text;
    #endregion


    private void Awake()
    {
        SetIcon(icon);
        SetText(text);
    }
    private void OnValidate()
    {
        SetIcon(icon);
        SetText(text);
    }

    public void SetDataByResource(BlocksResource resource)
    {
        SetIcon(resource.icon);
    }

    private void SetIcon(Sprite new_icon)
    {
        icon = new_icon;
        UpdateIcon();
    }

    private void SetText(string new_text)
    {
        text = new_text;
        UpdateText();
    }

    private void UpdateIcon()
    {
        if (icon_object == null) return;
        icon_object.sprite = icon;
    }

    private void UpdateText()
    {
        if (text_object == null) return;
        text_object.text = text;
    }
}
