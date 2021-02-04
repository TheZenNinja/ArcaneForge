using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Drawing;
using System.IO;

namespace TextureGen
{
    public class TextureGenerator : MonoBehaviour
    {
        [System.Serializable]
        public struct MaterialData
        {
            public Material mat;
            public Renderer[] renderers;
            public UnityEngine.Color color;
            [Range(0,1)]
            public float metallic;
            [Range(0,1)]
            public float smooth;
        }

        public MaterialData primary;
        public MaterialData accent;
        public MaterialData darker;
        public MaterialData cloth;
        public string fileName;
        public static string path => Application.dataPath + $"/TextureGenerator";
        private void UpdateMaterials()
        {
            foreach (var m in new MaterialData[] { primary, accent,darker, cloth})
            {
                m.mat.color = m.color;
                m.mat.SetFloat("_Metallic", m.metallic);
                m.mat.SetFloat("_Smoothness", m.smooth);
            }
        }
        private void OnValidate()
        {
            UpdateMaterials();
        }
        [ContextMenu("Export")]
        public void ExportColors()
        {
            int size = 128;
            Bitmap b = new Bitmap(size * 4, size * 2);
            using (System.Drawing.Graphics g = System.Drawing.Graphics.FromImage(b))
            {
                g.SmoothingMode = System.Drawing.Drawing2D.SmoothingMode.None;
                g.InterpolationMode = System.Drawing.Drawing2D.InterpolationMode.NearestNeighbor;

                Rectangle rect = new Rectangle(0, 0, size, size);

                g.FillRectangle(new SolidBrush(ConvertColor(primary.color)), rect);
                rect.X += size;
                g.FillRectangle(new SolidBrush(ConvertColor(accent.color)), rect);
                rect.X += size;
                g.FillRectangle(new SolidBrush(ConvertColor(darker.color)), rect);
                rect.X += size;
                g.FillRectangle(new SolidBrush(ConvertColor(cloth.color)), rect);
                
                rect.X = 0;
                rect.Y = size;
                
                g.FillRectangle(new SolidBrush(ColorFromAlpha(primary.metallic)), rect);
                rect.X += size;
                g.FillRectangle(new SolidBrush(ColorFromAlpha(accent.metallic)), rect);
                rect.X += size;
                g.FillRectangle(new SolidBrush(ColorFromAlpha(darker.metallic)), rect);
                rect.X += size;
                g.FillRectangle(new SolidBrush(ColorFromAlpha(cloth.metallic)), rect);
            }
            Debug.Log($"{path}/{fileName}.png");
            if (fileName == "")
                b.Save($"{path}/Texture.png", System.Drawing.Imaging.ImageFormat.Png);
            else
                b.Save($"{path}/{fileName}.png", System.Drawing.Imaging.ImageFormat.Png);
        }
        public System.Drawing.Color ConvertColor(UnityEngine.Color baseC)
        {
            int r = Mathf.RoundToInt(baseC.r * 255);
            int g = Mathf.RoundToInt(baseC.g * 255);
            int b = Mathf.RoundToInt(baseC.b * 255);
            return System.Drawing.Color.FromArgb(255, r,g,b);
        }
        public System.Drawing.Color ColorFromAlpha(float a)
        {
            int v = Mathf.RoundToInt(a * 255);
            return System.Drawing.Color.FromArgb(255,v,v,v);
        }
    }
}