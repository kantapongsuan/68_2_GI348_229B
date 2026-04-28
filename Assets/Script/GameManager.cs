using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager instance;

    public Vector3 checkpointPosition;

    void Awake()
    {
        if (instance == null)
        {
            instance = this;
            DontDestroyOnLoad(gameObject); // 🔥 ไม่หายตอนเปลี่ยนฉาก
        }
        else
        {
            Destroy(gameObject);
        }
    }
}
