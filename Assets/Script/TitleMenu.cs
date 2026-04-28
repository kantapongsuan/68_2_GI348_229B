using UnityEngine;
using UnityEngine.SceneManagement;

public class TitleMenu : MonoBehaviour
{
    public void PlayGame()
    {
        SceneManager.LoadScene("Game");
        Debug.Log("Clicked Play!");
    SceneManager.LoadScene("Game");
    }

    public void BackToMain()
    {
        SceneManager.LoadScene("MainMenu");
    }
}
