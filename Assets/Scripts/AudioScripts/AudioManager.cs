using UnityEngine;
using System;

public class AudioManager : MonoBehaviour
{
    public static AudioManager Instance { get; private set; }

    [Header("Audio Clips")]
    public Sound[] sounds;

    public AudioSource musicSource;
    public AudioSource soundEffectsSource;

    private float musicVolume = 1f;
    private float soundEffectsVolume = 1f;

    private void Awake()
    {
        if (Instance != null)
        {
            Destroy(gameObject);
        }
        else
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
        }
    }

    private void Start()
    {
        UpdateMusicVolume();
        UpdateSoundEffectsVolume();
    }

    public void PlaySound(string soundName)
    {
        Sound sound = Array.Find(sounds, s => s.name == soundName);
        if (sound != null)
        {
            soundEffectsSource.PlayOneShot(sound.clip);
        }
        else
        {
            Debug.LogWarning("Sound not found: " + soundName);
        }
    }

    public void SetBackgroundSound(string soundName)
    {
        Sound sound = Array.Find(sounds, s => s.name == soundName);
        if (sound != null)
        {
            musicSource.clip = sound.clip;
            musicSource.Play();
        }
        else
        {
            Debug.LogWarning("Sound not found: " + soundName);
        }
    }

    public void SetMusicVolume(float volume)
    {
        musicVolume = volume;
        UpdateMusicVolume();
    }

    public float GetMusicVolume()
    {
        return musicVolume;
    }

    private void UpdateMusicVolume()
    {
        musicSource.volume = musicVolume;
    }

    public void SetSoundEffectsVolume(float volume)
    {
        soundEffectsVolume = volume;
        UpdateSoundEffectsVolume();
    }

    public float GetSoundEffectsVolume()
    {
        return soundEffectsVolume;
    }

    private void UpdateSoundEffectsVolume()
    {
        soundEffectsSource.volume = soundEffectsVolume;
    }

    [Serializable]
    public class Sound
    {
        public string name;
        public AudioClip clip;
    }
}