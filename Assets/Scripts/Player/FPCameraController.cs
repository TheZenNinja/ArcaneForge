using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FPCameraController : MonoBehaviour
{
    [Header("Camera Look")]
    public Vector2 current;
    public float sensitivity = 3;
    public Vector2 clamp = new Vector2(80, -80);

    public bool canlook;

    public PlayerData player;
    public Vector3 playerPositionOffset;
    public Camera cam;
    public LayerMask defaultMask;
    public Vector3 forward => cam.transform.forward;
    public Ray getRay => new Ray(cam.transform.position, cam.transform.forward);
    protected enum FPSTarget
    { 
        Target30 = 30,
        Target60 = 60,
        Target90 = 90,
        Target120 = 120,
        Target144 = 144,
        Target240 = 240,
    }
    [SerializeField]
    protected FPSTarget fPSTarget;
    void Start()
    {
        Application.targetFrameRate = (int)fPSTarget;


        ShowCursor(false);
    }
    private void Update()
    {
        if (canlook && !(InteractableController.isDragging && Input.GetKey(KeyCode.LeftAlt)))
        {
            current.x += Input.GetAxis("Mouse X") * sensitivity;
            current.y += Input.GetAxis("Mouse Y") * sensitivity;
        }

        current.y = Mathf.Clamp(current.y, clamp.y, clamp.x);

        if (current.x > 360)
            current.x -= 360;
        if (current.x < -360)
            current.x += 360;

        player.transform.localEulerAngles = new Vector3(0, current.x, 0);
        transform.localEulerAngles = new Vector3(-current.y, 0, 0);
    }
    public void ShowCursor(bool active = true)
    {
        Cursor.lockState = active ? CursorLockMode.None : CursorLockMode.Locked;
        Cursor.visible = active;
        canlook = !active;
    }
    public bool GetRaycast(out RaycastHit hit, float maxDist = 0, bool hitTrigger = false) => GetRaycast(out hit, defaultMask, maxDist, hitTrigger);
    public bool GetRaycast(out RaycastHit hit, LayerMask mask, float maxDist = 0, bool hitTrigger = false)
    {
        QueryTriggerInteraction trigger = hitTrigger ? QueryTriggerInteraction.Collide : QueryTriggerInteraction.Ignore;

        if (maxDist > 0)
            return Physics.Raycast(getRay, out hit, maxDist, mask, trigger);
        else
            return Physics.Raycast(getRay, out hit, Mathf.Infinity, mask, trigger);
    }
    private void OnDestroy() => ShowCursor();
}
