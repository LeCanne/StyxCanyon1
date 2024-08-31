using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerController : MonoBehaviour
{
    [SerializeField]private float mouseSensitivity;
    private float xRotation;
    private Vector3 moveDirection;
    private Rigidbody rb;
    private GameObject camObject;
    private Vector3 velocity;
    public float speed;
    
    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody>();
        camObject = Camera.main.gameObject;
    }

    // Update is called once per frame
    void Update()
    {
        CameraValues();
    }

    private void FixedUpdate()
    {
        PhysicsMove();
    }

    private void PhysicsMove()
    {
        moveDirection = transform.forward * velocity.z + transform.right * velocity.x;
        rb.velocity = moveDirection * speed;
    }

    public void Move(InputAction.CallbackContext moveAxis)
    {
        float x;
        float y;

        x = moveAxis.ReadValue<Vector2>().x;
        y = moveAxis.ReadValue<Vector2>().y;

        velocity = new Vector3(x, 0, y);
       
    }

    public void CameraValues()
    {
      
       
    }
    public void MoveCam(InputAction.CallbackContext axisCam)
    {
      
       



    }
}
