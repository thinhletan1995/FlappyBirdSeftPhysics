using System;
using UnityEngine;
using UnityEngine.Audio;

public class AudioManager : MonoBehaviour
{
    [Range(0f, 1f)] public float bgMusicVolume;
    public Sound[] backgroudMusic;

    [Range(0f, 1f)] public float sfxVolume;
    public Sound[] sfxs;

    private AudioSource bgMusicAudioSource;
    private AudioSource sfxAudioSource;

    public void PlayThemeSong(string name)
    {
        Sound s = Array.Find(backgroudMusic, sound => sound.name == name);

        if (s == null)
        {
            Debug.LogWarning("Sound: " + name + " not found!");
            return;
        }

        bgMusicAudioSource.clip = s.clip;
        bgMusicAudioSource.pitch = s.pitch;
        bgMusicAudioSource.loop = s.loop;
        bgMusicAudioSource.Play();
    }

    public void PlaySFX(string name)
    {
        Sound s = Array.Find(sfxs, sound => sound.name == name);

        if (s == null)
        {
            Debug.LogWarning("Sound: " + name + " not found!");
            return;
        }

        sfxAudioSource.pitch = s.pitch;
        sfxAudioSource.PlayOneShot(s.clip);
    }

    private void Start()
    {
        bgMusicAudioSource = gameObject.AddComponent<AudioSource>();
        sfxAudioSource = gameObject.AddComponent<AudioSource>();

        bgMusicAudioSource.volume = bgMusicVolume;
        sfxAudioSource.volume = sfxVolume;
    }
}