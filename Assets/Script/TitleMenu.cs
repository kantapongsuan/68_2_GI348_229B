using UnityEngine;
using UnityEngine.SceneManagement;

public class TitleMenu : MonoBehaviour
{
    public void PlayGame()
    {
        if (GameManager.instance != null)
        {
            GameManager.instance.ResetCheckpoint(); // 🔥 ล้างตอนเริ่มใหม่เท่านั้น
        }

        SceneManager.LoadScene("Game");
    }

    public void BackToMain()
    {
        SceneManager.LoadScene("MainMenu");
    }
}
