using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Crafting
{
    public class Anvil : MonoBehaviour, IInteractable
    {
        public Zone zone;

        protected List<IngotGameObject> ingots = new List<IngotGameObject>();

        public int maxInRow = 3;
        public Vector3 ingotOffset;
        public Vector3 ingotTiling;

        public Vector3 alignmentDir;
        AudioSource audio;
        public bool canCraft()
        {
            if (currentPart == 0)
                return false;
            if (ingots.Count < PartFunctions.partDatas[currentPart - 1].ingotCost)
                return false;

            MetalMaterial m = ingots[0].material;

            for (int i = 1; i < ingots.Count; i++)
                if (ingots[i].material != m)
                    return false;

            return true;
        }

        public int currentPart;
        public PartFunctions.PartData getPartData => PartFunctions.partDatas[currentPart - 1];

        public AnvilUI ui;

        void Start()
        {
            audio = GetComponent<AudioSource>();
            ui.anvil = this;
            ui.UpdateUI();
            zone.onEnter += ValidateIngot;
            //zone.onExit += ValidateRemoveIngot;
        }
        private void FixedUpdate()
        {
            FilterIngots();

            if (ingots.Count > 0)
                for (int i = 0; i < ingots.Count; i++)
                {
                    Vector3 pos = transform.position + transform.TransformVector(ingotOffset);

                    Vector3 v = new Vector3(
                            ingotTiling.x * (i % maxInRow),
                            ingotTiling.y * (i / maxInRow),
                            ingotTiling.z * (i / maxInRow));

                    ingots[i].transform.forward = transform.TransformDirection(alignmentDir.normalized);
                    ingots[i].transform.position = pos + transform.TransformVector(v);
                }
        }
        public void FilterIngots()
        {
            if (ingots.Count > 0)
                foreach (var i in ingots)
                    if (i.beingDragged)
                    {
                        ingots.Remove(i);
                        ui.UpdateUI();
                        FilterIngots();
                        return;
                    }
        }
        public string GetPartDisplayData()
        {
            if (currentPart == 0)
                return "None";
            else
            {
                var data = PartFunctions.partDatas[currentPart - 1];

                string s = data.ID.GetCleanName();

                if (canCraft())
                    s += $" ({data.ingotCost})";
                else
                    s += $" <color=\"red\">({data.ingotCost})";
                return s;
            }
        }

        public void CycleSelection(bool reverse)
        {
            if (reverse)
                currentPart--;
            else
                currentPart++;

            if (currentPart > PartFunctions.partDatas.Count)
                currentPart = 0;
            if (currentPart < 0)
                currentPart = PartFunctions.partDatas.Count;
            ui.UpdateUI();
        }


        [ContextMenu("Drop Ingots")]
        public void DropIngots()
        {
            if (ingots.Count > 0)
                StartCoroutine(EjectIngots());
            ui.UpdateUI();
        }
        public void ConsumeIngots(int number)
        {
            if (number > ingots.Count)
                throw new System.ArgumentOutOfRangeException("Cant delete more ingots than you own");

            //Debug.Log($"Goal number: {ingots.Count - number}");
            int startNum = ingots.Count - 1;
            for (int i = 0; i < number; i++)
            {
                int index = startNum - i;
                //Debug.Log($"i index: {i}");
                //Debug.Log($"index index: {index}");

                Destroy(ingots[index].gameObject);
                ingots.RemoveAt(index);
            }

            //for (int i = ingots.Count-1; i > ingots.Count - number; i--)
            //{
            //    Debug.Log(i);
            //    Destroy(ingots[i].gameObject);
            //    ingots.RemoveAt(i);
            //}


            //ingots.RemoveRange(ingots.Count-number-1, number);
        }
        private IEnumerator EjectIngots()
        {
            zone.enabled = false;
            foreach (var i in ingots)
            {
                i.TogglePhys(true);
                i.GetComponent<Rigidbody>().AddForce(-alignmentDir * .5f, ForceMode.Impulse);

            }
            yield return new WaitForSeconds(3);
            zone.enabled = true;
        }
        public void ValidateIngot(GameObject g)
        {
           
            var i = g.GetComponent<IngotGameObject>();
            if (i && i.isHeated && !ingots.Contains(i))
            {
                ingots.Add(i);
                i.TogglePhys(false);
                ui.UpdateUI();
            }
        }
        public void ValidateRemoveIngot(GameObject g)
        {
            var i = g.GetComponent<IngotGameObject>();
            if (i && ingots.Contains(i))
            {
                i.TogglePhys(true);
                ingots.Remove(i);
                ui.UpdateUI();
            }
        }
        private void OnDrawGizmosSelected()
        {
            Gizmos.color = Color.yellow;
            Gizmos.DrawLine(transform.position + transform.up, transform.position + transform.up + alignmentDir);
            for (int i = 0; i < 16; i++)
            {
                Vector3 pos = transform.position + transform.TransformVector(ingotOffset);

                Vector3 v = new Vector3(
                        ingotTiling.x * (i % maxInRow),
                        ingotTiling.y * (i / maxInRow),
                        ingotTiling.z * (i / maxInRow));

                Gizmos.DrawSphere(pos + transform.TransformVector(v), 0.1f);
            }
        }

        public bool Interact(Player p)
        {
            if (canCraft())
            {
                var obj = PartObject.Create(transform.position + Vector3.up * 2f, new PartData(getPartData.ID, new CraftingMaterialID(ingots[0].material)));
                obj.transform.forward = transform.TransformDirection(alignmentDir.normalized);

                ConsumeIngots(getPartData.ingotCost);
                audio.Play();
            }
            return canCraft();
        }
    }
}