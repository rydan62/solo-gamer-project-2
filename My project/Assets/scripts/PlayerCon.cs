using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.SceneManagement;

public class PlayerController : MonoBehaviour
{
    // R for hits and r for everything else
    Camera playerCam;
    Rigidbody rb;
    Ray jumpRay;
    Ray interactray;
    RaycastHit interactRay;
    GameObject pickupObj;
    float inputX;
    float inputY;

    public PlayerInput input;
    public Transform weaponSlot;
        public Weapon currentWeapon;

    public float speed = 5f;
    public float jumpHeight = 10f;
    public float jumpRayDistance = 1.1f;
    public float interactDistance = 1f;
    public int health = 200;
    public int maxHealth = 200;
    public bool attacking = false;

    private void Start()
    {
        jumpRay = new Ray(transform.position, -transform.up);
        rb = GetComponent<Rigidbody>();
        playerCam = Camera.main;

        Cursor.visible = false;
        Cursor.lockState = CursorLockMode.Locked;
        input = GetComponent<PlayerInput>();
        interactray = new Ray(transform.position, transform.forward);
        weaponSlot = playerCam.transform.GetChild(0);
    }
    private void Update()
    {
        if (health <= 0)
        {
            SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
        }
        interactray.origin = playerCam.transform.position;
        interactray.direction = playerCam.transform.forward;

        if (Physics.Raycast(interactray, out interactRay, interactDistance))
        {
            if (interactRay.collider.tag == "weapon")
            {
                pickupObj = interactRay.collider.gameObject;
            }
        }
        else
            pickupObj = null;

        if (currentWeapon)
            if (currentWeapon.holdToAttack && attacking)
                currentWeapon.fire();

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
    public void Attack(InputAction.CallbackContext context)
    {
        if (currentWeapon)
        {
            if (currentWeapon.holdToAttack)
            {
                if (context.ReadValueAsButton())
                    attacking = true;
                else
                    attacking = false;
            }
            else if (context.ReadValueAsButton())
                currentWeapon.fire();

        }
    }
    public void reload()
    {
        if (currentWeapon)
            currentWeapon.reload();
    }

    public void Interact()
    {
        if (pickupObj)
        {
            if (pickupObj.tag == "Weapon")
                if (currentWeapon)
                    pickupObj.GetComponent<Weapon>().equip(this);
        }
        pickupObj = null;
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