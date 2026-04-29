using UnityEngine;

public class BGMManager : MonoBehaviour
{
    public static BGMManager instance;

    public AudioSource musicSource;

    void Awake()
    {
        // มีตัวเดียวในเกม
        if (instance == null)
        {
            instance = this;
            DontDestroyOnLoad(gameObject); // ไม่หายตอนเปลี่ยนฉาก
        }
        else
        {
            Destroy(gameObject);
        }
    }

    public void PlayMusic(AudioClip clip)
    {
        if (clip == null) return;

        // เปลี่ยนเพลงทันที
        musicSource.Stop();
        musicSource.clip = clip;
        musicSource.loop = true;
        musicSource.Play();
    }

    public void StopMusic()
    {
        musicSource.Stop();
    }
}
