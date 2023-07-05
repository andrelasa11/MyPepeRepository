using UnityEngine;
using System;

public class AudioManager : MonoBehaviour
{
    public static AudioManager Instance { get; private set; }

    [Header("Audio Clips")]
    public Sound[] sounds;

    public AudioSource AudioSource { get; private set; }

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

        AudioSource = GetComponent<AudioSource>();
    }

    public void PlaySound(string soundName, AudioClip clip = null)
    {
        Sound sound = Array.Find(sounds, s => s.name == soundName);
        if (sound != null)
        {
            AudioSource.PlayOneShot(clip != null ? clip : sound.clip);
        }
        else
        {
            Debug.LogWarning("Sound not found: " + soundName);
        }
    }

    public void SetBackgroundSound(string soundName, AudioClip clip = null)
    {
        Sound sound = Array.Find(sounds, s => s.name == soundName);
        if (sound != null)
        {
            AudioSource.clip = clip != null ? clip : sound.clip;
            AudioSource.Play();
        }
        else
        {
            Debug.LogWarning("Sound not found: " + soundName);
        }
    }

    [Serializable]
    public class Sound
    {
        public string name;
        public AudioClip clip;
    }
}