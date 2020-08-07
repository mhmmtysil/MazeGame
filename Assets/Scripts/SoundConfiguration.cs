using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundConfiguration : MonoBehaviour
{
    public static SoundConfiguration Instance { get; private set; }

    public AudioSource mainSource;
    public AudioSource subSource;

    private bool musinOn = true;
    private bool soundOn = true;
    public bool MusicOn { get => musinOn; set => SetMusicStatus(value); }
    public bool SoundOn { get => soundOn; set => SetSoundStatus(value); }
    void Awake()
    {
      
        if(Instance != null)
        {
            Destroy(gameObject);
            return;
        }
        DontDestroyOnLoad(gameObject);
        Instance = this;
    }
    private void SetMusicStatus(bool b)
    {
        if (b)
        {
            mainSource.volume = 1;
        }
        else
        {
            mainSource.volume = 0;
        }
        musinOn = b;
    }

    private void SetSoundStatus(bool b)
    {
        if (b)
        {
            subSource.volume = 1;
        }
        else
        {
            subSource.volume = 0;
        }
        soundOn = b;
    }

    public void PlayClickSound() => subSource.PlayOneShot(Resources.Load<AudioClip>("Sounds/buttonSound"));
    public void PlaySetMainSourceClipInGame()
    {
        mainSource.clip = Resources.Load<AudioClip>("Sounds/ingame");
        mainSource.Play();
    }
    public void PlaySetMainSourceClipMenu() {
        mainSource.clip = Resources.Load<AudioClip>("Sounds/mazEscape");
        mainSource.Play();
    }
    
    public void PlayCollectSound() => subSource.PlayOneShot (Resources.Load<AudioClip>("Sounds/LetterCollectSound"));

    public void PlaySpeedSound() => subSource.PlayOneShot(Resources.Load<AudioClip>("Sounds/speedUp"));
    public void PlayEndStageSound() => subSource.PlayOneShot(Resources.Load<AudioClip>("Sounds/stagefinished"));
    public void PlayGameOverSound() => subSource.PlayOneShot(Resources.Load<AudioClip>("Sounds/levelFailed"));
}
