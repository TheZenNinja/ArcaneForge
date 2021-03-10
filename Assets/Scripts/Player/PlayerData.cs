using UnityEngine;
using Inventory;

public class PlayerData : MonoBehaviour
{
    private InventoryController _inv;
    public InventoryController inv => _inv;
    public InventoryUI invUi;
    public PlayerMovement move;
    public Health hp;
    public Vector3 center => transform.position + transform.TransformVector(Vector3.up * .5f);
    public void Awake()
    {
        StaticRefences.player = this;
        _inv = GetComponent<InventoryController>();
        move = GetComponent<PlayerMovement>();
        hp = GetComponent<Health>();
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
