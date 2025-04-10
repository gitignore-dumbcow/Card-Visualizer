using Newtonsoft.Json.Linq;
using System;
using UnityEngine;

[Serializable]
public class Spell : CardDataObject
{
    public string _name { get; private set; }
    public string _tier { get; private set; }
    public string _school { get; private set; }
    public string _castingTime { get; private set; }
    public string _range { get; private set; }
    public string _components { get; private set; }
    public string _duration { get; private set; }
    public string _description { get; private set; }
    public string _bonus { get; private set; }

    public Spell() { }

    public Spell(
        string name,
        string tier,
        string school,
        string castingTime,
        string range,
        string components,
        string duration,
        string description,
        string bonus)
    {
        _name = name;
        _tier = tier;
        _school = school;
        _castingTime = castingTime;
        _range = range;
        _components = components;
        _duration = duration;
        _description = description;
        _bonus = bonus;
    }

    public override CardDataObject CreateFromJObject(JObject item)
    {
        string name = item["Name"]?.ToString() ?? "Unknown Spell";
        string tier = item["Tier"]?.ToString() ?? "";
        string school = item["Class"]?.ToString() ?? "";
        string castingTime = item["Casting time"]?.ToString() ?? "";
        string range = item["Range"]?.ToString() ?? "";
        string components = item["Components"]?.ToString() ?? "";
        string duration = item["Duration"]?.ToString() ?? "";
        string description = item["Description"]?.ToString() ?? "";
        string bonus = item["Bonus"]?.ToString() ?? "";

        return new Spell(
            name,
            tier,
            school,
            castingTime,
            range,
            components,
            duration,
            description,
            bonus
        );
    }
}
