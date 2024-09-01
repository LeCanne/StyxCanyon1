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


    [SerializeField]private float mouseSensitivity;

    private Ray ray;
    private Vector3 moveDirection;
    private Rigidbody rb;

    private Vector3 velocity;
    public float speed;
    
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
        rb.MovePosition(transform.position + moveDirection * speed * Time.deltaTime);
    }

    private void PhysicsJump()
    {
        if(jumping == true)
        {
            rb.AddForce(0, jumpHeight, 0, ForceMode.Impulse);
            jumping = false;
        }

        if(rb.velocity.y < 0)
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
        RaycastHit hit;
        Debug.DrawRay(groundedOrigin.transform.position, -transform.up * distanceRaycast, Color.yellow);
        if (Physics.Raycast(groundedOrigin.transform.position, -transform.up, out hit, distanceRaycast, jumpMask))
        {
            Debug.Log(grounded);
            grounded = true;
        }
        else
        {
            grounded = false;
        }
    }

    public void CameraValues()
    {
      
       
    }
    public void MoveCam(InputAction.CallbackContext axisCam)
    {
      
       



    }

    public void OnDrawGizmos()
    {
      
    }
}
