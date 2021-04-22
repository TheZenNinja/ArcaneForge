using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Zen.UI.Palette
{
    [CreateAssetMenu(menuName = "UI/Color Palette")]
    public class UIColorPalette : ScriptableObject
    {
        public Color mainLight = Color.white;
        public Color mainDark = Color.black;
        public Color bg = Color.white;
    }
}