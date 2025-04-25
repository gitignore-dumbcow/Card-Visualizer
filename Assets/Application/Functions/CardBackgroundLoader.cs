using UnityEngine;
using UnityEngine.UI;
using SFB; // StandaloneFileBrowser
using System.IO;
using System.Collections;

public class CardBackgroundLoader : MonoBehaviour
{
    public RawImage cardBackground; // Assign this in the inspector

    public void LoadCardBackground()
    {
        var extensions = new[] {
            new ExtensionFilter("Image Files", "png", "jpg", "jpeg")
        };

        StandaloneFileBrowser.OpenFilePanelAsync("Select Base Image", "", extensions, false, (string[] paths) =>
        {
            if (paths.Length > 0 && File.Exists(paths[0]))
            {
                StartCoroutine(LoadImage(paths[0]));
            }
        });
    }

    private IEnumerator LoadImage(string path)
    {
        byte[] imgData = File.ReadAllBytes(path);
        Texture2D tex = new Texture2D(2, 2);
        tex.LoadImage(imgData);
        tex.Apply();

        cardBackground.texture = tex;

        RectTransform rt = cardBackground.GetComponent<RectTransform>();
        RectTransform parent = rt.parent as RectTransform;

        if (parent != null)
        {
            float imgWidth = tex.width;
            float imgHeight = tex.height;

            float parentWidth = parent.rect.width;
            float parentHeight = parent.rect.height;

            float imageAspect = imgWidth / imgHeight;
            float parentAspect = parentWidth / parentHeight;

            Vector2 newSize;

            if (imageAspect > parentAspect)
            {
                // Fit by width
                newSize = new Vector2(parentWidth, parentWidth / imageAspect);
            }
            else
            {
                // Fit by height
                newSize = new Vector2(parentHeight * imageAspect, parentHeight);
            }

            // Center and apply
            rt.anchorMin = new Vector2(0.5f, 0.5f);
            rt.anchorMax = new Vector2(0.5f, 0.5f);
            rt.pivot = new Vector2(0.5f, 0.5f);
            rt.sizeDelta = newSize;
            rt.anchoredPosition = Vector2.zero;
        }

        yield return null;
    }

}
