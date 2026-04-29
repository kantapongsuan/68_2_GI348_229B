using UnityEngine;
using UnityEngine.SceneManagement;

public class PlayerMovement : MonoBehaviour
{
    public CharacterController controller;

    [Header("Sound")]
    public AudioClip gunClip;
    public AudioSource footstepSound;

    [Header("Gun")]
    public float fireRate = 2f;
    float nextFireTime = 0f;

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

        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;

        // 👣 ตั้งค่าเสียงเดิน
        if (footstepSound != null)
        {
            footstepSound.loop = true;     // 🔁 ลูป
            footstepSound.playOnAwake = false;
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

        // 🔫 ยิง
        if (Input.GetButtonDown("Fire1") && Time.time >= nextFireTime)
        {
            Shoot();
            nextFireTime = Time.time + 1f / fireRate;
        }

        // 👣 เสียงเดินแบบลูป
        HandleFootsteps(x, z);

        if (Input.GetKeyDown(KeyCode.R))
        {
            Die();
        }
    }

    void Shoot()
    {
        if (gunClip != null)
        {
            AudioSource.PlayClipAtPoint(gunClip, transform.position);
        }
    }

    void HandleFootsteps(float x, float z)
    {
        bool isMoving = Mathf.Abs(x) > 0.1f || Mathf.Abs(z) > 0.1f;

        if (isGrounded && isMoving)
        {
            // ถ้ายังไม่เล่น → เริ่มเล่น
            if (!footstepSound.isPlaying)
            {
                footstepSound.Play();
            }

            // ปรับความเร็วเสียงตอนวิ่ง
            footstepSound.pitch = Input.GetKey(KeyCode.LeftShift) ? 1.5f : 1f;
        }
        else
        {
            // หยุดเดิน → หยุดเสียงทันที
            if (footstepSound.isPlaying)
            {
                footstepSound.Stop();
            }
        }
    }

    public void Die()
    {
        Cursor.lockState = CursorLockMode.None;
        Cursor.visible = true;

        SceneManager.LoadScene("GameOver");
    }
}