using UnityEngine;
using UnityEngine.SceneManagement;

public class PlayerMovement : MonoBehaviour
{
    public CharacterController controller;

    [Header("Movement")]
    public float speed = 5f;
    public float sprintSpeed = 9f;

    [Header("Jump & Gravity")]
    public float jumpHeight = 1.5f;
    public float gravity = -9.8f;

    Vector3 velocity;
    bool isGrounded;

    void Start()
    {
        controller = GetComponent<CharacterController>();

        // 🔒 ล็อคเมาส์ตอนเข้าเกม
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;

        // 🔁 วาร์ปไป Checkpoint ถ้ามี
        if (GameManager.instance != null && GameManager.instance.checkpointPosition != Vector3.zero)
        {
            controller.enabled = false;
            transform.position = GameManager.instance.checkpointPosition;
            controller.enabled = true;
        }
    }

    void Update()
    {
        isGrounded = controller.isGrounded;

        if (isGrounded && velocity.y < 0)
            velocity.y = -2f;

        float x = Input.GetAxis("Horizontal");
        float z = Input.GetAxis("Vertical");

        float currentSpeed = Input.GetKey(KeyCode.LeftShift) ? sprintSpeed : speed;

        Vector3 move = transform.right * x + transform.forward * z;
        controller.Move(move * currentSpeed * Time.deltaTime);

        // Jump
        if (Input.GetButtonDown("Jump") && isGrounded)
        {
            velocity.y = Mathf.Sqrt(jumpHeight * -2f * gravity);
        }

        // Gravity
        velocity.y += gravity * Time.deltaTime;
        controller.Move(velocity * Time.deltaTime);

        // กด R = ตาย (เอาไว้เทส)
        if (Input.GetKeyDown(KeyCode.R))
        {
            Die();
        }
    }

    public void Die()
    {
        Debug.Log("Player Died!");

        // 🔓 ปลดล็อคเมาส์
        Cursor.lockState = CursorLockMode.None;
        Cursor.visible = true;

        SceneManager.LoadScene("GameOver");
    }
}