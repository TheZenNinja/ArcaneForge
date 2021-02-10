using UnityEngine;
using Inventory;

public class Player : MonoBehaviour
{
    public static Player instance;
    private InventoryController _inv;
    public InventoryController inv => _inv;
    public InventoryUI invUi;
    public PlayerMovement move;
    public Health hp;
    public Vector3 center => transform.position + transform.TransformVector(Vector3.up * .5f);
    public void Awake()
    {
        instance = this;
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
