using UnityEngine;

public class EnemyShoot : MonoBehaviour
{
    public Transform player;
    public GameObject bulletPrefab;
    public Transform firePoint;

    public float range = 15f;
    public float fireRate = 1f;

    float nextFireTime = 0f;

    void Update()
    {
        float distance = Vector3.Distance(transform.position, player.position);

        // 👉 1. เช็คระยะก่อน
        if (distance <= range)
            {
                // 👉 2. หันไปหาผู้เล่น
                RotateToPlayer();

                // 👉 3. ยิงตามเวลา
                HandleShooting();
            }
    }

    void RotateToPlayer()
    {
        Vector3 direction = player.position - transform.position;
        direction.y = 0;

        if (direction != Vector3.zero)
        {
            Quaternion rot = Quaternion.LookRotation(direction);
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
        GameObject bullet = Instantiate(bulletPrefab, firePoint.position, Quaternion.identity);

        Vector3 dir = player.position - firePoint.position;
        bullet.GetComponent<Bullet>().SetDirection(dir);
    }
}