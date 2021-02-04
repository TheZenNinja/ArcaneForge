using System;
using System.Collections;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

namespace Crafting
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