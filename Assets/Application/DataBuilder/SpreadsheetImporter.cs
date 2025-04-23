using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;
using UnityEngine.Networking;
using Newtonsoft.Json.Linq;

public class SpreadsheetImporter : MonoBehaviour
{
    [Header("Settings")]
    [Tooltip("Google Sheets JSON URL")]
    public string sheetURL = "https://script.google.com/macros/s/AKfycbzqlqtCWTaiT5upfiSVFBhxi2J4FgTM1DlV_fu4q3PkUdAeD1NNo6APIwALo0FWIqKjLQ/exec";

    [Tooltip("Whether to fetch latest data online")]
    public bool update = true;

    [Tooltip("Folder name (within /Data) where sub-files will be stored")]
    public string separatedDataFolder = "SubFiles";

    [Tooltip("Paths of all generated JSON subfiles")]
    public List<string> dataPaths = new List<string>();

    private string mainDataPath;

    private void Awake()
    {
        string dataDir = Path.Combine(Application.persistentDataPath, "Data");
        mainDataPath = Path.Combine(dataDir, "data.json");

        if (!Directory.Exists(dataDir))
            Directory.CreateDirectory(dataDir);
    }

    /// <summary>
    /// Public coroutine for other scripts to initialize and wait for data preparation.
    /// </summary>
    public IEnumerator InitializeData()
    {
        if (update)
        {
            yield return StartCoroutine(FetchDataFromURL(sheetURL, mainDataPath));
        }

        yield return StartCoroutine(SeparateAndLoad(mainDataPath));
    }

    private IEnumerator FetchDataFromURL(string url, string savePath)
    {
        UnityWebRequest request = UnityWebRequest.Get(url);
        yield return request.SendWebRequest();

        if (request.result == UnityWebRequest.Result.Success)
        {
            string json = request.downloadHandler.text;
            File.WriteAllText(savePath, json);
            Debug.Log($"JSON saved to: {savePath}");
        }
        else
        {
            Debug.LogError("Download error: " + request.error);
        }
    }

    private IEnumerator SeparateAndLoad(string filePath)
    {
        if (!File.Exists(filePath))
        {
            Debug.LogWarning("Main data file not found: " + filePath);
            yield break;
        }

        string json = File.ReadAllText(filePath);
        JObject fullJson = JObject.Parse(json);

        string outputFolder = Path.Combine(Application.persistentDataPath, "Data", separatedDataFolder);
        if (!Directory.Exists(outputFolder))
            Directory.CreateDirectory(outputFolder);

        foreach (var item in fullJson)
        {
            string key = item.Key;
            string fileName = key.ToLower().Replace(" ", "_") + ".json";
            string subFilePath = Path.Combine(outputFolder, fileName);

            JObject wrappedSection = new JObject
            {
                [key] = item.Value
            };

            File.WriteAllText(subFilePath, wrappedSection.ToString());
        }

        Debug.Log("Separated into " + fullJson.Count + " files");

        dataPaths = GetDataPaths(outputFolder);
        yield return null;
    }

    private List<string> GetDataPaths(string folderPath)
    {
        List<string> filePaths = new List<string>();

        if (Directory.Exists(folderPath))
        {
            string[] files = Directory.GetFiles(folderPath, "*.json");
            filePaths.AddRange(files);
        }
        else
        {
            Debug.LogWarning("Subfile directory not found: " + folderPath);
        }

        return filePaths;
    }
}
