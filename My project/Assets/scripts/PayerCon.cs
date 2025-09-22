using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.SceneManagement;

public class PlayerController : MonoBehaviour
{
    Camera playerCam;
    Rigidbody rb;
    Ray jumpRay;

    float inputX;
    float inputY;

    public float speed = 5f;
    public float jumpHeight = 10f;
    public float jumpRayDistance = 1.1f;

    public int health = 200;
    public int maxHealth = 200;


    private void Start()
    {
        jumpRay = new Ray(transform.position, -transform.up);
        rb = GetComponent<Rigidbody>();
        playerCam = Camera.main;

        Cursor.visible = false;
        Cursor.lockState = CursorLockMode.Locked;
    }
    private void Update()
    {
        if (health <= 0)
        {
            SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
        }

        
        Quaternion playerRotation = Quaternion.identity;
        playerRotation.y = playerCam.transform.rotation.y;
        playerRotation.w = playerCam.transform.rotation.w;
        transform.rotation = playerRotation;

        jumpRay.origin = transform.position;
        jumpRay.direction = -transform.up;

       
        Vector3 tempMove = rb.linearVelocity;

        tempMove.x = inputY * speed;
        tempMove.z = inputX * speed;

        rb.linearVelocity = (tempMove.x * transform.forward) +
                            (tempMove.y * transform.up) +
                            (tempMove.z * transform.right);
    }

    public void Move(InputAction.CallbackContext context)
    {
        Vector2 InputAxis = context.ReadValue<Vector2>();

        inputX = InputAxis.x;
        inputY = InputAxis.y;
    }
    public void Jump()
    {
        if (Physics.Raycast(jumpRay, jumpRayDistance))
            rb.AddForce(transform.up * jumpHeight, ForceMode.Impulse);
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "trap")
        {
            health = 20 - health;
        }
    }
    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.tag == "enemy")
        {
            health = 20 - health;
        }
    }
    

}