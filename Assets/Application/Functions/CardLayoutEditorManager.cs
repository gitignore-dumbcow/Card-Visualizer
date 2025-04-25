using System.Collections.Generic;
using System.IO;
using UnityEngine;
using Newtonsoft.Json; // <-- Add this
using UnityEngine.UI;

public class CardLayoutEditorManager : MonoBehaviour
{
    public List<GameObject> layoutElements = new List<GameObject>();
    private string layoutPath => Path.Combine(Application.persistentDataPath, "layout.json");

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            LoadLayout(layoutPath);
        }
    }

    [ContextMenu("Save Layout")]
    public void SaveLayout()
    {
        CardLayoutTemplate layout = new CardLayoutTemplate();

        foreach (GameObject go in layoutElements)
        {
            RectTransform rt = go.GetComponent<RectTransform>();
            if (rt == null) continue;

            CardElementTemplate el = new CardElementTemplate(
                go.name,
                rt.anchoredPosition,
                rt.sizeDelta,
                rt.localEulerAngles.z,
                go.activeSelf
            );
            layout.elements.Add(el);

        }

        string json = JsonConvert.SerializeObject(layout, Formatting.Indented); // <-- new
        File.WriteAllText(layoutPath, json);
        Debug.Log($"Layout saved to: {layoutPath}");
    }

    public void LoadLayout(string targetPath)
    {
        if (!File.Exists(targetPath))
        {
            Debug.LogWarning("Layout file not found!");
            return;
        }

        string json = File.ReadAllText(targetPath);
        CardLayoutTemplate layout = JsonConvert.DeserializeObject<CardLayoutTemplate>(json); // <-- new

        foreach (CardElementTemplate element in layout.elements)
        {
            GameObject go = layoutElements.Find(e => e.name == element.elementName);
            if (go == null) continue;

            RectTransform rt = go.GetComponent<RectTransform>();
            if (rt == null) continue;

            rt.anchoredPosition = element.GetAnchoredPosition();
            rt.sizeDelta = element.GetSizeDelta();
            rt.localEulerAngles = new Vector3(0, 0, element.rotationZ);
            go.SetActive(element.isVisible);
        }


        Debug.Log("Layout loaded!");
    }
}
