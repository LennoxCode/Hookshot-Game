using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

/// <summary>
/// this class is responsible for setting options with the optionsmenu. On start it sets the value to whatever pref
/// already exists and in the case of a changed slider value the corresponding value gets written to Playerprefs
/// so the AudioManager can use these settings to mix the audio accordingly 
/// </summary>
public class OptionsManager : MonoBehaviour
{
    [SerializeField] private Slider audioSlider;
    [SerializeField] private Slider musicSlider;
    void Start()
    {
        audioSlider.value = PlayerPrefs.GetFloat("audio_volume", 1.0f);
        musicSlider.value = PlayerPrefs.GetFloat("music_volume", 1.0f);
    }

    public void OnAudioSliderChanged()
    {
        float sliderVal = audioSlider.value;
        PlayerPrefs.SetFloat("audio_volume", sliderVal);
    }

    public void OnMusicSliderChanged()
    {
        float sliderVal = musicSlider.value;
        PlayerPrefs.SetFloat("music_volume", sliderVal);
    }
  
}
