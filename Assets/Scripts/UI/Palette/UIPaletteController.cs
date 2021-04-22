using System.Collections;
using UnityEngine;

namespace Zen.UI.Palette
{
    public class UIPaletteController : MonoBehaviour
    {

        public UIColorPalette palette;

        public Camera cam;

        [ContextMenu("Update Colors")]
        private void OnValidate()
        {
            if (palette != null)
            {
                if (cam)
                    cam.backgroundColor = palette.mainLight;

                foreach (var c in GetComponentsInChildren<UIPaletteSetter>())
                    c.SetColor(palette);
            }
        }
    }
}