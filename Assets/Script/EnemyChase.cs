using UnityEngine;

public class EnemyChase : MonoBehaviour
{
    public Transform player;
    public float speed = 3f;
    public float chaseDistance = 10f;
    public float attackDistance = 1.5f;
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            other.GetComponent<PlayerMovement>().Die();
        }
    }

    void Update()
    {
        float distance = Vector3.Distance(transform.position, player.position);

        // เห็นผู้เล่น → วิ่งเข้าไป
        if (distance < chaseDistance)
        {
            transform.LookAt(player);
            transform.position = Vector3.MoveTowards(
                transform.position,
                player.position,
                speed * Time.deltaTime
            );
        }

        // ถึงระยะโจมตี → ฆ่าเลย
        if (distance < attackDistance)
        {
            player.GetComponent<PlayerMovement>().Die();
        }
    }
}
