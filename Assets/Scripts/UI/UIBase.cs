using System.Collections;
using UnityEngine;

namespace Zen.UI
{
    public abstract class UIBase : MonoBehaviour
    {
        public GameObject canvas;
        public bool isActive;
        public virtual void Start()
        {
            canvas.SetActive(false);
        }
        //public virtual void Control()
        //{
        //    if (isActive && Input.GetKeyDown(KeyCode.Escape))
        //        Close();
        //}
        public virtual void Open(PlayerData p)
        {
            canvas.SetActive(true);
            isActive = true;
            UIController.onClose += Close;
            StaticRefences.camController.ShowCursor(true);
        }
        public virtual void Close(PlayerData p)
        {
            canvas.SetActive(false);
            isActive = false;
            StaticRefences.camController.ShowCursor(false);
        }
    }
}