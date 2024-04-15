using System;
using System.Collections.Generic;
using UnityEngine;

public enum EffectType
{
    BtnClick,
    Over,
} 
public class AudioMgr : MonoSingleton<AudioMgr>
{

    public List<AudioClip> backgroundMusic;  
    public EffectClipSO[] soundEffects;  

    private AudioSource backgroundMusicSource;  
    private AudioSource soundEffectSource;  

    private float backgroundVolume = 1f;  
    private float soundEffectVolume = 1f;  

    private int currentBackgroundMusicIndex = 0; 

    public bool PlayInStar = true;
    public override void Awake()
    {
        base.Awake();
        if (instance == this)
            DontDestroyOnLoad(gameObject);
        else Destroy(gameObject);
        // 初始化 AudioSource 组件
        backgroundMusicSource = gameObject.AddComponent<AudioSource>();
        soundEffectSource = gameObject.AddComponent<AudioSource>();
    }

    void Start()
    {
        if (PlayInStar)
            PlayBackgroundMusic();
    }

    public void PlayBackgroundMusic()
    {
        if (backgroundMusic.Count == 0) return;

        backgroundMusicSource.clip = backgroundMusic[currentBackgroundMusicIndex];
        backgroundMusicSource.volume = backgroundVolume;
        backgroundMusicSource.loop = true;
        backgroundMusicSource.Play();
    }
    public void PlayBackgroundMusic(AudioClip clip)
    {
        if (!clip)
            return;
        if(backgroundMusic.Contains(clip))
            currentBackgroundMusicIndex=backgroundMusic.IndexOf(clip);
        else
        {
            backgroundMusic.Add(clip);
            currentBackgroundMusicIndex = backgroundMusic.Count - 1;
        }

        backgroundMusicSource.clip = backgroundMusic[currentBackgroundMusicIndex];
        backgroundMusicSource.volume = backgroundVolume;
        backgroundMusicSource.loop = false;
        backgroundMusicSource.Play();
    }

    public void PlaySoundEffect(int index)
    {
        if (index < 0 || index >= soundEffects.Length) return;

        soundEffectSource.clip = soundEffects[index].clip;
        soundEffectSource.volume = soundEffectVolume;
        soundEffectSource.PlayOneShot(soundEffects[index].clip);
    } 
    public void PlaySoundEffect(EffectType type)
    {
        foreach (var effect in soundEffects)
            if (effect.type == type)
            {
                soundEffectSource.clip = effect.clip;
                soundEffectSource.volume = soundEffectVolume;
                soundEffectSource.PlayOneShot(effect.clip);
            }
    }

    public void SetBackgroundVolume(float volume)
    {
        backgroundVolume = Mathf.Clamp01(volume);
        backgroundMusicSource.volume = backgroundVolume;
    }

    public void SetSoundEffectVolume(float volume)
    {
        soundEffectVolume = Mathf.Clamp01(volume);
    }

    public void PauseBackgroundMusic()
    {
        backgroundMusicSource.Pause();
    }

    public void ResumeBackgroundMusic()
    {
        backgroundMusicSource.UnPause();
    }

    public void SwitchBackgroundMusic()
    {
        currentBackgroundMusicIndex = (currentBackgroundMusicIndex + 1) % backgroundMusic.Count;
        backgroundMusicSource.clip = backgroundMusic[currentBackgroundMusicIndex];
        backgroundMusicSource.Play();
    }
}
