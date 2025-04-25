using System.Collections.Generic;
using UnityEngine;

public class DataCollection : MonoBehaviour
{
    [SerializeField] private List<DataPage> dataCollection = new List<DataPage>();
    public List<DataPage> DataCollectionList => dataCollection; // Read-only access

    public DataPage CreateNewPage(string pageName)
    {
        DataPage page = new DataPage(pageName);
        dataCollection.Add(page);

        return page;
    }
}

