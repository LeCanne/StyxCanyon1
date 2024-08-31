using System.Collections;
using System.Collections.Generic;
using UnityEditor.Rendering;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.Rendering;


public class HumanCamera : MonoBehaviour
{
   
    private InputAction camVec;
    public GameObject Player;
    public float Speed;
    public float TurnSpeed;
    private Vector2 m_rotation;

    float xRotation;
    public GameObject followTransform;
    private Vector2 variableCam;
    public Animator animatorPlayer;

    float x;
    float y;

    [Header("CameraSettings")]
    public float max;
    public float min;
    
     
    // Start is called before the first frame update
    private void Awake()
    {
        Cursor.lockState = CursorLockMode.Locked;
    }

    private void OnEnable()
    {
       
    }
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
       // transform.position = followTransform.transform.position;
        //transform.forward = Player.transform.forward;
        MouseRotate();
        Clamp();

    }

    public void CameraMovement(InputAction.CallbackContext valuecam)
    {
      x = valuecam.ReadValue<Vector2>().x;
       y = valuecam.ReadValue<Vector2>().y;


    }
    void MouseRotate()
    {
      
        

      
        variableCam.x = x * Speed * Time.deltaTime;
        variableCam.y = y * Speed * Time.deltaTime;

        xRotation -= variableCam.y;

        xRotation = Mathf.Clamp(xRotation, min, max);



        transform.localRotation = Quaternion.Euler(xRotation, 0f, 0f);
       
         

        Player.transform.Rotate(Vector3.up * variableCam.x);
           
          
            
            
        
    }

    void Clamp()
    {
       
    }
}
