using Newtonsoft.Json.Linq;
using System;
using UnityEngine;

[Serializable]
public class Weapon : CardDataObject
{
    public WeaponType _weaponType { get; private set; }
    public int _cost { get; private set; }
    public Currency _currency { get; private set; }
    public int _damageX { get; private set; }
    public DamageBase _damageBase { get; private set; }
    public DamageType _damageType { get; private set; }
    public int _weight { get; private set; }
    public string _properties { get; private set; }

    public Weapon() { }
    public Weapon(
        string name,
        WeaponType weaponType,
        int cost,
        Currency currency,
        int damageX,
        DamageBase damageBase,
        DamageType damageType,
        int weight,
        string properties)
    {
        _name = name;
        _weaponType = weaponType;
        _cost = cost;
        _currency = currency;
        _damageX = damageX;
        _damageBase = damageBase;
        _damageType = damageType;
        _weight = weight;
        _properties = properties;
    }

    public override CardDataObject CreateFromJObject(JObject item)
    {
        string name = item["Weapon"]?.ToString() ?? "Unknown Weapon";
        string typeStr = item["Type"]?.ToString() ?? "";
        string currencyStr = item["Currency"]?.ToString() ?? "GP";
        string damageBaseStr = item["Damage base"]?.ToString() ?? "d6";
        string damageTypeStr = item["Stats and special"]?.ToString() ?? "Bludgeoning";

        // Safe enum parsing
        WeaponType weaponType = ParseWeaponType(typeStr);
        Currency currency = TryParseEnum(currencyStr.ToUpper(), Currency.GP);
        DamageBase damageBase = TryParseEnum("D" + damageBaseStr.Replace("d", ""), DamageBase.D6);
        DamageType damageType = TryParseEnum(damageTypeStr, DamageType.Bludgeoning);

        int cost = TryParseInt(item["Cost"]);
        int damageX = TryParseInt(item["Damage X"], 1);
        int weight = TryParseInt(item["Weight (lb)"]);
        string properties = item["Properties"]?.ToString() ?? "None";



        return new Weapon(name, weaponType, cost, currency, damageX, damageBase, damageType, weight, properties);
    }

}
