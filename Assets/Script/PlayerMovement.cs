using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    public CharacterController controller;
    public Vector3 respawnPoint;

    [Header("Movement")]
    public float speed = 5f;
    public float sprintSpeed = 9f;
    public float crouchSpeed = 2.5f;

    [Header("Jump & Gravity")]
    public float jumpHeight = 1.5f;
    public float gravity = -9.8f;
    public Transform groundCheck;
    public float groundDistance = 0.4f;
    public LayerMask groundMask;

    Vector3 velocity;
    bool isGrounded;

    [Header("Crouch")]
    public float normalHeight = 2f;
    public float crouchHeight = 1f;

    void Start()
    {
        controller = GetComponent<CharacterController>();
        respawnPoint = transform.position;
    }

    void Update()
    {
        isGrounded = controller.isGrounded;

        // กันไม่ให้สะสมแรงตอนอยู่พื้น
        if (isGrounded && velocity.y < 0)
            {
                velocity.y = -2f; // ค่านี้ช่วยให้ติดพื้นนิ่ง
            }

        // กระโดด
        if (Input.GetButtonDown("Jump") && isGrounded)
            {
                velocity.y = Mathf.Sqrt(jumpHeight * -2f * gravity);
            }

        // รับ input
        float x = Input.GetAxis("Horizontal");
        float z = Input.GetAxis("Vertical");

        // วิ่ง
        float currentSpeed = speed;
        if (Input.GetKey(KeyCode.LeftShift))
        {
            currentSpeed = sprintSpeed;
        }

        // ย่อ
        if (Input.GetKey(KeyCode.LeftControl))
        {
            controller.height = crouchHeight;
            currentSpeed = crouchSpeed;
        }
        else
        {
            controller.height = normalHeight;
        }

        // เดิน
        Vector3 move = transform.right * x + transform.forward * z;
        controller.Move(move * currentSpeed * Time.deltaTime);

        // กระโดด
        if (Input.GetButtonDown("Jump") && isGrounded)
        {
            velocity.y = Mathf.Sqrt(jumpHeight * -2f * gravity);
        }

        // gravity
        velocity.y += gravity * Time.deltaTime;
        controller.Move(velocity * Time.deltaTime);
        
        if (Input.GetKeyDown(KeyCode.R))
        {
            transform.position = respawnPoint;
        }
    }
}