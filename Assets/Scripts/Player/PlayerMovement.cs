using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    CharacterController cc;
    public Vector3 center => transform.position + transform.TransformVector(Vector3.up * 0.5f);
    public float speed;
    public float sprintSpeed;
    public bool isSprinting;

    public float accel;
    public float gravity = 20;
    public float jumpHeight = 3;
    [SerializeField] Vector2 dir;
    Vector3 moveDir => transform.TransformVector(dir.toV3().normalized);
    public Vector3 veloticy;

    private void Start()
    {
        cc = GetComponent<CharacterController>();
    }

    public void HandleInput()
    {
        dir = new Vector2(Input.GetAxisRaw("Horizontal"), Input.GetAxisRaw("Vertical"));
        if (cc.isGrounded && Input.GetKey(KeyCode.Space))
            Jump();
        isSprinting = Input.GetKey(KeyCode.LeftShift);
        //if (Input.GetKeyDown(KeyCode.LeftShift))
        //    Dash();
    }

    private void FixedUpdate()
    {
        if (cc.isGrounded)
        {
            if (veloticy.y < -0.1f)
                veloticy.y = -0.1f;
        }
        else
        {
            if (veloticy.y > 0)
                if (Input.GetKeyUp(KeyCode.Space))
                    veloticy.y *= .5f;

            veloticy.y -= gravity * Time.fixedDeltaTime;
        }

        veloticy = veloticy.LerpXZ(moveDir * (isSprinting ? sprintSpeed : speed), accel * Time.fixedDeltaTime);
        cc.Move(veloticy * Time.fixedDeltaTime);
    }
    //private void Dash()
    //{
    //    cc.Move(moveDir * 10);
    //}
    private void Jump()
    {
        veloticy.y = Mathf.Sqrt(2 * gravity * jumpHeight);
    }
}
