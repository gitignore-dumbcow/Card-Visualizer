using UnityEngine;
using UnityEngine.EventSystems;
using TMPro;
using System;
using System.Reflection;
using System.Text;

public class ObjectListItemUI : MonoBehaviour, IPointerEnterHandler
{
    public object targetObject;
    public TextMeshProUGUI displayText;

    public void SetTarget(object obj, TextMeshProUGUI textDisplay)
    {
        targetObject = obj;
        displayText = textDisplay;
    }

    public void OnPointerEnter(PointerEventData eventData)
    {
        if (targetObject == null || displayText == null)
            return;

        Type type = targetObject.GetType();
        PropertyInfo[] props = type.GetProperties(BindingFlags.Public | BindingFlags.Instance);

        StringBuilder sb = new StringBuilder();
        sb.AppendLine($"<b>{type.Name}</b>");

        foreach (var prop in props)
        {
            object value = prop.GetValue(targetObject);
            sb.AppendLine($"{prop.Name}: {value}");
        }

        displayText.text = sb.ToString();
    }
}
