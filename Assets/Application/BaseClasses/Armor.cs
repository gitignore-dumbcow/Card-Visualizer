using Newtonsoft.Json.Linq;
using System;
using UnityEngine;

public enum ArmorType
{
    Light,
    Medium,
    Heavy,
    Shield
}

[Serializable]
public class Armor : CardDataObject
{
    public string _name { get; private set; }
    public ArmorType _type { get; private set; }
    public int _cost { get; private set; }
    public int _ac { get; private set; }
    public string _special { get; private set; }
    public int _weight { get; private set; }

    public Armor() { }

    public Armor(string name, ArmorType type, int cost, int ac, string special, int weight)
    {
        _name = name;
        _type = type;
        _cost = cost;
        _ac = ac;
        _special = special;
        _weight = weight;
    }

    public override CardDataObject CreateFromJObject(JObject item)
    {
        string name = item["Armor"]?.ToString() ?? "Unknown Armor";
        string typeStr = item["Type"]?.ToString() ?? "Light Armor";
        ArmorType type = ParseArmorType(typeStr);
        int cost = TryParseInt(item["Cost (gp)"], 0);
        int ac = TryParseInt(item["Armor Class (AC)"], 0);
        string special = item["Special"]?.ToString() ?? "None";
        int weight = TryParseInt(item["Weight (lb)"], 0);

        return new Armor(name, type, cost, ac, special, weight);
    }

    private ArmorType ParseArmorType(string typeStr)
    {
        typeStr = typeStr.ToLower();
        if (typeStr.Contains("light")) return ArmorType.Light;
        if (typeStr.Contains("medium")) return ArmorType.Medium;
        if (typeStr.Contains("heavy")) return ArmorType.Heavy;
        if (typeStr.Contains("shield")) return ArmorType.Shield;
        return ArmorType.Light;
    }
}
