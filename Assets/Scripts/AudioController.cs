using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioController : MonoBehaviour
{
    public AudioClip backgroundMusic;
    public AudioClip dialogSound;
    public AudioClip coinSound;
    public AudioClip openCloseSound;
    public AudioClip equipSound;

    void Start()
    {
        AudioManager.Instance.PlayMusic(backgroundMusic);
    }

    public void PlayButtonClickSound()
    {
        AudioManager.Instance.PlaySoundEffect(dialogSound);
    }

    public void PlayCoinSound()
    {
        AudioManager.Instance.PlaySoundEffect(coinSound);
    }

    public void PlayOpenCloseSound()
    {
        AudioManager.Instance.PlaySoundEffect(openCloseSound);
    }

    public void PlayEquipSound()
    {
        AudioManager.Instance.PlaySoundEffect(equipSound);
    }
}
