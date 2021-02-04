using UnityEngine;
using System.Globalization;
namespace Inventory
{
#pragma warning disable CS0660 // Type defines operator == or operator != but does not override Object.Equals(object o)
#pragma warning disable CS0661 // Type defines operator == or operator != but does not override Object.GetHashCode()
    [CreateAssetMenu(menuName = "Item/Resource")]
    public class ResourceItem : Item
#pragma warning restore CS0660 // Type defines operator == or operator != but does not override Object.Equals(object o)
#pragma warning restore CS0661 // Type defines operator == or operator != but does not override Object.GetHashCode()
    {
        public ResourceType type;
        public ResourceMaterial material;

        public ResourceItem Create(ResourceType type, ResourceMaterial material)
        {
            var i = CreateInstance<ResourceItem>();
            i.type = type;
            i.material = material;
            i.UpdateData();
            return i;
        }

        private void UpdateData()
        {
            itemName = $"{material} {type}";
            itemName = CultureInfo.GetCultureInfo("en").TextInfo.ToTitleCase(itemName);
            name = itemName;
        }

        private void OnValidate()
        {
            UpdateData();
        }


        public static bool operator ==(ResourceItem x, ResourceItem y)
        {
            if (x.GetType() != y.GetType())
                return false;
            return x.type == y.type && x.material == y.material;
        }
        public static bool operator !=(ResourceItem x, ResourceItem y) => !(x == y);

    }
}