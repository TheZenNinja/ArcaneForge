using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Inventory;
[CreateAssetMenu(menuName = "Singleton/Item Dictionary", fileName = "Item Dictionary")]
public class ItemDictionary : ScriptableObject
{
    public static ItemDictionary instance => GetInstance();
    private static ItemDictionary GetInstance()
    {
        var i = Resources.Load<ItemDictionary>("Items/Item Dictionary");
        
        if (i == null)
            throw new System.NullReferenceException("Can't load Item Dictionary from resources");
        
        return i;
    }


    public List<Item> items;

    public Item GetItemFromName(string name)
    {
        foreach (var i in items)
            if (i.itemName == name)
                return i;

        throw new System.NullReferenceException($"Item named: {name}\t doesnt exit");
    }


    
}
public static class StaticItemDictionary
{
    public static readonly Dictionary<string, Item> basicItems = new Dictionary<string, Item>()
    {
        ["paper"] = Item.Create("Paper", ""),
    };
}


