using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Zen.UI
{
    public class WorldUI : UIBase, IInteractable
    {
        public Collider interactCol;
        public override void Start()
        {
            var c = GetComponent<Canvas>();
            c.worldCamera = StaticRefences.camController.cam;
            canvas = c.gameObject;
        }
        public bool Interact(PlayerData p)
        {
            Open(p);
            Debug.Log("Interact");
            return true;
        }
        public override void Open(PlayerData player)
        {
            //Debug.Log("Interacted with canvas");
            base.Open(player);
            canvas.SetActive(true);
            if (interactCol)
                interactCol.enabled = false;
        }

        public override void Close(PlayerData player)
        {
            base.Close(player);
            canvas.SetActive(true);
            if (interactCol)
                interactCol.enabled = true;
        }
    }
}