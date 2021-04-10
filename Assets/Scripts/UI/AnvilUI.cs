using System;
using System.Collections;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using Crafting;

namespace Zen.UI
{
    public class AnvilUI : WorldUI
    {
        [HideInInspector]
        public Anvil anvil;
        public TextMeshProUGUI text;

        public void UpdateUI()
        {
            text.text = anvil.GetPartDisplayData();
        }
    }
}