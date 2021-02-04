using System;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using TMPro;

namespace Inventory
{
    public class InventorySlotUI : MonoBehaviour, IPointerDownHandler
    {
        public Image image;
        public TextMeshProUGUI nameTxt;
        public TextMeshProUGUI countTxt;
        public int index;
        //do i even need to remove list on destroy?
        public Action<int> onClick;
        public void Setup(int index, Action<int> action)
        {
            this.index = index;
            onClick += action;
        }
        public void UpdateData(ItemStack stack)
        {
            nameTxt.gameObject.SetActive(stack.item != null);
            countTxt.gameObject.SetActive(stack.item != null);
            if (stack.item != null)
            {
                nameTxt.text = stack.item.itemName;
                countTxt.text = stack.count.ToString();
                image.sprite = stack.item.sprite;
            }
        }

        public void OnPointerDown(PointerEventData eventData)
        {
            onClick?.Invoke(index);
        }
    }
}


//add drag and drop
//add heating up ingots on forge