using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerController : MonoBehaviour
{
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
        
    }

    private void FixedUpdate()
    {
        PhysicsMove();
    }

    private void PhysicsMove()
    {
        rb.velocity = velocity * speed;
    }

    public void Move(InputAction.CallbackContext moveAxis)
    {
        float x;
        float y;

        x = moveAxis.ReadValue<Vector2>().x;
        y = moveAxis.ReadValue<Vector2>().y;

        velocity = new Vector3(x, 0, y);
        
    }
}
