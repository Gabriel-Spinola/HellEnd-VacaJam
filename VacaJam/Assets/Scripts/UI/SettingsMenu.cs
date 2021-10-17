using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SettingsMenu : MonoBehaviour
{
    [SerializeField] private Slider _masterVolumeSlider;
    [SerializeField] private Slider _SFXVolumeSlider;
    [SerializeField] private Slider _musicVolumeSlider;

    private void Start()
    {
        _masterVolumeSlider.value = AudioManager._I.MasterVolumePercent;
        _SFXVolumeSlider.value = AudioManager._I.SFXVolumePercent;
        _musicVolumeSlider.value = AudioManager._I.MusicVolumePercent;
    }

    public void SetMasterVolume(float volume) => AudioManager._I.SetVolume(volume, AudioManager.AudioChannel.Master);

    public void SetSFXVolume(float volume) => AudioManager._I.SetVolume(volume, AudioManager.AudioChannel.SFX);

    public void SetMusicVolume(float volume) => AudioManager._I.SetVolume(volume, AudioManager.AudioChannel.Music);
}
