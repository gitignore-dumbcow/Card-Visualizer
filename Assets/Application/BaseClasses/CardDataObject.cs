using Newtonsoft.Json.Linq;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum WeaponType
{
    SimpleMelee,
    MartialMelee,
    SimpleRanged,
    MartialRanged,
}

public enum Currency
{
    SP,
    GP,
}

public enum DamageBase
{
    D4,
    D6,
    D8,
    D10,
    D12,
}


public enum DamageType
{
    Bludgeoning,
    Piercing,
    Slashing,
}

[Serializable]
public abstract class CardDataObject
{
    public abstract CardDataObject CreateFromJObject(JObject item);

    public string _name { get; protected set; }

    protected WeaponType ParseWeaponType(string str)
    {
        return str switch
        {
            "Simple Melee Weapons" => WeaponType.SimpleMelee,
            "Martial Melee Weapons" => WeaponType.MartialMelee,
            "Simple Ranged Weapons" => WeaponType.SimpleRanged,
            "Martial Ranged Weapons" => WeaponType.MartialRanged,
            _ => WeaponType.SimpleMelee // fallback
        };
    }
    protected TEnum TryParseEnum<TEnum>(string value, TEnum fallback) where TEnum : struct
    {
        return Enum.TryParse<TEnum>(value, true, out var result) ? result : fallback;
    }
    protected int TryParseInt(JToken token, int defaultValue = 0)
    {
        if (token == null) return defaultValue;

        if (int.TryParse(token.ToString().Trim(), out int result))
            return result;

        return defaultValue;
    }

}


