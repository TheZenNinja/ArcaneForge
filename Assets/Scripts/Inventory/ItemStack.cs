using System;

namespace Inventory
{
    [Serializable]
    public class ItemStack
    {
        public Item item;
        public int count;

        public ItemStack()
        {
            item = null;
            count = 0;
        }

        public ItemStack(Item item, int count = 1)
        {
            this.item = item;
            this.count = count;
        }

        public static ItemStack operator +(ItemStack s, int i)
        {
            s.count += i;
            return s;
        }
        public static ItemStack operator -(ItemStack s, int i)
        {
            s.count -= i;
            return s;
        }
        public static implicit operator ItemStack(Item i) => new ItemStack(i);
        public static implicit operator Item(ItemStack i) => i.item;
    }
}