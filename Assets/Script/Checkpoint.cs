using UnityEngine;

public class Checkpoint : MonoBehaviour
{
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            PlayerMovement player = other.GetComponent<PlayerMovement>();

            if (player != null)
            {
                player.respawnPoint = other.transform.position;
                Debug.Log("Checkpoint Saved!");
            }
        }
    }
}
