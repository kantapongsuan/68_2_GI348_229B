using UnityEngine;

public class PlayBGM : MonoBehaviour
{
    public AudioClip bgm;

    void Start()
    {
        if (BGMManager.instance != null && bgm != null)
        {
            BGMManager.instance.PlayMusic(bgm);
        }
    }
}
