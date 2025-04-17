using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class ObjectBrowserManager : MonoBehaviour
{
    public Transform listParent;
    public GameObject objectListItemPF;
    public GameObject scrollView;
    public ObjectDataCollection data;
    public TextMeshProUGUI hoverTextBox;

    private bool open;

    //public CardGenerator cardGenerator;

    public void Populate<T>(List<T> objects) where T : CardDataObject
    {
        foreach (var obj in objects)
        {
            GameObject item = Instantiate(objectListItemPF, listParent);
            item.GetComponentInChildren<TextMeshProUGUI>().text = obj._name;

            // Hook the hover script
            var hoverScript = item.AddComponent<ObjectListItemUI>();
            hoverScript.SetTarget(obj, hoverTextBox); // `hoverTextBox` is your TMP field in UI
        }
    }



    public void UpdateList()
    {
        List<Armor> armors = data.armors;
        List<Belonging> belongings = data.belongings;
        List<Food> foods = data.foods;
        List<MatAndCom> matAndComs = data.matAndComs;
        List<Monster> monsters = data.monsters;
        List<Spell> spells = data.spells;
        List<Weapon> weapons = data.weapons;

        Populate(armors);
        Populate(belongings);
        Populate(foods);
        Populate(spells);
        Populate(weapons);
        Populate(monsters);
        Populate(matAndComs);

    }

    public void ClearList()
    {
        foreach (Transform child in listParent)
            Destroy(child.gameObject);
    }

    public void ToggleList()
    {
        open = !open;

        if(open)
        {
            UpdateList();
            scrollView.SetActive(true);
        }
        else
        {
            ClearList();
            scrollView.SetActive(false);
        }

    }

    private void OnSelectObject(object obj)
    {
        System.Type type = obj.GetType();
        Debug.Log($"--- Selected Object Type: {type.Name} ---");

        var props = type.GetProperties(System.Reflection.BindingFlags.Public | System.Reflection.BindingFlags.Instance);

        foreach (var prop in props)
        {
            object value = prop.GetValue(obj);
            Debug.Log($"{prop.Name}: {value}");
        }

        // You can now pass this object to your card system for auto-generation
        // cardGenerator.SetSelectedObject(obj);
    }

}
