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

    public ObjectDataCollection collection;

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

        collection.armors = ListFromArray<Armor>(armorArray);
        collection.foods = ListFromArray<Food>(foodArray);
        collection.belongings = ListFromArray<Belonging>(belongingArray);
        collection.spells = ListFromArray<Spell>(spellArray);
        collection.weapons = ListFromArray<Weapon>(weaponArray);
        collection.monsters = ListFromArray<Monster>(monsterArray);

        int sumEntries = collection.armors.Count + collection.belongings.Count + foodArray.Count + monsterArray.Count + collection.spells.Count + collection.weapons.Count;
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
