using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;

public class SoundManager : MonoBehaviour {

    public static SoundManager instance;
    public AudioMixer audioMixer;

    
    float musicVolume = 1f;
    float soundVolume = 1f;

    private void Start()
    {
        if(instance == null)
        {
            instance = this;
        }
        else
        {
            Destroy(this);
        }
    }

    private void Awake()
    {
        LoadSavedValues();
    }

    private void OnDestroy()
    {
        SaveValues();
    }

    public void ChangeMusicVolume(float newValue)
    {
        musicVolume = newValue;
        SaveValues();
        CommitVolumeChanges();
    }

    public void ChangeSoundVolume(float newValue)
    {
        soundVolume = newValue;
        SaveValues();
        CommitVolumeChanges();
    }

    void LoadSavedValues()
    {
        musicVolume = PlayerPrefs.GetFloat("musicVolume", 1f);
        soundVolume = PlayerPrefs.GetFloat("soundVolume", 1f);
    }

    void SaveValues()
    {
        PlayerPrefs.SetFloat("musicVolume", musicVolume);
        PlayerPrefs.SetFloat("soundVolume", soundVolume);
    }

    void CommitVolumeChanges()
    {
        audioMixer.SetFloat("musicVolume", musicVolume);
    }
}
