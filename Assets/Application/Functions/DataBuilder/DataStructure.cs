using System;
using System.Collections.Generic;
using UnityEngine;

[Serializable]
public class DataProperty
{
    public bool extended = false;
    public List<GameObject> children = new List<GameObject>();

    [SerializeField] private string key = "New Key";
    [SerializeField] private string value = "N/a";

    public string Key => key;
    public string Value => value;

    public DataProperty(string key, string value)
    {
        this.key = key;
        this.value = value;
    }
}


[Serializable]
public class DataItem
{
    public bool extended = false;
    public List<GameObject> children = new List<GameObject>();

    [SerializeField] private string dataItemName = "New Data Item";
    [SerializeField] private List<DataProperty> properties = new List<DataProperty>();

    public string DataItemName => dataItemName;
    public List<DataProperty> Properties => properties;

    public DataItem(string name)
    {
        this.dataItemName = name;
    }

    public void AddDataProperty(string key, string value)
    {
        DataProperty prop = new DataProperty(key, value);
        properties.Add(prop); // <- This was missing in your original code
    }
}


[Serializable]
public class DataPage
{
    public bool extended = false;
    public List<GameObject> children = new List<GameObject>();

    [SerializeField] private string pageName = "New Page";
    [SerializeField] private List<DataItem> list = new List<DataItem>();

    public string PageName => pageName;
    public List<DataItem> List => list;

    public DataPage(string name)
    {
        this.pageName = name;
    }

    public DataItem AddDataItem(string itemName)
    {
        DataItem item = new DataItem(itemName);
        list.Add(item);

        return item;
    }
}
