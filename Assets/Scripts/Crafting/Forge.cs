using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Crafting
{
    public class Forge : TickableObject
    {
        public Zone heatingZone;
        public List<HeatableObject> ingots;
        public override void Start()
        {
            base.Start();
            heatingZone.onEnter += FilterObjectEnter;
            heatingZone.onExit += FilterObjectExit;
            TickManager.tick += Tick;
        }
        //private void FixedUpdate()
        //{
        //    if (ingots.Count > 0)
        //        foreach (var i in ingots)
        //            if (i.beingDragged)
        //            {
        //                ingots.Remove(i);
        //                break;
        //            }
        //}
        public override void Tick()
        {
            if (ingots.Count > 0)
                foreach (var i in ingots)
                    i.ChangeHeatLevel(1);
        }
        public void FilterObjectEnter(GameObject obj)
        {
            var i = obj.GetComponent<HeatableObject>();
            if (i && !ingots.Contains(i))
                ingots.Add(i);
        }
        public void FilterObjectExit(GameObject obj)
        {
            var i = obj.GetComponent<HeatableObject>();
            if (i && ingots.Contains(i))
                    ingots.Remove(i);
        }
    }
}