using Newtonsoft.Json.Linq;
using System;
using UnityEngine;

[Serializable]
public class Food : CardDataObject
{
    
    public string _class { get; private set; }
    public string _subClass { get; private set; }
    public int _cost { get; private set; }
    public Currency _currency { get; private set; }
    public int _weight { get; private set; }
    public int _speed { get; private set; }
    public int _carryingCapacity { get; private set; }

    public Food() { }

    public Food(
        string name,
        string foodClass,
        string subClass,
        int cost,
        Currency currency,
        int weight,
        int speed,
        int carryingCapacity)
    {
        _name = name;
        _class = foodClass;
        _subClass = subClass;
        _cost = cost;
        _currency = currency;
        _weight = weight;
        _speed = speed;
        _carryingCapacity = carryingCapacity;
    }

    public override CardDataObject CreateFromJObject(JObject item)
    {
        string name = item["Name"]?.ToString() ?? "Unknown Food";
        string foodClass = item["Class"]?.ToString() ?? "";
        string subClass = item["Sub-class"]?.ToString() ?? "";

        int cost = TryParseInt(item["Cost"]);
        Currency currency = TryParseEnum(item["Currency"]?.ToString()?.ToUpper(), Currency.GP);
        int weight = TryParseInt(item["Weight (lb)"], 0);
        int speed = TryParseInt(item["Speed (ft)"], 0);
        int carryingCapacity = TryParseInt(item["Carrying Capacity"], 0);

        return new Food(
            name,
            foodClass,
            subClass,
            cost,
            currency,
            weight,
            speed,
            carryingCapacity
        );
    }
}
