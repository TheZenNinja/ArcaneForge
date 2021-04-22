using System.Collections;
using UnityEngine;

namespace Zen.UI
{
    public abstract class UIBase : MonoBehaviour
    {
        public GameObject canvas;
        public bool alwaysOpen;
        public bool isActive;
        public virtual void Start()
        {
            canvas = GetComponent<GameObject>();
            if (!alwaysOpen)
            canvas.SetActive(false);
        }
        public virtual void Open(PlayerData p)
        {
            if (UIController.inUI)
                return;
            if (!alwaysOpen)
            canvas.SetActive(true);
            isActive = true;
            UIController.inUI = true;
            UIController.onClose += Close;
            StaticRefences.camController.ShowCursor(true);
        }
        public virtual void Close(PlayerData p)
        {
            if (!alwaysOpen)
            canvas.SetActive(false);
            isActive = false;
            UIController.onClose -= Close;
            StaticRefences.camController.ShowCursor(false);
        }
    }
}