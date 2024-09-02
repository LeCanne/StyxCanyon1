using System;
using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerController : MonoBehaviour
{
    [Header("JumpChecks")]
    [SerializeField] private Transform groundedOrigin;
    [SerializeField] private float distanceRaycast;
    [SerializeField] private bool grounded;
    [SerializeField] private float jumpHeight;
    private bool jumping;
    [SerializeField] LayerMask jumpMask;



    

    private Ray ray;
    private Vector3 moveDirection;
   
    private Rigidbody rb;

    private Vector3 velocity;
    public float speed;
    [Header("SlopeHandling")]
    public float maxAngle;
    private Vector3 slopeDirection;
    RaycastHit slopeHit;
    private float distanceSlopRay = 1f;
    private bool OnSlope()
    {
        if (Physics.Raycast(groundedOrigin.transform.position, Vector3.down, out slopeHit, distanceSlopRay, jumpMask))
        {
            float angle = Vector3.Angle(slopeHit.normal, Vector3.up);
            if (angle < MathF.Abs(maxAngle))
            {
                if (slopeHit.normal != Vector3.up)
                {
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
        
    }

    private void FixedUpdate()
    {
        PhysicsMove();
        PhysicsJump();
    }

    private void PhysicsMove()
    {
        moveDirection = transform.forward * velocity.z + transform.right * velocity.x;

        if (!OnSlope())
        {
            rb.MovePosition(transform.position + moveDirection * speed * Time.deltaTime);
        }
        else if(grounded && OnSlope())
        {
            rb.MovePosition(transform.position + slopeDirection * speed * Time.deltaTime);
        }
       
    }

    private void PhysicsJump()
    {
        if(jumping == true)
        {
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
        
        grounded = Physics.CheckSphere(transform.position - new Vector3(0,1,0), distanceRaycast, jumpMask);
     

        slopeDirection = Vector3.ProjectOnPlane(moveDirection, slopeHit.normal);
    }

    public void CameraValues()
    {
      
       
    }
 

    public void OnDrawGizmos()
    {
      
    }
}
