using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioManager : TorpedoShooter
{
    public static AudioManager Instance;
    //public static AudioManager instance;
    public Sound[] musicSounds, sfxSounds;
    public AudioSource musicSource, sfxSource;

    private void Start()
    {
        Debug.Log("Started");
        PlayMusic("underWaterAmbience");
    }

    private void Awake()
    {
        if(Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
            
        }
        else
        {
            Destroy(gameObject);
        }
    }

    public void PlayMusic(string name)
    {
        Debug.Log("Music");
        Sound s = Array.Find(musicSounds, x => x.name == name);
        if (s == null)
        {
            Debug.Log("Sound not Found!");
            
        }
        else
        {
            musicSource.clip = s.clip;
            musicSource.Play();
        }
    }

    public void PlaySound(string name)
    {
        Sound s = Array.Find(sfxSounds, x => x.name == name);
        if (s != null)
        {
            //sfxSource.clip = s.clip;
            sfxSource.PlayOneShot(s.clip);
        }
        else
        {
            Debug.Log("Sound not Found!");
        }
    }

}
