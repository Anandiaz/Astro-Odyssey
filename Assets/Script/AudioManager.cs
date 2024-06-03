using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioManager : MonoBehaviour
{
    public static AudioManager instance;
    public Song[] musicSounds;
    public AudioSource musicSource;
    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }
    }

    private void Start()
    {
        PlayMusic("Theme");
        GameObject.DontDestroyOnLoad(this.gameObject);
    }

    public void PlayMusic(string name)
    {
        Song s = Array.Find(musicSounds, x => x.name == name);
        if (s == null)
        {
            Debug.Log("Sound Not Found");
        }
        else
        {
            musicSource.clip = s.clip;
            musicSource.Play();
        }
    }

    public void OnDestroy()
    {
        musicSource.Stop();
    }

    public void PlaySound(AudioClip sound)
    {
        musicSource.PlayOneShot(sound);
    }
}