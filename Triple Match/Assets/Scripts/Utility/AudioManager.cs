using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioManager : MonoBehaviour
{
    public static AudioManager instance;
    public Sound[] musicSounds, sfxSounds;
    public AudioSource musicSource, sfxSource;

    private void Awake()
    {
        if (instance != null)
        {
            Destroy(this.gameObject);
            return;
        }
        instance = this;
        DontDestroyOnLoad(this.gameObject);
    }

    private void Start()
    {
        PlayMusic("Background");
    }

    public void PlayMusic(string name)
    {
        Sound s = Array.Find(musicSounds, x => x.name == name);
        if (s == null)
        {
            Debug.LogWarning("Music not found");
        }
        else
        {
            musicSource.clip = s.clip;
            musicSource.Play();
        }
    }

    public void PlaySFX(string name)
    {
        Sound s = Array.Find(sfxSounds, x => x.name == name);
        if (s == null)
        {
            Debug.LogWarning("Sound not found");
        }
        else
        {
            sfxSource.PlayOneShot(s.clip);
        }
    }

    public void PlaySFX(string name, float pitch)
    {
        Sound s = Array.Find(sfxSounds, x => x.name == name);
        if (s == null)
        {
            Debug.LogWarning("Sound not found");
        }
        else
        {
            sfxSource.pitch = pitch;
            sfxSource.PlayOneShot(s.clip);
        }
    }

    public void ToggleSFX()
    {
        sfxSource.mute = !sfxSource.mute;
    }

    public void ToggleMusic()
    {
        musicSource.mute = !musicSource.mute;
    }

    public void MusicVolume(float volume)
    {
        if(volume == 0){
            musicSource.mute = true;
        }
        else musicSource.mute = false;
        musicSource.volume = volume;
    }

    public void SFXVolume(float volume)
    {
        if(volume == 0){
            sfxSource.mute = true;
        }
        else sfxSource.mute = false;
        sfxSource.volume = volume;
    }

    public bool sfxOn()
    {
        if (sfxSource.mute)
        {
            return false;
        }
        else return true;
    }

    public bool musicOn()
    {
        if (musicSource.mute)
        {
            return false;
        }
        else return true;
    }

    public void StopMusic()
    {
        musicSource.Stop();
    }

}
