using System.Collections.Generic;
using UnityEngine;
using Crafting;
using TMPro;

namespace Zen.Economy
{
    public class IngotShop : MonoBehaviour
    {
        public RectTransform entryUIParent;
        public List<IngotShopEntry> entries;
        public TextMeshProUGUI totalTxt;
        public int totalCost;

        public Transform dropZone;

        void Start()
        {
            AddEntries();
        }

        private void AddEntries()
        {
            GameObject pref = Resources.Load<GameObject>("UI/Shop UI Item");

            foreach (var m in MaterialDataFunctions.metals)
            {
                var e = Instantiate(pref, entryUIParent).GetComponent<IngotShopEntry>();
                e.Setup(m, this);
                entries.Add(e);
            }

            entryUIParent.sizeDelta = new Vector2(entryUIParent.sizeDelta.x, 25 * entries.Count);
        }
        public void Buy()
        {
            if (totalCost > 0)
            {
                var ingot = Resources.Load<GameObject>("Objects/Ingot");
                foreach (var e in entries)
                {
                    if (e.currentCount > 0)
                        for (int i = 0; i < e.currentCount; i++)
                        {
                            IngotGameObject.Spawn(e.material.ID, dropZone.position);
                        }
                }
                StaticRefences.player.money -= totalCost;
            }
        }
        public void ResetCost()
        {
            if (entries.Count > 0)
                foreach (var e in entries)
                    e.ResetCount();
            UpdateCost();
        }
        public void UpdateCost()
        {
            totalCost = 0;
            
            foreach (var e in entries)
                totalCost += e.cost;

            totalTxt.text = totalCost.ToString();
        }
    }
}