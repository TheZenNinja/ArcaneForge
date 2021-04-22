using System.Collections;
using UnityEngine;
using Zen.UI;

public class InteractableController : MonoBehaviour
{
    FPCameraController cam;
    PlayerData player;
    public KeyCode interactKey;
    public float currentObjDist;
    public Vector2 distanceClamp = new Vector2(1,3);
    public float rotateSpeed = 10;
    public bool invertRotation;

    [SerializeField]
    private GameObject targetObj;
    [SerializeField]
    private DraggableObject dragObj;
    public static bool isDragging;

    public float holdTime;
    public bool isHolding;
    private void Start()
    {
        cam = StaticRefences.camController;
        player = StaticRefences.player;

    }
    public void Update()
    {
        if (UIController.inUI)
            return;

        if (Input.GetKeyDown(interactKey))
        {
            RaycastObject();
            if (targetObj != null)
                isHolding = true;
        }

        if (isHolding)
        {
            if (holdTime < .25f)
            {
                holdTime += Time.deltaTime;
                if (Input.GetKeyUp(interactKey))
                    InteractObject();
            }
            else
                StartDraggingObject();
        }


        if (dragObj != null)
            DragObject();
        isDragging = dragObj != null;
    }
    public void RaycastObject()
    {
        RaycastHit hit;
        if (cam.GetRaycast(out hit, distanceClamp.y, true))
        {
            //Debug.Log(hit.collider.gameObject.name);
            var i = hit.collider.GetComponent<IInteractable>();
            if (i != null)
                targetObj = hit.collider.gameObject;
        }
        //else
        //    Debug.Log("Hit Nothing");
    }
    public void InteractObject()
    {
        targetObj.GetComponent<IInteractable>().Interact(player);
        targetObj = null;
        isHolding = false;
        holdTime = 0;
    }
    public void StartDraggingObject()
    {
        var d = targetObj.GetComponent<DraggableObject>();
        if (d != null)
        {
            dragObj = d;
            dragObj.StartDrag(cam.transform);
            currentObjDist = Vector3.Distance(cam.transform.position, d.transform.position);
        }
        else
            InteractObject();

        isHolding = false;
        targetObj = null;
        holdTime = 0;
    }
    public void DragObject()
    {
        //Debug.Log(Input.GetAxisRaw("Mouse ScrollWheel"));
        if (Input.GetAxisRaw("Mouse ScrollWheel") >= 0.1f)
            currentObjDist = Mathf.Clamp(currentObjDist + 0.2f, distanceClamp.x, distanceClamp.y);
        else if (Input.GetAxisRaw("Mouse ScrollWheel") <= -0.1f)
            currentObjDist = Mathf.Clamp(currentObjDist - 0.2f, distanceClamp.x, distanceClamp.y);

        if (Input.GetKey(KeyCode.LeftAlt))
        {
            float s = invertRotation ? -rotateSpeed : rotateSpeed;
            Debug.DrawRay(dragObj.transform.position, cam.cam.transform.right, Color.red, .1f);
            dragObj.transform.Rotate(cam.cam.transform.right, Input.GetAxis("Mouse Y") * s, Space.World);
            Debug.DrawRay(dragObj.transform.position, cam.cam.transform.up, Color.green, .1f);
            dragObj.transform.Rotate(cam.cam.transform.up, Input.GetAxis("Mouse X") * s, Space.World);
        }

        currentObjDist = Mathf.Clamp(currentObjDist, distanceClamp.x, distanceClamp.y);

        RaycastHit hit;
        if (cam.GetRaycast(out hit, currentObjDist, false))
            dragObj.position = (hit.point);
        else
        dragObj.position = (cam.getRay.GetPoint(currentObjDist));


        if (Input.GetKeyUp(interactKey))
        {
            dragObj.StopDrag();
            dragObj = null;
        }
    }
}
