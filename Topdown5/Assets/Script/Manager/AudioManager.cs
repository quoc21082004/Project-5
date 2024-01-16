using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
public class AudioManager : Singleton<AudioManager>
{
    public Sound[] musicSound, sfxSound;
    public AudioSource musicSource, sfxSource;
    public AudioSource PlayMusicBGM(string name)
    {
        Sound sound = Array.Find(musicSound, s => s.name == name);
        if (sound == null)
            return null;
        AudioSource sourceClone = GetComponent<AudioSource>();
        sourceClone.clip = sound.clip;
        sourceClone.Play();
        musicSource = sourceClone;
        return sourceClone;        
    }
    public AudioSource PlaySfx(string name)
    {
        Sound sound = Array.Find(sfxSound, s => s.name == name);
        if (sound == null)
            return null;
        AudioSource sourceClone = GetComponent<AudioSource>();
        sourceClone.clip = sound.clip;
        sourceClone.PlayOneShot(sound.clip);
        sfxSource = sourceClone;
        return sourceClone;
    }
    public void ToggleMusic()
    {
        musicSource.mute = !musicSource.mute;
    }
    public void ToggleSfx()
    {
        sfxSource.mute = !sfxSource.mute;
    }
    public void MusicVolume(float volume)
    {
        musicSource.volume = volume;
    }
    public void SfxVolume(float volume)
    {
        sfxSource.volume = volume;
    }
}
