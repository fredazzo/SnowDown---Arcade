﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundManager : MonoBehaviour
{
    public AudioSource musicSource;

    public AudioSource p1ShootingSource;
    public AudioSource p1HitSource;
    public AudioSource p2ShootingSource;
    public AudioSource p2HitSource;
    public AudioSource coverHitSource;
    public AudioSource coverSpawnSource;
    public AudioSource p1MoveSource;
    public AudioSource p2MoveSource;
    public AudioSource p1DeathSource;
    public AudioSource p2Deathsource;
    public AudioSource p1ReloadSource;
    public AudioSource p2ReloadSource;
    public AudioSource menuMusic;
    public AudioSource menuClickP1;
    public AudioSource menuClickP2;
    public AudioSource powerUpSound;
    public AudioSource bigWin;

    public static SoundManager instance = null;

    public float lowPitchRange = .95f;
    public float highPitchRange = 1.05f;

    // Start is called before the first frame update
    void Awake()
    {
        if (instance == null)
            instance = this;
        else if (instance != this)
            Destroy(gameObject);

        DontDestroyOnLoad(gameObject);
    }

    public void PlaySingle(AudioSource source)
    {
        float randomPitch = Random.Range(lowPitchRange, highPitchRange);

        source.pitch = randomPitch;
        source.Play();
    }



    //public void RandomizeSfx(AudioSource source, params AudioClip[] clips)
    //{
    //    int randomIndex = Random.Range(0, clips.Length);
    //    //float randomPitch = Random.Range(lowPitchRange, highPitchRange);

    //    //source.pitch = randomPitch;
    //    source.clip = clips[randomIndex];
    //    source.Play();
    //}

}
