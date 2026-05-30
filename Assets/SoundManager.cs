using UnityEngine;

public class SoundManager : MonoBehaviour
{
    public static SoundManager Instance;

    [Header("Audio Sources")]
    [SerializeField] AudioSource musicSource;
    [SerializeField] AudioSource sfxSource;

    [Header("Volume Settings")]
    [Range(0, 1)][SerializeField] private float musicVolume = 1f;
    [Range(0, 1)][SerializeField] private float sfxVolume = 1f;

    [Header("Default Effects Settings")]
    [SerializeField] AudioClip musicClip;
    [SerializeField] AudioClip buttonClip;
    [SerializeField] AudioClip gameWin;
    [SerializeField] AudioClip gameOver;
    

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }

        musicSource.volume = musicVolume;
        sfxSource.volume = sfxVolume;
    }

    private void Start()
    {
        PlayMusic(musicClip);
    }

    public void PlayMusic(AudioClip clip, bool loop = true)
    {
        musicSource.clip = clip;
        musicSource.loop = loop;
        musicSource.volume = musicVolume;
        musicSource.Play();
    }

    public void ResetMusic()
    {
        musicVolume = 1;
        SetMusicVolume(1f); // apply full scale from the base
        musicSource.Play();

    }
   
    public void SetSoundVolume(float volume)
    {
        sfxSource.volume = volume * sfxVolume;
    }
    public void SetMusicVolume(float volume)
    {
        musicSource.volume = volume * musicVolume;
    }
    public void UpdateMusicVolume(float volume)
    {
        musicVolume = volume;
        SetMusicVolume(musicSource.volume);
        musicSource.Play();
    }
    public void StopMusic()
    {
        musicSource.Stop();
    }

    public void GameWin()
    {
        PlaySFX(gameWin);
    }

    public void GameOver()
    {
        PlaySFX(gameOver);

    }

    Timer clipCountDown;

    public void PlayButton()
    {
        PlaySFX(buttonClip, 0, 1 , 1);
    }

    public void PlaySFX(AudioClip clip,float increment = 0.05f, float minPitch = 0.95f, float maxPitch = 1.05f)
    {
        if (clip == sfxSource.clip)
        {
            sfxSource.pitch += increment;
            clipCountDown.Time = 0;
        }
        else
        {
            sfxSource.pitch = minPitch;

            if (clipCountDown != null)
            {
                clipCountDown.Kill();
                clipCountDown = null;
            }

            clipCountDown = new Timer(3, () => { sfxSource.pitch = minPitch; });
        }

        sfxSource.pitch = Mathf.Clamp(sfxSource.pitch, minPitch, maxPitch);

        sfxSource.clip = clip;
        sfxSource.volume = sfxVolume;

        sfxSource.Play();
    }
}
