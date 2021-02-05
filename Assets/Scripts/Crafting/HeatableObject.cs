using System;
using UnityEngine;

namespace Crafting
{
    public class HeatableObject : DraggableObject
    {
        public MetalMaterial material;
        [SerializeField] protected Renderer r;
        [SerializeField] protected Material mat;
        [SerializeField] protected ParticleSystem ps;
        [SerializeField]
        protected int maxHeatLevel = 30;
        public int currentHeatLevel;
        protected bool initialized;
        protected virtual void Start()
        {
            if (!initialized)
                Setup();
        }
        [ContextMenu("Init")]
        public virtual void Setup()
        {
            UpdateMetalData();
            //ps = CreateParticleSystem();
            initialized = true;
        }
        public void UpdateMetalData()
        {
            r = GetComponentInChildren<Renderer>();
            var data = MaterialDataFunctions.FindMetal(material);
            maxHeatLevel = data.maxHeatLevel;

            mat = new Material(data.GetIngredientShader);
            r.material = mat;
        }
        [ContextMenu("Setup Particlesystem")]
        public void CreateParticleSystem()
        {
            if (ps == null)
            {
                var basePS = Resources.Load<GameObject>("FXs/Heat Particle");

                var t = Instantiate(basePS, transform).transform;
                t.localScale = Vector3.one;
                t.localEulerAngles = Vector3.zero;
                t.localPosition = Vector3.zero;
            }
            ps = GetComponentInChildren<ParticleSystem>();
            var shape = ps.shape;
            shape.shapeType = ParticleSystemShapeType.Box;
            var col = GetComponent<BoxCollider>();
            shape.position = col.center;
            shape.scale = col.size;
        }

        public bool isHeated => currentHeatLevel >= maxHeatLevel;
        public float heatPercent => (float)currentHeatLevel / maxHeatLevel;
        public void ChangeHeatLevel(int amt)
        {
            currentHeatLevel = Mathf.Clamp(currentHeatLevel + amt, 0, maxHeatLevel);
            mat.SetFloat("_HeatLevel", heatPercent);
            if (!isHeated && ps.isPlaying)
                ps.Stop();
            else if (isHeated && !ps.isPlaying)
                ps.Play();
        }
        public void MaxHeat() => ChangeHeatLevel(maxHeatLevel);
        
    }
}
