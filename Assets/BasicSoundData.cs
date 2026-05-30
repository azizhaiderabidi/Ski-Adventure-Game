using UnityEngine;

public class BasicSoundData : MonoBehaviour
{
    public AudioClip Btn;
    public AudioClip music;

    public void Start()
    {
        SoundManager.Instance.PlayMusic(music);
    }
}