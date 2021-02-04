using System.Collections;
using UnityEngine;
using TMPro;

namespace Crafting
{
    public class CraftingBook : DraggableObject
    {
        public int currentRecipe = 0;

        public TextMeshProUGUI nameTxt, ingredTxt;
        
        void Start()
        {
            LoadRecipe();
        }
        public void LoadRecipe()
        {
            var recipe = CraftingRecipies.anvilRecipies[currentRecipe];
            nameTxt.text = recipe.name;
            string ingred = "Ingredients:\n";
            foreach (var i in recipe.inputs)
                ingred += $"- {i.GetCleanName()}\n";
            ingredTxt.text = ingred;
        }
        public void CycleRecipies(bool reverse)
        {
            if (reverse)
                currentRecipe--;
            else
                currentRecipe++;

            if (currentRecipe < 0)
                currentRecipe = CraftingRecipies.anvilRecipies.Count - 1;
            else if (currentRecipe > CraftingRecipies.anvilRecipies.Count - 1)
                currentRecipe = 0;

            LoadRecipe();
        }
    }
}