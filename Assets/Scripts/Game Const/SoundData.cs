using UnityEngine;

public class SoundData
{
    public float MusicVolume = 0.5f;
    public float SFXVolume = 0.5f;

    public bool IsMusicMuted = false;
    public bool IsSFXMuted = false;

    public SoundData()
    {
        this.MusicVolume = SoundConst.MUSIC_VOLUME_DEFAULT;
        this.SFXVolume = SoundConst.SFX_VOLUME_DEFAULT;
        this.IsMusicMuted = false;
        this.IsSFXMuted = false;
    }
}


public class SoundConst
{
    public const float MUSIC_VOLUME_DEFAULT = 0.5f;
    public const float SFX_VOLUME_DEFAULT = 0.5f;
}


