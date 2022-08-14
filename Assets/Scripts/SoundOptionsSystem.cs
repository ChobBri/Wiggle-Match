using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.UI;

public class SoundOptionsSystem : MonoBehaviour
{
    [SerializeField] AudioMixer audioMixer;

    [SerializeField] Slider masterVolumeSlider;
    [SerializeField] Slider musicVolumeSlider;
    [SerializeField] Slider soundVolumeSlider;

    void Start()
    {
        LoadValues();
    }

    public void SetMasterVolume(float value)
    {
        masterVolumeSlider.value = value;

        float volume = Mathf.Log10(Mathf.Clamp(value, 0.00001f, 1.0f)) * 20.0f;

        audioMixer.SetFloat("Master", volume);
        PlayerPrefs.SetFloat("MasterVolume", value);
    }
    public void SetMusicVolume(float value)
    {
        musicVolumeSlider.value = value;

        float volume = Mathf.Log10(Mathf.Clamp(value, 0.00001f, 1.0f)) * 20.0f;

        audioMixer.SetFloat("Music", volume);
        PlayerPrefs.SetFloat("MusicVolume", value);
    }

    public void SetSoundVolume(float value)
    {
        soundVolumeSlider.value = value;

        float volume = Mathf.Log10(Mathf.Clamp(value, 0.00001f, 1.0f)) * 20.0f;

        audioMixer.SetFloat("Sounds", volume);
        PlayerPrefs.SetFloat("SoundVolume", value);
    }

    void LoadValues()
    {
        SetMasterVolume(PlayerPrefs.GetFloat("MasterVolume", 1.0f));
        SetMusicVolume(PlayerPrefs.GetFloat("MusicVolume", 1.0f));
        SetSoundVolume(PlayerPrefs.GetFloat("SoundVolume", 1.0f));
    }
}
