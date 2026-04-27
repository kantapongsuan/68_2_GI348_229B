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

        if (distance <= range && CanSeePlayer())
        {
            RotateToPlayer();
            HandleShooting();
        }
    }

    void RotateToPlayer()
    {
        Vector3 dir = player.position - transform.position;
        dir.y = 0;

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
        GameObject bullet = Instantiate(bulletPrefab, firePoint.position, firePoint.rotation);

        Vector3 dir = player.position - firePoint.position;
        bullet.GetComponent<Bullet>().SetDirection(dir);
    }

    bool CanSeePlayer()
    {
        Vector3 dir = (player.position - firePoint.position).normalized;

        RaycastHit hit;
        if (Physics.Raycast(firePoint.position, dir, out hit, range))
        {
            if (hit.transform == player)
                return true;
        }

        return false;
    }
}