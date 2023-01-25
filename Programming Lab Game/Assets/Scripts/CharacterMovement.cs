using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterMovement : MonoBehaviour
{
    //How fast the Player Moves
    public float moveSpeed = 10f;
    //How fast the Player can rotate the camera
    public float rotateSpeed = 75f;
    public float jumpVelocity = 5f;
    public float distanceToGround = 0.1f;
    public LayerMask groundLayer;



    //Will store the vertical axis input
    private float vInput;
    //Wil store the horizontal axis input
    private float hInput;
    //Contains the capsule's Rigidbody componment info
    private Rigidbody _rb;
    private CapsuleCollider _col;


    void Start()
    {
        _rb = GetComponent<Rigidbody>();
        _col = GetComponent<CapsuleCollider>();
    }

    // Update is called once per frame
    void Update()
    {
        //To detect when the up arrow, down arrow, W or S keys are pressed
        //The up arrow and W keys return a vaule of 1, which will move the player in foward (positive) direction
        //The down arrow and S keys return -1, which moves the player backwards in the neagative direction
        vInput = Input.GetAxis("Vertical") * moveSpeed;
        //To detect when the left arrow, right arrow, A or D keys are pressed
        //The right arrow and D keys return a vaule of 1, which will rotate the player to the right
        //The left arrow and A keys return -1, which rotates the player to the left
        hInput = Input.GetAxis("Horizontal") * rotateSpeed;


        //public GameObject player;
        //Provides direction and speed the capsule needs to move foward or back, along the z axis at the speed we've calculated
        //this.transform.Translate(Vector3.forward * vInput * Time.deltaTime);
        //this.transform.Rotate(Vector3.up * hInput * Time.deltaTime);
    }

    void FixedUpdate()
    {
        if (IsGrounded() && Input.GetKeyDown(KeyCode.Space))
        {
            _rb.AddForce(Vector3.up * jumpVelocity, ForceMode.Impulse);
        }

        Vector3 rotation = Vector3.up * hInput;

        Quaternion angleRot = Quaternion.Euler(rotation * Time.fixedDeltaTime);

        _rb.MovePosition(this.transform.position + this.transform.forward * vInput * Time.fixedDeltaTime);
        _rb.MoveRotation(_rb.rotation * angleRot);

       
    }

    //utility function
    private bool IsGrounded()
    {
        Vector3 capsuleBottom = new Vector3(_col.bounds.center.x, _col.bounds.min.y, _col.bounds.center.z);

        bool grounded = Physics.CheckCapsule(_col.bounds.center, capsuleBottom, distanceToGround, groundLayer, QueryTriggerInteraction.Ignore);

        return grounded;
    }
}
