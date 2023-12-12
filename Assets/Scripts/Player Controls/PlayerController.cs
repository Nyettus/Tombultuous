using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerController : MonoBehaviour
{
    public PlayerMaster Master;

    [Header("General Movement")]
    public float height = 2;
    private Vector2 vertMove;
    public Rigidbody rb;
    public float moveSpeed => Mathf.Clamp(Master.moveSpeed + Master.itemMaster.M_MoveSpeed, Master.itemMaster.MIN_MoveSpeed, Mathf.Infinity);


    [Header("Jump")]
    public int jumpCount;
    public int b_jumpCount => Mathf.Clamp(Master.jumpCount + Master.itemMaster.M_JumpCount, Master.itemMaster.MIN_JumpCount, int.MaxValue);



    public float jumpPower => Mathf.Clamp(Master.jumpPower + Master.itemMaster.M_JumpPower, Master.itemMaster.MIN_JumpPower, Mathf.Infinity);


    private bool jumpTick = true;
    [SerializeField]
    private float coyoteTime = 0f;

    [Header("Grounded")]
    public LayerMask whatIsGround;
    public bool grounded;
    private Vector3 groundNormal;
    private bool tflop;

    public float groundDrag = 2;
    public float airDrag = 0.75f;

    private float accel;
    private float gaccel = 1;

    public float aaccel => Mathf.Clamp(Master.airAccel + Master.itemMaster.M_AirAcceleration, Master.itemMaster.MIN_AirAcceleration, Mathf.Infinity);

    public float dashSpeed => Master.dashSpeed;

    [SerializeField]
    private float dashBuff => Master.itemMaster.M_DashSpeed;

    private bool dashTick = true;
    private float dashEndTime;
    private float dashFreezeTime;
    public float dashCooldown => Mathf.Clamp(Master.dashCooldown + Master.itemMaster.M_DashCooldown, Master.itemMaster.MIN_DashCooldown, Mathf.Infinity);

    public float dashDuration = 0.1f;




    public void Establish()
    {
        Master = GetComponent<PlayerMaster>();
        rb = GetComponent<Rigidbody>();

    }

    //Input gather
    public void OnMove(InputAction.CallbackContext context)
    {
        vertMove = context.ReadValue<Vector2>();
    }
    public void OnJump(InputAction.CallbackContext context)
    {

        
        if (jumpCount > 0 && context.ReadValue<float>() > 0.5f && jumpTick && dashTick)
        {
            if (jumpCount == b_jumpCount)
            {
                if (Master.grounded)
                {
                    jumpCompress();
                }
                else if (b_jumpCount > 1)
                {
                    jumpCompress(2);
                }
            }
            else
            {
                jumpCompress();
            }
        }
    }

    private void jumpCompress(int decrement = 1)
    {
        if (Master.grounded)
        {
            Master.grounded = false;
        }

        rb.velocity -= new Vector3(0, rb.velocity.y, 0);
        rb.AddForce(new Vector3(0, jumpPower, 0), ForceMode.Impulse);
        StartCoroutine(jumpTimer());
        jumpCount -= decrement;
    }
    public void OnDash(InputAction.CallbackContext context)
    {
        float localDashSpeed;
        if (dashSpeed > moveSpeed * 4)
            localDashSpeed = dashSpeed;
        else
            localDashSpeed = moveSpeed * 4;
        if (Time.time > dashEndTime && context.ReadValue<float>() > 0.5)
        {
            rb.velocity = new Vector3(0, 0, 0);
            if (vertMove.y != 0 || vertMove.x != 0)
            {
                Vector3 movementDir = (transform.forward * vertMove.y + transform.right * vertMove.x).normalized;
                rb.AddForce(movementDir * (localDashSpeed + dashBuff), ForceMode.Impulse);
            }
            else
                rb.AddForce(transform.forward * (localDashSpeed + dashBuff), ForceMode.Impulse);
            rb.drag = 0;
            dashEndTime = Time.time + dashCooldown;
            dashFreezeTime = Time.time + dashDuration;
            GameManager._.Master.cameraEffects.DashShake(2f, dashDuration);
            dashTick = false;

        }



    }


    void Update()
    {
        DashDisable();
        Detections();
        if (dashTick)
        {
            PhysicsMod();
        }


    }



    private void FixedUpdate()
    {
        MatchRotation();
        if (dashTick)
        {
            BasicWalk();
        }

    }

    public void BasicWalk()
    {


        Vector3 movementFor = Vector3.ProjectOnPlane(transform.forward * vertMove.y * moveSpeed * accel, groundNormal);
        Vector3 movementRig = Vector3.ProjectOnPlane(transform.right * vertMove.x * moveSpeed * accel, groundNormal);
        Debug.Log(rb.velocity.magnitude);

        float forwardVel = Vector3.Dot(rb.velocity, transform.forward);
        float rightVel = Vector3.Dot(rb.velocity, transform.right);

        if (vertMove.y > 0 && forwardVel < moveSpeed * Mathf.Abs(vertMove.y) || vertMove.y < 0 && forwardVel > -moveSpeed * Mathf.Abs(vertMove.y))
        {
            rb.AddForce(movementFor, ForceMode.Force);
        }
        if (vertMove.x > 0 && rightVel < moveSpeed * Mathf.Abs(vertMove.x) || vertMove.x < 0 && rightVel > -moveSpeed * Mathf.Abs(vertMove.x))
        {
            rb.AddForce(movementRig, ForceMode.Force);
        }

    }

    public Transform WeaponHolder;
    private void MatchRotation()
    {
        WeaponHolder.transform.localRotation = Quaternion.Euler(new Vector3(Camera.main.transform.eulerAngles.x,0,0));
        rb.rotation = Quaternion.Euler(new Vector3(0,Camera.main.transform.eulerAngles.y,0));
    }

    public void DashDisable()
    {
        if (Time.time > dashFreezeTime)
        {
            if (dashTick == false)
            {
                Vector3 flatvel = new Vector3(rb.velocity.x, 0f, rb.velocity.z);
                if (flatvel.magnitude > moveSpeed)
                {
                    Vector3 limitedVel = flatvel.normalized * moveSpeed;
                    rb.velocity = new Vector3(limitedVel.x, rb.velocity.y, limitedVel.z);
                }
            }
            dashTick = true;

        }
    }



    public void Detections()
    {

        Ray cast = new Ray(transform.position, Vector3.down);
        RaycastHit hit;
        if (Physics.Raycast(cast,out hit, height * 0.5f + 0.4f, whatIsGround) && jumpTick)
        {
            groundNormal = hit.normal;
            Master.grounded = true;
            coyoteTime = Time.time + 0.16f;
        }
        else
        {


            if (coyoteTime < Time.time)
                Master.grounded = false;


        }
        Master.dashing = !dashTick;
    }

    public void PhysicsMod()
    {
        if (Master.grounded)
        {
            if (tflop)
            {
                jumpCount = b_jumpCount;
                tflop = false;
            }


            rb.drag = groundDrag;
            accel = moveSpeed * gaccel;

            if (vertMove == Vector2.zero)
                rb.useGravity = false;
            else
                rb.useGravity = true;
        }
        else
        {
            rb.drag = airDrag;
            accel = moveSpeed * aaccel;
            rb.useGravity = true;
            tflop = true;
        }
    }

    IEnumerator jumpTimer()
    {
        jumpTick = false;
        yield return new WaitForSeconds(0.1f);
        jumpTick = true;
    }


    public void KnockBack(Vector3 direction, float amount)
    {
        rb.velocity = Vector3.zero;
        Vector3 forceDir = direction * amount;
        rb.AddForce(forceDir, ForceMode.Impulse);

    }


}
