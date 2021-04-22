using System.Collections;
using UnityEngine;
using TMPro;
using UnityEngine.UI;
namespace Zen.Economy
{
    public class IngotShopEntry : MonoBehaviour
    {
        public Crafting.MaterialDataFunctions.MaterialData material;
        public TextMeshProUGUI nameTxt;
        public TextMeshProUGUI countTxt;
       

        private IngotShop shop;

        [SerializeField]
        private int _curCount;
        public int currentCount
        {
            get =>_curCount;
            set
            {
                _curCount = value;
                if (countTxt)
                    countTxt.text = value.ToString();

                var shop = GetComponentInParent<IngotShop>();
                if (shop)
                    shop.UpdateCost();
            }
        }
        public int cost => currentCount * material.moneyValue;

        public void Setup(Crafting.MaterialDataFunctions.MaterialData data, IngotShop shop)
        {
            this.shop = shop;
            material = data;
            nameTxt.text = data.GetName();
            currentCount = 0;
        }

        public void ChangeCount(bool reverse)
        {
            int max = StaticRefences.player.money;
            if (max > 99)
                max = 99;
            if (reverse)
                currentCount--;
            else
            {
                if (shop.totalCost + material.moneyValue <= StaticRefences.player.money)
                    currentCount++;
            }

            currentCount = Mathf.Clamp(currentCount, 0, max);
        }
        public void ResetCount() => currentCount = 0;
    }
}