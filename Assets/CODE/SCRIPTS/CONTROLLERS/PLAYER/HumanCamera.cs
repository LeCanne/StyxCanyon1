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
    [SerializeField] Transform orientation;

    float xRotation;
    float yRotation;

    public float sensX;
    public float sensY;
    public GameObject CameraParent;
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
        Cursor.visible = false;
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
       
        MouseRotate();
       

    }

    public void CameraMovement(InputAction.CallbackContext valuecam)
    {
      x = valuecam.ReadValue<Vector2>().x;
       y = valuecam.ReadValue<Vector2>().y;

        
    }
    void MouseRotate()
    {
        yRotation += x * sensX * Time.deltaTime;
        xRotation -= y * sensY * Time.deltaTime;
        xRotation = Mathf.Clamp(xRotation, min, max);



        CameraParent.transform.localRotation = Quaternion.Euler(xRotation, yRotation, 0f);
       orientation.transform.rotation = Quaternion.Euler(0,yRotation,0);
           
          
            
            
        
    }

  
}
