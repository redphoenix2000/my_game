﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System.IO;

public class OptionsManager : MonoBehaviour{
    public Toggle fullscreentoggle;
    public Dropdown qualitydropdown;
    public Dropdown resolutiondropdown;
    public Resolution[] resolutions;
    public Options options;
    public Slider slidervolume;
    public AudioSource musicSource;
    public Button apply;
    void OnEnable()
    {
        options = new Options();
        fullscreentoggle.onValueChanged.AddListener(delegate { OnFullscreenToggle(); });
        resolutiondropdown.onValueChanged.AddListener(delegate { OnResolutionChange(); });
        qualitydropdown.onValueChanged.AddListener(delegate { OnQualityChange(); });
        slidervolume.onValueChanged.AddListener(delegate { OnMusicChanged(); });
        apply.onClick.AddListener(delegate { savebutton(); });
        resolutions = Screen.resolutions;
        foreach (Resolution resol in resolutions)
        {
            resolutiondropdown.options.Add(new Dropdown.OptionData(resol.ToString()));
        }
        LoadSettings();
    }
    public void OnFullscreenToggle()
    {
       options.fullscreen = Screen.fullScreen = fullscreentoggle.isOn;
    }
    public void OnResolutionChange()
    {
        Screen.SetResolution(resolutions[resolutiondropdown.value].width, resolutions[resolutiondropdown.value].height, Screen.fullScreen);
        options.resolution = resolutiondropdown.value;
    }
    public void OnQualityChange()
    {
        QualitySettings.masterTextureLimit = options.quality = qualitydropdown.value;
    }
    public void OnMusicChanged()
    {
        musicSource.volume = options.musicvolume = slidervolume.value;
    }
    public void savebutton()
    {
        SaveSettings();
    }
    public void SaveSettings()
    {
        string jsondata = JsonUtility.ToJson(options,true);
        File.WriteAllText(Application.persistentDataPath + "/optionsettings.json",jsondata);

    }
    public void LoadSettings()
    {
        if (File.Exists(Application.persistentDataPath + "/optionsettings.json") == true)
        {
            JsonUtility.FromJsonOverwrite(File.ReadAllText(Application.persistentDataPath + "/optionsettings.json"),options);
            slidervolume.value = options.musicvolume;
            resolutiondropdown.value = options.resolution;
            fullscreentoggle.isOn = options.fullscreen;
            qualitydropdown.value = options.quality;
            Screen.fullScreen = options.fullscreen;
            resolutiondropdown.RefreshShownValue();
        }
    }


}
