using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class VolumeChanger : MonoBehaviour {

    public Slider slider;

    enum WhatToDo
    {
        UpdateSoundVolume,
        UpdateMusicVolume
    }

    [SerializeField]
    WhatToDo whatToDo;

    public void ValueHasChanged()
    {
        switch(whatToDo)
        {
            case WhatToDo.UpdateMusicVolume:
                SoundManager.instance.ChangeMusicVolume(slider.value);
                break;
            case WhatToDo.UpdateSoundVolume:
                SoundManager.instance.ChangeSoundVolume(slider.value);
                break;
        }
    }
}
