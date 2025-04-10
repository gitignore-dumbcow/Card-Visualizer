using System.Collections;
using System.IO;
using UnityEngine;
using UnityEngine.Networking;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using static System.Collections.Specialized.BitVector32;

public class SpreadsheetImporter : MonoBehaviour
{
    public string url = "https://script.google.com/macros/s/AKfycbzqlqtCWTaiT5upfiSVFBhxi2J4FgTM1DlV_fu4q3PkUdAeD1NNo6APIwALo0FWIqKjLQ/exec";
    public bool update;

    private void Start()
    {
        if(update)
        {
            StartCoroutine(FetchDataFromSheet());

        }
        else
        {
            SeperateFile(Path.Combine(Application.persistentDataPath, "data.json"));

        }
    }

    IEnumerator FetchDataFromSheet()
    {
        UnityWebRequest request = UnityWebRequest.Get(url);
        yield return request.SendWebRequest();

        if(request.result == UnityWebRequest.Result.Success)
        {

            string json = request.downloadHandler.text;
            string path = Path.Combine(Application.persistentDataPath, "data.json");


            File.WriteAllText(path, json);

            Debug.Log("JSON saved to: " + path);

            SeperateFile(path);
        }
        else
        {
            Debug.Log("Error: " + request.error);
        }
    }

    void SeperateFile(string dataPath)
    {

        string json = File.ReadAllText(dataPath);
        JObject fullJson = JObject.Parse(json);

        foreach (var item in fullJson)
        {
            string key = item.Key;
            string fileName = key.ToLower().Replace(" ", "_") + ".json";
            string path = Path.Combine(Application.persistentDataPath, fileName);

            JToken section = fullJson[key];

            JObject wrappedSection = new JObject
            {
                [key] = section
            };

            // Write the wrapped JSON to file
            File.WriteAllText(path, wrappedSection.ToString());
        }

        Debug.Log("Seperated into " + fullJson.Count + " files");

    }

}
