using UnityEngine;

[RequireComponent(typeof(CharacterController))]
public class PlayerController : MonoBehaviour
{
    public float walkSpeed = 5f;
    public float jumpSpeed = 8f;
    public float gravity = 20f;
    public float mouseSensitivity = 2f;

    public GameObject projectilePrefab;
    public Transform firePoint;
    public float fireForce = 20f;
    public float reloadTime = 2f;
    public AudioSource src;

    private Vector3 moveDirection = Vector3.zero;
    private CharacterController controller;
    private float rotationX = 0f;
    private float reload;
    private bool canShoot = true;

    void Start()
    {
        controller = GetComponent<CharacterController>();
        Cursor.lockState = CursorLockMode.Locked; // Fareyi kilitle
        Cursor.visible = false;

        reload = Time.time + reloadTime;

        if ((GameObject)projectilePrefab == null )
        {
            Debug.LogError("Prefab is missing.");
        }

        if (fireForce <= 0)
        {
            Debug.LogError("Too small value: " + fireForce);
        }

        if (firePoint == null)
        { 
        Debug.LogError("Fire point is missing.");
        }

    }

    void Update()
    {
        // Fare ile bakma
        float mouseX = Input.GetAxis("Mouse X") * mouseSensitivity;
        float mouseY = Input.GetAxis("Mouse Y") * mouseSensitivity;

        rotationX -= mouseY;
        rotationX = Mathf.Clamp(rotationX, -90f, 90f);

        Camera.main.transform.localRotation = Quaternion.Euler(rotationX, 0f, 0f);
        transform.Rotate(0f, mouseX, 0f);

        // Yürüme & zıplama
        if (controller.isGrounded)
        {
            float moveX = Input.GetAxis("Horizontal") * walkSpeed;
            float moveZ = Input.GetAxis("Vertical") * walkSpeed;

            moveDirection = transform.TransformDirection(new Vector3(moveX, 0f, moveZ));

            if (Input.GetButton("Jump"))
            {
                moveDirection.y = jumpSpeed;
            }

            if (Input.GetMouseButtonDown(0) && canShoot)
            {
                GameObject proj = (GameObject)Instantiate(projectilePrefab, firePoint.position, firePoint.rotation);
                src.Play();
                reload = Time.time + reloadTime;
                canShoot = false;
            }
            if (!canShoot && Time.time >= reload)
            {
                canShoot = true;
            }
        }

        // Yerçekimi
        moveDirection.y -= gravity * Time.deltaTime;

        // Hareketi uygula
        controller.Move(moveDirection * Time.deltaTime);
    }
}
