// ------------------------------------
// SettingManager.cs
//
// 환경설정 Manager 
// ------------------------------------

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SettingManager : MonoBehaviour
{
    public static SettingManager instance;

    [SerializeField]
    private Slider[] SoundSlider = new Slider[2];
    [SerializeField]
    private Toggle[] SoundToggle = new Toggle[2];

    void Start()
    {
        instance = this;
        SetSound();
    }

    public void SetSound()
    {
        SoundSlider[0].value = PlayerPrefs.GetFloat("BGM_s", 1);
        SoundSlider[1].value = PlayerPrefs.GetFloat("Effect_s", 1);

        SoundToggle[0].isOn = (PlayerPrefs.GetInt("BGM_t") == 1) ? true : false;
        SoundToggle[1].isOn = (PlayerPrefs.GetInt("Effect_t") == 1) ? true : false;
    }

    // BGM Volume Slider
    public void BGMVolume_s(Slider bgm)
    {
        PlayerPrefs.SetFloat("BGM_s", bgm.value);
        SoundManager.instance.SetVolume(1, bgm.value);
    }

    // Effect Volume Slider
    public void EffectVolume_s(Slider effect)
    {
        PlayerPrefs.SetFloat("Effect_s", effect.value);
        SoundManager.instance.SetVolume(2, effect.value);
    }

    // BGM Volume Toggle
    public void BGMVolume_t()
    {
        float volume;
        int togglechk;

        if (SoundToggle[0].isOn)
        {
            volume = 0;
            togglechk = 1;
        }
        else
        {
            volume = SoundSlider[0].value;
            togglechk = 0;
        }

        PlayerPrefs.SetInt("BGM_t", togglechk);
        SoundManager.instance.SetVolume(1, volume);
    }

    // Effect Volume Slider
    public void EffectVolume_t()
    {
        float volume;
        int togglechk;

        if (SoundToggle[1].isOn)
        {
            volume = 0;
            togglechk = 1;
        }
        else
        {
            volume = SoundSlider[1].value;
            togglechk = 0;
        }

        PlayerPrefs.SetInt("Effect_t", togglechk);
        SoundManager.instance.SetVolume(2, volume);
    }
}
