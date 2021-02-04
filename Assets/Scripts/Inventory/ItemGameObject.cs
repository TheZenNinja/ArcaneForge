using UnityEngine;

namespace Inventory
{
    public class ItemGameObject : MonoBehaviour, IInteractable
    {
        public ItemStack stack;
        public bool Interact(Player p) => p.inv.AddItem(stack);

        public static ItemGameObject Spawn(ItemStack stack, Vector3 position)
        {
            GameObject g = new GameObject();
            //GameObject g = Instantiate(stack.item.obj, position, Quaternion.identity);
            g.transform.position = position;

            var i = g.GetComponent<ItemGameObject>();
            if (i == null)
                i = g.AddComponent<ItemGameObject>();
            i.stack = stack;
            return i;
        }
    }
}