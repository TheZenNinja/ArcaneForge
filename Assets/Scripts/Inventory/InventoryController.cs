using System;
using System.Collections.Generic;
using UnityEngine;

namespace Inventory
{
    public class InventoryController : MonoBehaviour
    {
        [SerializeField]
        protected List<ItemStack> items = new List<ItemStack>();
        public void SetItems(IEnumerable<ItemStack> newItems) => items = new List<ItemStack>(newItems);
        public List<ItemStack> GetAllItems() => items;
        /// <returns>Returns false if the inventory is full</returns>
        public bool AddItem(ItemStack item)
        {
            if (ContainsItem(item.item))
            {
                ItemStack stack = GetItemStack(item.item);
                stack += item.count;
                return true;
            }
            return false;
        }
        public ItemStack GetItemStackInSlot(int index) => items[index];

        public ItemStack GetItemStack(ItemStack stack)
        {
            foreach (var i in items)
                if (i.item == stack.item && i.count >= stack.count)
                    return i;

            return null;
        }
        public void ForceSetItem(ItemStack stack, int index)
        {
            items[index] = stack;
        }
        public ItemStack GetItemStack(Item item) => GetItemStack(new ItemStack(item));
        public bool ContainsItem(ItemStack stack) => GetItemStack(stack) != null;
        public bool ContainsItem(Item item) => GetItemStack(item) != null;


        public void ClickSlot(int index)
        {
            Debug.Log($"Clicked {index}");
        }
    }
}
