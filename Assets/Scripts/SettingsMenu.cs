using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.UI; 
using UnityEngine.SceneManagement;

public class SettingsMenu : MonoBehaviour
{
    [Header("Audio Settings")]
    public AudioMixer mainMixer;
    public Slider musicSlider;
    public Slider sfxSlider;

    void Start()
    {
        float musicVal = PlayerPrefs.GetFloat("MusicVolume", 0.75f);
        float sfxVal = PlayerPrefs.GetFloat("SFXVolume", 0.75f);

        musicSlider.value = musicVal;
        sfxSlider.value = sfxVal;

        SetMusicVolume(musicVal);
        SetSFXVolume(sfxVal);
    }

    public void SetMusicVolume(float sliderValue)
    {
        PlayerPrefs.SetFloat("MusicVolume", sliderValue);

        mainMixer.SetFloat("MusicVolume", Mathf.Log10(sliderValue) * 20);
    }

    public void SetSFXVolume(float sliderValue)
    {
        PlayerPrefs.SetFloat("SFXVolume", sliderValue);
        mainMixer.SetFloat("SFXVolume", Mathf.Log10(sliderValue) * 20);
    }

    public void BackToMenu()
    {
        SceneManager.LoadScene("MainMenu");
    }
}