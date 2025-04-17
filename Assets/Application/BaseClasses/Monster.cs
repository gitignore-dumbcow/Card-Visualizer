using Newtonsoft.Json.Linq;
using System;
using UnityEngine;

[Serializable]
public class Monster : CardDataObject
{
    public string _description { get; private set; }
    public string _behavior { get; private set; }
    public string _ac { get; private set; }
    public string _hp { get; private set; }
    public int _speed { get; private set; }

    public string _str { get; private set; }
    public string _dex { get; private set; }
    public string _con { get; private set; }
    public string _int { get; private set; }
    public string _wis { get; private set; }
    public string _cha { get; private set; }

    public string _savingThrows { get; private set; }
    public string _skills { get; private set; }
    public string _damageVulnerabilities { get; private set; }
    public string _damageResistances { get; private set; }
    public string _damageImmunities { get; private set; }
    public string _conditionImmunities { get; private set; }
    public string _senses { get; private set; }
    public string _languages { get; private set; }
    public string _challenge { get; private set; }
    public string _abilities { get; private set; }
    public string _actions { get; private set; }
    public string _bonus { get; private set; }

    public Monster() { }

    public Monster(
        string name,
        string description,
        string behavior,
        string ac,
        string hp,
        int speed,
        string str,
        string dex,
        string con,
        string intel,
        string wis,
        string cha,
        string savingThrows,
        string skills,
        string damageVulnerabilities,
        string damageResistances,
        string damageImmunities,
        string conditionImmunities,
        string senses,
        string languages,
        string challenge,
        string abilities,
        string actions,
        string bonus)
    {
        _name = name;
        _description = description;
        _behavior = behavior;
        _ac = ac;
        _hp = hp;
        _speed = speed;
        _str = str;
        _dex = dex;
        _con = con;
        _int = intel;
        _wis = wis;
        _cha = cha;
        _savingThrows = savingThrows;
        _skills = skills;
        _damageVulnerabilities = damageVulnerabilities;
        _damageResistances = damageResistances;
        _damageImmunities = damageImmunities;
        _conditionImmunities = conditionImmunities;
        _senses = senses;
        _languages = languages;
        _challenge = challenge;
        _abilities = abilities;
        _actions = actions;
        _bonus = bonus;
    }

    public override CardDataObject CreateFromJObject(JObject item)
    {
        string name = item["Name"]?.ToString() ?? "Unknown Monster";
        string description = item["Description"]?.ToString() ?? "";
        string behavior = item["Behaviors and characteristics"]?.ToString() ?? "";
        string ac = item["AC"]?.ToString() ?? "";
        string hp = item["HPs"]?.ToString() ?? "";
        int speed = TryParseInt(item["Sps (ft)"]);
        string str = item["STR"]?.ToString() ?? "0";
        string dex = item["DEX"]?.ToString() ?? "0";
        string con = item["CON"]?.ToString() ?? "0";
        string intel = item["INT"]?.ToString() ?? "0";
        string wis = item["WIS"]?.ToString() ?? "0";
        string cha = item["CHA"]?.ToString() ?? "0";

        string savingThrows = item["Saving Thows"]?.ToString() ?? "";
        string skills = item["Skill"]?.ToString() ?? "";
        string damageVulnerabilities = item["Damage Vulnerabilities"]?.ToString() ?? "";
        string damageResistances = item["Damage Resistances"]?.ToString() ?? "";
        string damageImmunities = item["Damage Immunities"]?.ToString() ?? "";
        string conditionImmunities = item["Condition Immunities"]?.ToString() ?? "";
        string senses = item["Senses"]?.ToString() ?? "";
        string languages = item["Languages"]?.ToString() ?? "";
        string challenge = item["Challenge"]?.ToString() ?? "";
        string abilities = item["Ability"]?.ToString() ?? "";
        string actions = item["Actions"]?.ToString() ?? "";
        string bonus = item["Bonus"]?.ToString() ?? "";

        return new Monster(
            name,
            description,
            behavior,
            ac,
            hp,
            speed,
            str,
            dex,
            con,
            intel,
            wis,
            cha,
            savingThrows,
            skills,
            damageVulnerabilities,
            damageResistances,
            damageImmunities,
            conditionImmunities,
            senses,
            languages,
            challenge,
            abilities,
            actions,
            bonus
        );
    }
}
