using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;
using TMPro;

namespace Crafting
{
    public class CraftingTable : MonoBehaviour
    {
        [SerializeField] Zone zone;
        public TextMeshPro text;

        private CraftingRecipies.WeaponRecipe recipe;

        public float rotationOffset;
        public float rotationSpeed = 1;
        public float radius = 1;
        public float slideSpeed = 1;

        public Transform orbitTransform;
        public List<PartObject> parts;

        [Space]
        public ParticleSystem startParticle;
        public ParticleSystem finishParticle;
        [Space]
        AudioSource audio;
        public AudioData craftStartSound;
        public AudioData craftFinishSound;

        [Space]
        [Header("Debug")]
        [Range(1, 10)]
        public int debugCount = 2;
        void Start()
        {
            audio = GetComponent<AudioSource>();
            zone.onEnter += ValidateObjectAdd;
            //zone.onExit += ValidateObjectRemove;
            CheckRecipe();
        }

        void FixedUpdate()
        {
            if (parts.Count > 0)
                foreach (var p in parts)
                    if (p.beingDragged)
                    {
                        parts.Remove(p);
                        CheckRecipe();
                        break;
                    }
            UpdatePositions();
        }
        public void ValidateObjectAdd(GameObject g)
        {
            var p = g.GetComponent<PartObject>();
            if (p && !parts.Contains(p))
            {
                if (!p.allowCraft)
                    return;

                parts.Add(p);
                p.TogglePhys(false);
                CheckRecipe();
            }
        }
        public void CheckRecipe()
        {
            if (parts.Count > 1)
                recipe = CraftingRecipies.GetWeaponRecipe(parts);
            else
                recipe = null;
            
            Debug.Log(recipe);
            
            text.text = recipe == null ? "" : recipe.name;
        }
        public void ValidateObjectRemove(GameObject g)
        {
            var p = g.GetComponent<PartObject>();
            if (p && parts.Contains(p))
            {
                parts.Remove(p);
                p.TogglePhys(true);
                CheckRecipe();
            }
        }
        public void UpdatePositions()
        {
            rotationOffset += Time.fixedDeltaTime * rotationSpeed;
            if (rotationOffset > 360)
                rotationOffset -= 360;

            if (parts.Count <= 0)
                return;

            if (parts.Count == 1 && !parts[0].beingDragged)
                parts[0].position = Vector3.Lerp(parts[0].position, orbitTransform.TransformPoint(Vector3.zero), Time.fixedDeltaTime * slideSpeed);
            else
            {
                float angle = 360f / parts.Count;
                for (int i = 0; i < parts.Count; i++)
                {
                    if (parts[i].beingDragged)
                        continue;

                    Vector2 v = ClassExtentions.FromRotation(angle * i + rotationOffset);

                    parts[i].position = Vector3.Lerp(parts[i].position, orbitTransform.TransformPoint(new Vector3(v.x, 0, v.y) * radius), Time.fixedDeltaTime * slideSpeed);
                }
            }
        }
        public void PlayStartCraftParticle()
        {
            startParticle.Play();
            audio.SetData(craftStartSound);
            audio.Play();
        }
        public void PlayFinishCraftParticle()
        {
            finishParticle.Play();
            WeaponObject.Create(orbitTransform.position, recipe.output, (from p in parts select p.data));

            while (parts.Count > 0)
            { 
                Destroy(parts[0].gameObject);
                parts.RemoveAt(0);
            }
            audio.SetData(craftFinishSound);
            audio.Play();

            CheckRecipe();
        }
        public void TryCraft(Player p)
        {
            if (recipe == null)
                return;
            GetComponent<Animator>().SetTrigger("Craft");
            text.text = "";
        }


        public void OnDrawGizmosSelected()
        {
            Gizmos.color = Color.gray;
            if (!orbitTransform)
                return;
            if (debugCount == 1)
                Gizmos.DrawSphere(orbitTransform.TransformPoint(Vector3.zero), .1f);
            else if (debugCount > 0)
            {
                float angle = 360f / debugCount;
                for (int i = 0; i < debugCount; i++)
                {
                    Vector2 v = ClassExtentions.FromRotation(angle * i + rotationOffset);

                    Gizmos.DrawSphere(orbitTransform.TransformPoint(new Vector3(v.x, 0, v.y) * radius), .1f);
                }
            }
        }
    }
}