using UnityEngine.Audio;
using UnityEngine;
using System;

public class AudioManager : MonoBehaviour
{
    public SoundFiles[] sounds;
    public static AudioManager instance; 
    void Awake()
    {
        if(instance != null) Destroy(this);
        instance = this;
        float soundVolume = PlayerPrefs.GetFloat("audio_volume", 1.0f);
        foreach (SoundFiles s in sounds)
        {
            s.source = gameObject.AddComponent<AudioSource>();
            s.source.clip = s.clip;
            s.source.volume = s.volume * soundVolume;
            s.source.pitch = s.pitch;
        }
    }

    public void Play(string name)
    {
        SoundFiles s = Array.Find(sounds, sound => sound.name == name);
        //https://www.youtube.com/watch?v=6OT43pvUyfY&t=207s
        s.source.Play();
    }
    
}

