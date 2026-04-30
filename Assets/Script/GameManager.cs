using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager instance;

    public Vector3 checkpointPosition = Vector3.zero;

    void Awake()
    {
        if (instance == null)
        {
            instance = this;
            DontDestroyOnLoad(gameObject); // 🔥 สำคัญมาก
        }
        else
        {
            Destroy(gameObject);
        }
    }

    public void ResetCheckpoint()
    {
        checkpointPosition = Vector3.zero;
    }
}
