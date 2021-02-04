using UnityEngine;

namespace Inventory
{
    [CreateAssetMenu(menuName = "Item/Basic")]

    public class Item : ScriptableObject
    {
        public string itemName;
        public int maxStack = 999;
        public Sprite sprite;
        public string description;

        public static Item Create(string itemName, string description = "", int maxStack = 999)
        {
            Item i = CreateInstance<Item>();

            i.InitData(itemName, description, maxStack);
            return i;
        }
        public void InitData(string itemName, string description, int maxStack)
        {
            name = itemName;
            this.itemName = itemName;
            this.maxStack = maxStack;
            this.description = description;
        }
        public static bool operator ==(Item x, Item y)
        {
            if (x.GetType() != y.GetType())
                return false;

            return x.itemName == y.itemName;
        }
        public static bool operator !=(Item x, Item y) => !(x == y);

        public void LogType() => Debug.Log(GetType().ToString());
    }
}