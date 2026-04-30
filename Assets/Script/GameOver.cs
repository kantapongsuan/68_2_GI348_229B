using UnityEngine;
using UnityEngine.SceneManagement;

public class GameOverUI : MonoBehaviour
{
    // 🔁 เล่นต่อจาก checkpoint
    public void Retry()
    {
        SceneManager.LoadScene("Game"); // ❌ ไม่ต้อง reset
    }

    // 🔙 กลับเมนู
    public void BackToMenu()
    {
        SceneManager.LoadScene("MainMenu");
    }
}
