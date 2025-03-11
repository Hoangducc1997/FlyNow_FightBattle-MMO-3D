using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundManager : MonoBehaviour
{
    public static SoundManager Instance { get; private set; }

    public SoundData SoundData;

    private void Awake()
    {
        this.InitManager();
        this.DefaultSoundData();
    }
    private void InitManager()
    {
        if (Instance != null && Instance != this)
        {
            Destroy(gameObject);
            return;
        }
        Instance = this;
        DontDestroyOnLoad(gameObject);
    }

    public void DefaultSoundData()
    {
        SoundData = new SoundData();
    }

    public float GetCurrentMusicVolume()
    {
        return SoundData.MusicVolume;
    }

    public float GetCurrentVfxVolume()
    {
        return SoundData.SFXVolume;
    }

    public void SetVfxVolume(float value)
    {
        SoundData.SFXVolume = value;
        SoundData.IsSFXMuted = SoundData.SFXVolume == 0;
    }

    public void SetMusicVolume(float value)
    {
        SoundData.MusicVolume = value;
        SoundData.IsMusicMuted = SoundData.MusicVolume == 0;
    }

    public void SetSFXVolume(float value)
    {
        SoundData.SFXVolume = value;
        SoundData.IsSFXMuted = SoundData.SFXVolume == 0;
    }

    public void ToggleMusicMute()
    {
        SoundData.IsMusicMuted = !SoundData.IsMusicMuted;
        SoundData.MusicVolume = SoundData.IsMusicMuted ? 0 : 0.5f;
    }

    public void ToggleSFXMute()
    {
        SoundData.IsSFXMuted = !SoundData.IsSFXMuted;
        SoundData.SFXVolume = SoundData.IsSFXMuted ? 0 : 0.5f;
    }
}