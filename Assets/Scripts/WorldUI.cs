using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WorldUI : UIBase, IInteractable
{
    public override void Start()
    {
        var c = GetComponent<Canvas>();
        c.worldCamera = FindObjectOfType<FPCameraController>().cam;
        canvas = c.gameObject;
    }
    public void Update()
    {
        if (isActive ==true && (Input.GetKeyDown(KeyCode.Tab) || Input.GetKeyDown(KeyCode.F) || Input.GetKeyDown(KeyCode.Escape)))
            Close(null);

    }
    public virtual bool Interact(PlayerData p)
    {
        Open(p);
        return true;
    }
    public override void Open(PlayerData player)
    {
        //Debug.Log("Interacted with canvas");
        base.Open(player);
        canvas.SetActive(true);
    }

    public override void Close(PlayerData player)
    { 
        base.Close(player);
        canvas.SetActive(true);
    }
    public void ButtonTest() => Debug.Log("Pushed button");
}
