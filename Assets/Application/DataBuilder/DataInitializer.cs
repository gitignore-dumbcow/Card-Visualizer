using Newtonsoft.Json.Linq;
using System.Collections;
using System.IO;
using System.Linq;
using UnityEngine;

public class DataInitializer : MonoBehaviour
{
    public SpreadsheetImporter importer;
    public DataCollection dataCollection;

    void Start()
    {
        StartCoroutine(FetchData());

        foreach (string path in importer.dataPaths)
        {
            JObject jObject = CreateJObjectFromPath(path);
            string key = jObject.Properties().First().Name;

            DataPage newPage = dataCollection.CreateNewPage(key);

            JArray jArray = (JArray)jObject[key];
            foreach (JObject item in jArray)
            {
                string itemName = item["Name"]?.ToString() ?? "Unnamed";
                DataItem newItem = newPage.AddDataItem(itemName);

                foreach (var prop in item.Properties())
                {
                    if (prop.Name == "Name") continue; // Already used as item name
                    newItem.AddDataProperty(prop.Name, prop.Value?.ToString() ?? "null");
                }
            }
        }

    }

    private IEnumerator FetchData()
    {
        yield return importer.InitializeData();

        Debug.Log("Data setup complete. Ready to continue.");
        // Now you can safely proceed
        ProceedToGame();
    }

    JObject CreateJObjectFromPath(string path)
    {
        string dataPath = Path.Combine(Application.persistentDataPath, path);

        string json = File.ReadAllText(dataPath);

        JObject obj = JObject.Parse(json);

        return obj;
    }

    void ProceedToGame()
    {
        // Continue logic here
    }
}