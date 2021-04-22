using System.Collections;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

namespace Zen.UI.Palette
{
    public class UIPaletteSetter : MonoBehaviour
    {
        [SerializeField]
        private ColorType type;
        private enum ColorType
        { 
            mainLight, mainDark
        }
        public void SetColor(UIColorPalette palette)
        {
            Color c;
            switch (type)
            {
                default:
                case ColorType.mainLight:
                    c = palette.mainLight;
                    break;
                case ColorType.mainDark:
                    c = palette.mainDark;
                    break;
            }

            Image i = GetComponent<Image>();
            if (i)
                i.color = c;

            var t = GetComponent<TextMeshProUGUI>();
            if (t)
                t.color = c;
        }
    }
}