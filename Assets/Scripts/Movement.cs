using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Movement : MonoBehaviour
{
    public float moveSpeed;
    public float groundDrag;
    public Transform orientation;
    float horizontalInput;
    float verticalInput;

    Vector3 moveDirection;
    private Rigidbody rb;
    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody>();
        rb.freezeRotation = true;
    }

    // Update is called once per frame
    void Update()
    {
        MyInput();
        rb.drag= groundDrag;
    }
    void FixedUpdate()
    {
        MovePlayer();
    }
    private void MyInput()
    {
         
         horizontalInput = Input.GetAxisRaw("Horizontal");
         verticalInput = Input.GetAxisRaw("Vertical");
    }
    private void MovePlayer()
        {
        moveDirection = orientation.forward * verticalInput + orientation.right * horizontalInput;

        rb.AddForce(moveDirection.normalized * moveSpeed * 10f, ForceMode.Force); 
     }
}
