using UnityEngine;
using TMPro;
using UnityEngine.UI;
using Unity.VisualScripting;
public class SettingPopup : UIPopup
{
    [SerializeField] private Button musicToggleButton;
    [SerializeField] private Button sfxToggleButton;
    [SerializeField] private Button BackBtn;

    [SerializeField] private Slider musicSlider;
    [SerializeField] private Slider sfxSlider;

    [SerializeField] private Sprite musicOnIcon;
    [SerializeField] private Sprite musicOffIcon;
    [SerializeField] private Sprite sfxOnIcon;
    [SerializeField] private Sprite sfxOffIcon;

    public override void OnShown(object parament = null)
    {
        base.OnShown(parament);
        this.SetSoundData();
        this.UpdateButtonIcons();
        this.RegisterEvents();
    }

    public override void OnHide()
    {
        AudioManager.Instance.PlayVFX("Click UI");
        base.OnHide();
        Time.timeScale = 1f; // Tiếp tục thời gian trong game
        if (GameSceneManager.Instance.GetCurrentScene() == SceneName.StartGame.ToString() )UIManager.Instance.ShowPopup(PopupName.StartGamePopup);
    }
    void RegisterEvents()
    {
        musicSlider.onValueChanged.AddListener(OnMusicSliderChanged);
        sfxSlider.onValueChanged.AddListener(OnSFXSliderChanged);

        musicToggleButton.onClick.AddListener(ToggleMusicMute);
        sfxToggleButton.onClick.AddListener(ToggleSFXMute);

        BackBtn.onClick.AddListener(() => { this.OnHide(); });
    }
    void SetSoundData()
    {
        musicSlider.value = SoundManager.Instance.SoundData.MusicVolume;
        sfxSlider.value = SoundManager.Instance.SoundData.SFXVolume;
    }

    private void OnMusicSliderChanged(float value)
    {
        SoundManager.Instance.SetMusicVolume(value);
        AudioManager.Instance.GetMusicAudioSource().volume = value;
        UpdateButtonIcons();
    }

    private void OnSFXSliderChanged(float value)
    {
        SoundManager.Instance.SetSFXVolume(value);
        AudioManager.Instance.GetVFXAudioSource().volume = value;
        UpdateButtonIcons();
    }

    private void ToggleMusicMute()
    {
        SoundManager.Instance.ToggleMusicMute();
        musicSlider.value = SoundManager.Instance.SoundData.MusicVolume;
        AudioManager.Instance.GetMusicAudioSource().volume = SoundManager.Instance.SoundData.MusicVolume;
        UpdateButtonIcons();
    }

    private void ToggleSFXMute()
    {
        SoundManager.Instance.ToggleSFXMute();
        sfxSlider.value = SoundManager.Instance.SoundData.SFXVolume;
        AudioManager.Instance.GetVFXAudioSource().volume = SoundManager.Instance.SoundData.SFXVolume;
        UpdateButtonIcons();
    }

    private void UpdateButtonIcons()
    {
        musicToggleButton.image.sprite = SoundManager.Instance.SoundData.IsMusicMuted ? musicOffIcon : musicOnIcon;
        sfxToggleButton.image.sprite = SoundManager.Instance.SoundData.IsSFXMuted ? sfxOffIcon : sfxOnIcon;
    }
}
