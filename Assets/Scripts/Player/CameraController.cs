using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour
{
    [Header("Camera Look")]
    public Vector2 current;
    public Vector2 sensitivity = Vector2.one * 3;
    public Vector2 clamp = new Vector2(80, -80);

    public bool canlook;

    public Player player;
    public Vector3 playerPositionOffset;
    public float moveSpeed = 15;
    public Vector3 camPosOffset = Vector3.up;
    public Vector3 camOffsetAngle = -Vector3.forward;
    public float maxOffsetDist = 5;
    public LayerMask camPosMask;
    public Camera cam;
    public LayerMask defaultMask;
    public Ray getRay => new Ray(cam.transform.position, cam.transform.forward);


    private void OnValidate()
    {
        if (cam && player)
        {
            Ray r = new Ray(transform.position, transform.TransformVector(camOffsetAngle.normalized));
            Debug.DrawLine(r.GetPoint(maxOffsetDist), r.GetPoint(maxOffsetDist) + Vector3.up, Color.green);
            cam.transform.position = r.GetPoint(maxOffsetDist);
            transform.position = player.center;
        }
    }
    void Start()
    {
        ShowCursor(true);
        StartCoroutine(LateFixedUpate());
    }
    private void Update()
    {
        if (canlook)
        {
            current.x += Input.GetAxis("Mouse X") * sensitivity.x;
            current.y += Input.GetAxis("Mouse Y") * sensitivity.y;
        }
        current.y = Mathf.Clamp(current.y, clamp.y, clamp.x);

        if (current.x > 360)
            current.x -= 360;
        if (current.x < -360)
            current.x += 360;

        player.transform.localEulerAngles = new Vector3(0, current.x, 0);
        transform.localEulerAngles = new Vector3(-current.y, current.x, 0);
    }
    IEnumerator LateFixedUpate()
    {
        while (true)
        {
            yield return new WaitForFixedUpdate();
            UpdateCam();
        }
    }
    public Vector3 getCamPos()
    {
        Ray r = new Ray(transform.position, transform.TransformVector(camOffsetAngle.normalized));

        RaycastHit hit;

        if (Physics.Raycast(r, out hit, maxOffsetDist, camPosMask, QueryTriggerInteraction.Ignore))
            return r.GetPoint(hit.distance - 0.25f);
        else
            return r.GetPoint(maxOffsetDist);

    }
    void UpdateCam()
    {
        if (cam && player)
        {
            cam.transform.position = getCamPos();
            transform.position = Vector3.Lerp(transform.position, player.center, Time.deltaTime * moveSpeed);
        }
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
        if (maxDist > 0)
            return Physics.Raycast(getRay, out hit, maxDist, mask, hitTrigger ? QueryTriggerInteraction.Collide : QueryTriggerInteraction.Ignore);
        else
            return Physics.Raycast(getRay, out hit, Mathf.Infinity, mask, hitTrigger ? QueryTriggerInteraction.Collide : QueryTriggerInteraction.Ignore);
    }
    private void OnDestroy() => ShowCursor();
}
