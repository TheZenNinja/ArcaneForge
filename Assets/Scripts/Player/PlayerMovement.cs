using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    [System.Serializable]
    public class VelocityInfo
    {
        public string name;
        public Vector3 force;
        public float timeLeft;
        public bool decayOverTime;
        public bool localSpace;
        public bool destroy;
        public VelocityInfo(Vector3 force, float timeLeft, bool decayOverTime = true, bool localSpace = false, string name = "")
        {
            this.force = force;
            this.timeLeft = timeLeft;
            this.localSpace = localSpace;
            this.decayOverTime = decayOverTime;
            this.name = name;
            destroy = false;
        }

        public void TickTime(float change)
        {
            timeLeft -= change;
            if (timeLeft <= 0)
                destroy = true;
        }

        public void ApplyDrag(float drag)
        {
            force = Vector3.Lerp(force, Vector3.zero, drag);
            if (force.sqrMagnitude <= 0.01f)
                destroy = true;
        }
    }
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
    private List<VelocityInfo> additionalVelocities = new List<VelocityInfo>();

    [Space]
    [Header("Dash")]
    public float dashDistance;
    public float dashDuration;
    public Timer dashCooldown;

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
        if (dashCooldown.finished && Input.GetKeyDown(KeyCode.LeftShift))
            Dash();
    }

    private void FixedUpdate()
    {
        dashCooldown.Tick();
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
            if (!(additionalVelocities.Count > 0 && additionalVelocities.Exists(x => x.name == "Dash")))
                veloticy.y -= gravity * Time.fixedDeltaTime;
        }

        var totalAdditionalLocalVel = Vector3.zero;
        var totalAdditionalGlobalVel = Vector3.zero;
        if (additionalVelocities.Count > 0)
            foreach (var v in additionalVelocities)
            {
                if (v.localSpace)
                    totalAdditionalLocalVel += v.force;
                else
                    totalAdditionalGlobalVel += v.force;
            }

        veloticy = veloticy.LerpXZ(moveDir * (isSprinting ? sprintSpeed : speed), accel * Time.fixedDeltaTime);
        var totalVel = veloticy + totalAdditionalGlobalVel + transform.TransformVector(totalAdditionalLocalVel);
        cc.Move(totalVel * Time.fixedDeltaTime);

        if (additionalVelocities.Count > 0)
        {
            for (int i = 0; i < additionalVelocities.Count; i++)
            {
                var v = additionalVelocities[i];
                if (v.decayOverTime)
                    v.ApplyDrag(1);
                v.TickTime(Time.fixedDeltaTime);
            }
            additionalVelocities.RemoveAll(x => x.destroy);
        }
    }
    private void Dash()
    {
        dashCooldown.Start();
        AddForce(new VelocityInfo(moveDir * (dashDistance/dashDuration), .1f, false, name: "Dash"));
        Player.instance.hp.InvulernableFor(dashDuration * 2f);
    }
    public void AddForce(VelocityInfo info)
    {
        additionalVelocities.Add(info);
    }
    private void Jump()
    {
        veloticy.y = Mathf.Sqrt(2 * gravity * jumpHeight);
    }
}
