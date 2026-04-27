using UnityEngine;

public class EnemyHealth : MonoBehaviour
{
    private Vector3 startPosition;
    private Quaternion startRotation;

    void Start()
    {
        startPosition = transform.position;
        startRotation = transform.rotation;
    }

    public void Die()
    {
        gameObject.SetActive(false);
    }

    public void Respawn()
    {
        transform.position = startPosition;
        transform.rotation = startRotation;
        gameObject.SetActive(true);
    }
}