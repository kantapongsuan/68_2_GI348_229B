using UnityEngine;

public class EnemyShoot : MonoBehaviour
{
    public Transform player;
    public GameObject bulletPrefab;
    public Transform firePoint;
    public AudioSource gunSound;

    public float range = 15f;
    public float fireRate = 1f;

    float nextFireTime = 0f;

    void Update()
    {
        // ❗ กัน error ถ้ายังไม่ได้ลากค่าใน Inspector
        if (player == null || firePoint == null || bulletPrefab == null)
            return;

        float distance = Vector3.Distance(transform.position, player.position);

        if (distance <= range && CanSeePlayer())
        {
            RotateToPlayer();
            HandleShooting();
        }
    }

    void RotateToPlayer()
    {
        Vector3 dir = player.position - transform.position;
        dir.y = 0f;

        if (dir != Vector3.zero)
        {
            Quaternion rot = Quaternion.LookRotation(dir);
            transform.rotation = Quaternion.Slerp(transform.rotation, rot, 5f * Time.deltaTime);
        }
    }

    void HandleShooting()
    {
        if (Time.time >= nextFireTime)
        {
            Shoot();
            nextFireTime = Time.time + 1f / fireRate;
        }
    }

    void Shoot()
    {
        // 🔊 กัน null (ถ้าไม่ได้ใส่เสียงจะไม่ error)
        if (gunSound != null)
            gunSound.Play();

        GameObject bullet = Instantiate(bulletPrefab, firePoint.position, firePoint.rotation);

        Vector3 dir = player.position - firePoint.position;

        // ❗ กัน error ถ้า Bullet script ไม่มี
        Bullet b = bullet.GetComponent<Bullet>();
        if (b != null)
        {
            b.SetDirection(dir);
        }
    }

    bool CanSeePlayer()
    {
        Vector3 dir = (player.position - firePoint.position).normalized;

        RaycastHit hit;
        if (Physics.Raycast(firePoint.position, dir, out hit, range))
        {
            if (hit.transform.CompareTag("Player"))
                return true;
        }

        return false;
    }
}