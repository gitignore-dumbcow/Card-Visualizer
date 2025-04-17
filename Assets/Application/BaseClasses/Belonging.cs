using Newtonsoft.Json.Linq;
using System;
using UnityEngine;

[Serializable]
public class Belonging : CardDataObject
{
    public int _cost { get; private set; }
    public Currency _currency { get; private set; }
    public int _weight { get; private set; }

    public Belonging() { }

    public Belonging(string name, int cost, Currency currency, int weight)
    {
        _name = name;
        _cost = cost;
        _currency = currency;
        _weight = weight;
    }

    public override CardDataObject CreateFromJObject(JObject item)
    {
        string name = item["Name"]?.ToString() ?? "Unknown Belonging";
        int cost = TryParseInt(item["Cost"], 0);
        Currency currency = TryParseEnum(item["Currency"]?.ToString()?.ToUpper(), Currency.GP);
        int weight = TryParseInt(item["Weight (lb)"], 0);

        return new Belonging(name, cost, currency, weight);
    }

}
