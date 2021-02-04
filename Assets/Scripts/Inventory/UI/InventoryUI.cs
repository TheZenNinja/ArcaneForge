using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace Inventory
{
    public class InventoryUI : MonoBehaviour
    {
        public InventoryController controller;

        public GridLayoutGroup grid;
        public List<InventorySlotUI> slots;
        public GameObject slotPref;

        public void Load(InventoryController cont)
        {
            controller = cont;
            for (int i = 0; i < cont.GetAllItems().Count; i++)
            {
                slots.Add(Instantiate(slotPref, grid.transform).GetComponent<InventorySlotUI>());
                slots[i].Setup(i, cont.ClickSlot);
            }
        }

        public void Update()
        {
            for (int i = 0; i < slots.Count; i++)
                slots[i].UpdateData(controller.GetItemStackInSlot(i));
        }
    }
}