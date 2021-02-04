using System.Collections;
using UnityEngine;

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
    public virtual void Open(Player p)
    {
        canvas.SetActive(true);
        isActive = true;
        UIController.onClose += Close;
        FindObjectOfType<FPCameraController>().ShowCursor(true);
    }
    public virtual void Close(Player p)
    {
        canvas.SetActive(false);
        isActive = false;
        FindObjectOfType<FPCameraController>().ShowCursor(false);
    }
}
