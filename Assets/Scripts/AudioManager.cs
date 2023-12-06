using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioManager : MonoBehaviour
{
    public static AudioManager Instance;

    public AudioSource musicSource;
    public AudioSource soundEffectSource;

    void Awake()
    {
        //Singleton
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }
    }

    public void PlayMusic(AudioClip musicClip)
    {
        if (musicSource.isPlaying)
        {
            musicSource.Stop();
        }

        musicSource.clip = musicClip;
        musicSource.Play();
    }

    public void PlaySoundEffect(AudioClip soundEffectClip)
    {
        soundEffectSource.PlayOneShot(soundEffectClip);
    }

    public void StopMusic()
    {
        musicSource.Stop();
    }
}
