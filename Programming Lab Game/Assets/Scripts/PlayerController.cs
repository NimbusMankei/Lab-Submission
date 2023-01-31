using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerController : MonoBehaviour
{
    PlayerControls inputAction;
    Vector2 move;
    Vector2 rotate;
    Rigidbody rb;

    private float distanceToGround;
    bool isGrounded;
    public float jump = 5f;
    public float walkspeed = 5f;
    public Camera playerCamera;
    Vector3 cameraRotation;

    private Animator playerAnimator;
    private bool isWalking = false;

    private void Awake()
    {
        inputAction = new PlayerControls();

        inputAction.Character.Move.performed += cntxt => move = cntxt.ReadValue<Vector2>();
        inputAction.Character.Move.canceled += cntxt => move = Vector2.zero;

        inputAction.Character.Jump.performed += cntxt => Jump();

        inputAction.Character.Look.performed += cntxt => rotate = cntxt.ReadValue<Vector2>();
        inputAction.Character.Look.canceled += cntxt => rotate = Vector2.zero;

        //inputAction.Character.Sword.performed += cntxt => Sword();

        rb = GetComponent<Rigidbody>();
        playerAnimator = GetComponent<Animator>();

        cameraRotation = new Vector3(transform.rotation.x, transform.rotation.y, transform.rotation.z);
        Cursor.lockState = CursorLockMode.Locked;

    }

    private void OnEnable()
    {
        inputAction.Character.Enable();
    }

    //20 frames per second
    // 1/20 = 0.05
    //20 * 1 * 10 * 0.05 = 200
    //500 frames per second
    //1/500 = 0.002
    //500 *1 * 10 * 0.002 = 10
    private void Update()
    {
        cameraRotation = new Vector3(cameraRotation.x + rotate.y, cameraRotation.y + rotate.x, cameraRotation.z);
        
        transform.eulerAngles = new Vector3(transform.rotation.x, cameraRotation.y, transform.rotation.z);



        transform.Translate(Vector3.forward * move.y * Time.deltaTime * walkspeed, Space.Self);
        transform.Translate(Vector3.right * move.x * Time.deltaTime * walkspeed, Space.Self);
    }

    private void LateUpdate()
    {
       //playerCamera.transform.eulerAngles = new Vector3(cameraRotation.x, cameraRotation.y, cameraRotation.z);
       //either or works
       playerCamera.transform.rotation = Quaternion.Euler(cameraRotation);
    }

    private void OnDisable()
    {
        inputAction.Character.Disable();
    }

    private void Jump()
    {
        Debug.Log("Jump button is pressed");
    }

    private void Sword()
    {

    }
}
