using UnityEngine;
using UnityEngine.AI;

public class EnemyChase : MonoBehaviour
{
    public Transform player;
    public float chaseRange = 10f;
    public float killDistance = 1.5f;

    public LayerMask wallMask; // เอาไว้เช็คกำแพง

    NavMeshAgent agent;

    void Start()
    {
        agent = GetComponent<NavMeshAgent>();
    }

    void Update()
    {
        float distance = Vector3.Distance(transform.position, player.position);

        if (distance <= chaseRange && CanSeePlayer())
        {
            agent.SetDestination(player.position);

            if (distance <= killDistance)
            {
                player.GetComponent<PlayerMovement>().Die();
            }
        }
        else
        {
            agent.ResetPath();
        }
    }

    bool CanSeePlayer()
    {
        Vector3 dir = (player.position - transform.position).normalized;

        RaycastHit hit;
        if (Physics.Raycast(transform.position + Vector3.up, dir, out hit, chaseRange))
        {
            if (hit.transform == player)
                return true;
        }

        return false;
    }
}