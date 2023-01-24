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
    public float b_MoveSpeed;
    public float moveSpeed;

    [Header("Jump")]
    public int b_jumpCount;
    public int jumpCount;
    public float b_JumpPower;
    public float jumpPower;
    [SerializeField]
    private bool jumpTick = true;
    private float coyoteTime = 0f;

    [Header("Grounded")]
    public LayerMask whatIsGround;
    public bool grounded;
    private bool tflop;

    public float groundDrag = 2;
    public float airDrag = 0.75f;

    private float accel;
    private float gaccel = 1;
    public float b_aaccel;
    public float aaccel;

    [Header("Dash Movement")]
    public float b_dashSpeed;
    public float dashSpeed;
    [SerializeField]
    private float dashBuff;
    private bool dashTick = true;
    private float dashEndTime;
    private float dashFreezeTime;
    public float b_dashCooldown;
    public float dashCooldown = 2f;
    public float dashDuration = 0.1f;




    public void Establish(float move, float jumpP, int jumpC, float AirAccel, float dCool, float dSpeed)
    {
        Master = GetComponent<PlayerMaster>();
        b_MoveSpeed = moveSpeed = move;
        b_aaccel = aaccel = AirAccel;
        b_JumpPower = jumpPower = jumpP;
        b_jumpCount = jumpCount = jumpC;
        b_dashCooldown =dashCooldown = dCool;
        b_dashSpeed = dashSpeed =dSpeed;
        rb = GetComponent<Rigidbody>();
        
    }

    //Input gather
    public void OnMove(InputAction.CallbackContext context)
    {
        vertMove = context.ReadValue<Vector2>();
    }
    public void OnJump(InputAction.CallbackContext context)
    {


        if (jumpCount > 0 && context.ReadValue<float>() > 0.5f&& jumpTick&&dashTick)
        {
            if (jumpCount == b_jumpCount)
            {
                if (grounded)
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
        if (grounded)
        {
        grounded = false;
        }

        rb.velocity -= new Vector3(0, rb.velocity.y, 0);
        rb.AddForce(new Vector3(0, jumpPower, 0), ForceMode.Impulse);
        StartCoroutine(jumpTimer());
        jumpCount -= decrement;
    }
    public void OnDash(InputAction.CallbackContext context)
    {


        if (Time.time > dashEndTime&& context.ReadValue<float>() > 0.5)
        {
            rb.velocity = new Vector3(0,0,0);
            if (vertMove.y != 0 || vertMove.x != 0)
            {
                Vector3 movementDir = (transform.forward *vertMove.y + transform.right * vertMove.x).normalized;
                rb.AddForce(movementDir * (dashSpeed+dashBuff), ForceMode.Impulse);
            }
            else
                rb.AddForce(transform.forward * (dashSpeed+dashBuff), ForceMode.Impulse);
            rb.drag = 0;
            dashEndTime = Time.time + dashCooldown;
            dashFreezeTime = Time.time + dashDuration;
            StartCoroutine(Master.cameraEffects.DashShake(dashDuration, 0.1f));
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
        if (dashTick)
        {
          BasicWalk();
        }

    }

    public void BasicWalk()
    {


        Vector3 movementFor = transform.forward * vertMove.y * moveSpeed * accel;
        Vector3 movementRig = transform.right * vertMove.x * moveSpeed * accel;
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

    private void resetVel()
    {

    }



    public void Detections()
    {

        Ray cast = new Ray(transform.position, Vector3.down);
        if (Physics.Raycast(cast, height * 0.5f + 0.4f, whatIsGround)&&jumpTick)
        {
            grounded = true;
            coyoteTime = Time.time + 0.16f;
        }
        else
        {
           
                
            if (coyoteTime < Time.time)
                grounded = false;


        }
        Master.grounded = grounded;
        Master.dashing = !dashTick;
    }

    public void PhysicsMod()
    {
        Ray cast = new Ray(transform.position, Vector3.down);
        RaycastHit hit;
        if (Physics.Raycast(cast, out hit, height * 0.5f + 0.2f, whatIsGround))
        {
            if (hit.normal != Vector3.up && !Physics.Raycast(cast, out hit, height * 0.5f + 0.1f, whatIsGround) && jumpTick)
            {
                transform.position -= new Vector3(0, 0.05f, 0);

            }

        }


        if (grounded)
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
        jumpTick =false;
        yield  return new WaitForSeconds(0.1f);
        jumpTick = true;
    }

    public void GetBuff(int[] statRef, float[] statChange)
    {
        for(int i = 0; i<statRef.Length; i++)
        {
            if (statRef.Length == statChange.Length)
            {
                switch (statRef[i])
                {
                    case 1:
                        moveSpeed += b_MoveSpeed * statChange[i];
                        if (moveSpeed > dashSpeed / 4)
                            dashSpeed = moveSpeed * 4;
                        break;
                    case 2:
                        aaccel += b_aaccel * statChange[i];
                        break;
                    case 3:
                        jumpPower += b_JumpPower * statChange[i];
                        break;
                    case 4:
                        b_jumpCount += (int)statChange[i];
                        jumpCount += (int)statChange[i];
                        break;
                    case 5:
                        dashCooldown = Mathf.Clamp(dashCooldown+ b_dashCooldown * statChange[i],0.2f,1);
                        break;
                    case 6:
                        dashBuff += b_dashSpeed * statChange[i];
                        break;
                    default:
                        Debug.LogError("Stat out of range");
                        break;

                }
            }
            else
                Debug.LogError("Buff arrays not equal");

        }

    }



}
