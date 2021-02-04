using UnityEngine;

public class Player : MonoBehaviour
{
    private Inventory.InventoryController _inv;
    public Inventory.InventoryController inv => _inv;
    public Inventory.InventoryUI invUi;
    private PlayerMovement move;
    public Vector3 center => transform.position + transform.TransformVector(Vector3.up * .5f);
    public void Awake()
    {
        _inv = GetComponent<Inventory.InventoryController>();
        move = GetComponent<PlayerMovement>();
        inv.ForceSetItem(ItemDictionary.instance.GetItemFromName("Paper"), 0);
        inv.ForceSetItem(ItemDictionary.instance.GetItemFromName("Copper Bar"), 1);
        inv.GetItemStackInSlot(0).item.LogType();
        inv.GetItemStackInSlot(1).item.LogType();
    }
    private void Start()
    {
        invUi.Load(inv);
    }
    public void Update()
    {
        move.HandleInput();
    }
}
