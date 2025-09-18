using UnityEditor;
using UnityEngine;
using System.Collections.Generic;
using System.Collections
public class pPayerCon : MonoBehaviour
{
    [Header("movement")]
    public float moveSpeed;
    
    public Transform orientation;

    float horizontalInput;
    float verticalInput;

    Vector3 moveDirection;

    Rigidbody rb;
    private void Start()
    {
        rb = GetComponent<Rigidbody>();
        rb.freezeRotation = true;
    }
    void Update()
    {
        MyInput();
    }
    private void MyInput()
    {
        verticalInput = Input.GetAxisRaw("Vertical");
        horizontalInput = Input.GetAxisRaw("horizontal");
    }
    private void MovePlayer()
    {
        moveDirection = Orientation.forward * verticalInput + orientation.right * horizontalInput;
        rb.AddForce(moveDirection.normalized * moveSpeed * moveSpeed * 10f, ForceMode.Force);
    }
        
}
