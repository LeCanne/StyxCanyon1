using System;
using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerController : MonoBehaviour
{
    [Header("JumpChecks")]
    float playerHeight = 2;
    [SerializeField] private Transform groundedOrigin;
    [SerializeField] private float groundDistance;
    [SerializeField] private bool grounded;
    [SerializeField] private float jumpHeight;
    [SerializeField] Transform orientation;
    private bool jumping;
    [SerializeField] LayerMask jumpMask;
    [SerializeField] Transform groundCheck;
    [SerializeField] float airMultiplier = 0.4f;



    

    private Ray ray;
    private Vector3 moveDirection;
   
    private Rigidbody rb;

    private Vector3 velocity;
    public float speed;
    public float movementMultiplier;
    [Header ("Drag")]
    public float groundDrag = 6f;
    public float airDrag = 2f;
    [Header("SlopeHandling")]
    public float maxAngle;
    private Vector3 slopeDirection;
    RaycastHit slopeHit;
   
    private bool OnSlope()
    {
        if (Physics.Raycast(transform.position, Vector3.down, out slopeHit, playerHeight / 2 + 1f, jumpMask))
        {
            
            float angle = Vector3.Angle(slopeHit.normal, Vector3.up);
            if (angle < MathF.Abs(maxAngle))
            {
                if (slopeHit.normal != Vector3.up)
                {
                    Debug.Log("onSlope");
                    return true;
                    
                }
                else
                {
                    return false;
                }
            }
            else
            {
                Debug.Log(angle);
                return false;
            }
           
            
        }
        else
        {
            return false;
        }
    }
    
    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody>();
        
    }

    // Update is called once per frame
    void Update()
    {
        CameraValues();
        CheckGround();
        ControlDrag();
    }

    private void FixedUpdate()
    {
        PhysicsMove();
        PhysicsJump();
    }

    private void ControlDrag()
    {
        if(grounded)
        rb.drag = groundDrag;
        else
            rb.drag = airDrag;
    }
    private void PhysicsMove()
    {
        slopeDirection = Vector3.ProjectOnPlane(moveDirection,slopeHit.normal);
        moveDirection = orientation.forward * velocity.z + orientation.right * velocity.x;

        if (grounded && !OnSlope())
        {
            rb.AddForce(moveDirection * speed * movementMultiplier, ForceMode.Acceleration);
        }
        else if(grounded && OnSlope())
        {
            rb.AddForce(slopeDirection.normalized * speed * movementMultiplier, ForceMode.Acceleration);
        }
        else if(!grounded)
        {
            rb.AddForce(moveDirection * speed * movementMultiplier * airMultiplier, ForceMode.Acceleration);
        }
       
    }

    private void PhysicsJump()
    {
        if(jumping == true)
        {
            rb.velocity = new Vector3(rb.velocity.x, 0, rb.velocity.z);
            rb.AddForce(0, jumpHeight, 0, ForceMode.Impulse);
            jumping = false;
        }

        if(rb.velocity.y < 0 && !grounded)
        {
            rb.AddForce(Physics.gravity, ForceMode.Acceleration);
        }
        
        
    }

    public void Move(InputAction.CallbackContext moveAxis)
    {
        float x;
        float y;

        x = moveAxis.ReadValue<Vector2>().x;
        y = moveAxis.ReadValue<Vector2>().y;

        velocity = new Vector3(x, 0, y);
       

    }

    public void Jump(InputAction.CallbackContext jump)
    {

        if (jump.performed)
        {
            if(grounded == true)
            {
                jumping = true;
            }
            else
            {
                jumping = false;
            }
        }
    }

    public void CheckGround()
    {
        
        grounded = Physics.CheckSphere(groundCheck.position, groundDistance, jumpMask);
     

        slopeDirection = Vector3.ProjectOnPlane(moveDirection, slopeHit.normal);
    }

    public void CameraValues()
    {
      
       
    }
 

    public void OnDrawGizmos()
    {
      
    }
}
