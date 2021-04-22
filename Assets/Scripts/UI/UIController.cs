using System;
using System.Collections;
using UnityEngine;

namespace Zen.UI
{
    public class UIController : MonoBehaviour
    {
        public static bool inUI;
        public static Action<PlayerData> onClose;

#if UNITY_EDITOR
        public bool isInUI;
#endif

        // Update is called once per frame
        void Update()
        {
            if (inUI)
            {
#if UNITY_EDITOR  
                isInUI = inUI;
                if (Input.GetKeyDown(KeyCode.Tab))
                    CloseUI();
#endif
                if (Input.GetKeyDown(KeyCode.Escape))
                    CloseUI();
            }
        }
        public void CloseUI()
        {
            Debug.Log("Close");
            onClose?.Invoke(StaticRefences.player);
            //clears the action while preventing null reference exceptions
            onClose = delegate { };
            StaticRefences.camController.ShowCursor(false);
            inUI = false;
        }
    }
}