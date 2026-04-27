using UnityEngine;

public class Bullet : MonoBehaviour
{
    public float speed = 20f;
    private Vector3 direction;

    public void SetDirection(Vector3 dir)
    {
        direction = dir.normalized;
    }

    void Start()
    {
        // กันกระสุนค้างในฉาก
        Destroy(gameObject, 5f);
    }

    void Update()
    {
        transform.position += direction * speed * Time.deltaTime;
    }

    private void OnTriggerEnter(Collider other)
    {
        // ❌ ไม่ชนตัวศัตรูที่ยิง
        if (other.CompareTag("Enemy"))
            return;

        // ✅ โดน Player
        if (other.CompareTag("Player"))
        {
            PlayerMovement player = other.GetComponent<PlayerMovement>();
            if (player != null)
            {
                player.Die();
            }
        }

        // 💥 ชนอะไรก็หาย (ยกเว้น Enemy)
        Destroy(gameObject);
    }
}