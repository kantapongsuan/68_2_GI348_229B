using UnityEngine;
using UnityEngine.SceneManagement;

public class WinUI : MonoBehaviour
{
    void Start()
    {
        // 🔓 ให้เมาส์ใช้ได้
        Cursor.lockState = CursorLockMode.None;
        Cursor.visible = true;
    }

    public void PlayAgain()
    {
        if (GameManager.instance != null)
        {
            GameManager.instance.ResetCheckpoint();
        }

        SceneManager.LoadScene("Game");
    }

    public void BackToMenu()
    {
        SceneManager.LoadScene("MainMenu");
    }
}
