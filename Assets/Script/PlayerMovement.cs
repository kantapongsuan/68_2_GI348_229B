using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    public CharacterController controller;
    public Vector3 respawnPoint;

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
        respawnPoint = transform.position;
    }

    void Update()
    {
        isGrounded = controller.isGrounded;

        // กันตัวลอย
        if (isGrounded && velocity.y < 0)
            velocity.y = -2f;

        // Movement
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

        // รีเอง
        if (Input.GetKeyDown(KeyCode.R))
        {
            Die();
        }
    }

    // 💀 ตอนตาย
    public void Die()
    {
        Debug.Log("Player Died!");

        Respawn();

        // 🔥 รีเซ็ตศัตรูทั้งหมด
        EnemyHealth[] enemies = FindObjectsOfType<EnemyHealth>();

        foreach (EnemyHealth enemy in enemies)
        {
            enemy.Respawn();
        }
    }

    // 🔁 วาร์ปกลับ
    void Respawn()
    {
        velocity = Vector3.zero;

        controller.enabled = false;
        transform.position = respawnPoint;
        controller.enabled = true;
    }
}