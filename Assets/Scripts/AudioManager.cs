using UnityEngine;
using UnityEngine.InputSystem;

public class AudioManager : MonoBehaviour
{
    public static AudioManager Instance;

    public AudioSource musicSource;
    public AudioSource sfxSource;

    void Awake()
    {
        if (Instance != null && Instance != this)
        {
            Destroy(gameObject);
            return;
        }

        Instance = this;
        DontDestroyOnLoad(gameObject);
        musicSource.volume = PlayerPrefs.GetFloat("MusicVolume", musicSource.volume);
        musicSource.Play();
    }

    void Update()
    {
        Keyboard kb = Keyboard.current;
        if (kb == null) return;

        if (kb.equalsKey.wasPressedThisFrame || kb.numpadPlusKey.wasPressedThisFrame)
            ChangeMusicVolume(0.1f);

        if (kb.minusKey.wasPressedThisFrame || kb.numpadMinusKey.wasPressedThisFrame)
            ChangeMusicVolume(-0.1f);
    }

    public void PlaySfx(AudioClip clip)
    {
        sfxSource.PlayOneShot(clip);
    }

    public void ChangeMusicVolume(float amount)
    {
        musicSource.volume = Mathf.Clamp01(musicSource.volume + amount);
        PlayerPrefs.SetFloat("MusicVolume", musicSource.volume);
    }
}