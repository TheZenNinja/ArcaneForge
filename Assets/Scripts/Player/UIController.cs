using System;
using System.Collections;
using UnityEngine;

public class UIController : MonoBehaviour
{
    public static bool inUI;
    public static Action<PlayerData> onClose;

    // Update is called once per frame
    void Update()
    {
        if (inUI)
        {
            if (Input.GetKeyDown(KeyCode.Escape) || Input.GetKeyDown(KeyCode.Return) || Input.GetKeyDown(KeyCode.Tab))
            {
                Debug.Log("Close");
                onClose?.Invoke(StaticRefences.player);
                //clears the action while preventing null reference exceptions
                onClose = delegate { };
                StaticRefences.camController.ShowCursor(false);
            }
        }
    }
}
