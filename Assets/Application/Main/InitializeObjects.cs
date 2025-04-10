using System.IO;
using UnityEngine;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System.Linq;
using System.Collections.Generic;

public class InitializeObjects : MonoBehaviour
{
    public string armorDataPath = "armor.json";
    public string belongingDataPath = "belongings.json";
    public string foodDataPath = "food.json";
    public string matAndComPath = "material_and_components.json";
    public string monsterPath = "monster_stats.json";
    public string spellPath = "spell.json";
    public string weaponPath = "weapon.json";

    public List<Armor> armors = new List<Armor>();
    public List<Belonging> belongings = new List<Belonging>();
    public List<Food> foods = new List<Food>();
    public List<MatAndCom> matAndComs = new List<MatAndCom>();
    public List<Monster> monsters = new List<Monster>();
    public List<Spell> spells = new List<Spell>();
    public List<Weapon> weapons = new List<Weapon>();


    private void Start()
    {
        

        // Load JSON files as JObject
        JObject armorJSON = CreateJObjectFromPath(armorDataPath);
        JObject belongingJSON = CreateJObjectFromPath(belongingDataPath);
        JObject foodDataJSON = CreateJObjectFromPath(foodDataPath);
        JObject matAndComJSON = CreateJObjectFromPath(matAndComPath);
        JObject monsterJSON = CreateJObjectFromPath(monsterPath);
        JObject spellJSON = CreateJObjectFromPath(spellPath);
        JObject weaponJSON = CreateJObjectFromPath(weaponPath);

        // Extract key names
        string armorKey = armorJSON.Properties().First().Name;
        string belongingKey = belongingJSON.Properties().First().Name;
        string foodKey = foodDataJSON.Properties().First().Name;
        string matAndComKey = matAndComJSON.Properties().First().Name;
        string monsterKey = monsterJSON.Properties().First().Name;
        string spellKey = spellJSON.Properties().First().Name;
        string weaponKey = weaponJSON.Properties().First().Name;

        // Extract arrays using the key variables
        JArray armorArray = (JArray)armorJSON[armorKey];
        JArray belongingArray = (JArray)belongingJSON[belongingKey];
        JArray foodArray = (JArray)foodDataJSON[foodKey];
        JArray matAndComArray = (JArray)matAndComJSON[matAndComKey];
        JArray monsterArray = (JArray)monsterJSON[monsterKey];
        JArray spellArray = (JArray)spellJSON[spellKey];
        JArray weaponArray = (JArray)weaponJSON[weaponKey];

        armors = ListFromArray<Armor>(armorArray);
        foods = ListFromArray<Food>(foodArray);
        belongings = ListFromArray<Belonging>(belongingArray);
        spells = ListFromArray<Spell>(spellArray);
        weapons = ListFromArray<Weapon>(weaponArray);
        monsters = ListFromArray<Monster>(monsterArray);

        int sumEntries = armors.Count + belongings.Count + foodArray.Count + monsterArray.Count + spells.Count + weapons.Count;
        Debug.Log("Logged " + sumEntries + " entries!");
    }

    public List<T> ListFromArray<T>(JArray array) where T : CardDataObject, new()
    {
        List<T> list = new List<T>();

        foreach (JObject item in array)
        {
            T obj = new T();
            CardDataObject created = obj.CreateFromJObject(item);
            list.Add((T)created);
        }

        return list;
    }



    JObject CreateJObjectFromPath(string path)
    {
        string dataPath = Path.Combine(Application.persistentDataPath, path);

        string json = File.ReadAllText(dataPath);

        JObject obj = JObject.Parse(json);

        return obj;
    }
}
