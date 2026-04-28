using UnityEngine;

public class Checkpoint : MonoBehaviour
{
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            GameManager.instance.checkpointPosition = other.transform.position;
            Debug.Log("Checkpoint Saved!");
        }
    }
}
