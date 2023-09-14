using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Audio;

public class SoundController : MonoBehaviour
{
    [SerializeField] private AudioSource _audioBackground;
    [SerializeField] private AudioMixerGroup _audioMixer;


    private void Update()
    {
        OffAndOffMusicBackground();
        OffAndOffSoundBackground();
    }
    
    private void Start()
    {
        OffAndOffMusicBackground();
        OffAndOffSoundBackground();
    }

    public void OffAndOffMusicBackground()   //PlayerPrefs set in the UiLevelManager
    {
        if (PlayerPrefs.GetInt("Music") == 0)
        {
            _audioMixer.audioMixer.SetFloat("MusicValue", 0);
        }
        else
        {
            _audioMixer.audioMixer.SetFloat("MusicValue", -80);
        }
    }
    public void OffAndOffSoundBackground()   //PlayerPrefs set in the UiLevelManager
    {
        if (PlayerPrefs.GetInt("Sound") == 0)
        {
            _audioMixer.audioMixer.SetFloat("SoundValue", 0);
        }
        else
        {
            _audioMixer.audioMixer.SetFloat("SoundValue", -80);
        }
    }
}
