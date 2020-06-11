using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// ** 추가 작업 필요 **
// 정해진 BGM, 이펙트 가짓수나 흐름을 모르기 때문에 러프하게 작업했음

public class SoundManager : MonoBehaviour
{
    public static SoundManager instance;

    [SerializeField]
    private AudioClip[] BGMClip;
    [SerializeField]
    private AudioClip[] EffectClip;

    private AudioSource BGMSource;
    private AudioSource[] EffectSource = new AudioSource[15];
    private int EffectSourceNum = 0;

    private void Awake()
    {
        instance = this;

        BGMSource = gameObject.AddComponent<AudioSource>();
        BGMSource.playOnAwake = false;
        BGMSource.loop = true;

        // Volume을 PlayerPrefs에 저장하고 불러옴
        if (PlayerPrefs.GetInt("BGM_t") == 1)
            SetVolume(1, 0);
        else
            SetVolume(1, PlayerPrefs.GetFloat("BGM_s", 1));

        if (PlayerPrefs.GetInt("Effect_t") == 1)
            SetVolume(2, 0);
        else
            SetVolume(2, PlayerPrefs.GetFloat("Effect_s", 1));
    }

    // BGM
    public void SetBGM(string name)
    {
        AudioClip ChangeClip = null;

        for(int i = 0; i < BGMClip.Length; ++i)
        {
            if(BGMClip[i].name == name)
            {
                ChangeClip = BGMClip[i];
                break;
            }
        }

        if (ChangeClip == null) return;

        BGMSource.clip = ChangeClip;
        BGMSource.Play();
    }

    public void SetEffect(bool set, AudioSource source, string name)
    {
        for(int i = 0; i < EffectClip.Length; ++i)
        {
            if(EffectClip[i].name == name)
            {
                if (!set) EffectSource[EffectSourceNum++] = source;
                source.clip = EffectClip[i];
                if (PlayerPrefs.GetInt("Effect_t") == 1)
                    source.volume = 0f;
                else
                    source.volume = PlayerPrefs.GetFloat("Effect_s", 1);
                return;
            }
        }
    }

    // 효과음
    public void CreateEffect()
    {
        // 임의 숫자
        for (int i = 0; i < 3; ++i)
        {
            EffectSource[i] = gameObject.AddComponent<AudioSource>();
            EffectSource[i].playOnAwake = false;
        }
    }

    // 효과음 플레이
    public void PlayEffect(string name)
    {
        for (int i = 0; i < EffectClip.Length; ++i)
        {
            if (EffectSource[i] != null)
            {
                if (EffectClip[i].name == name)
                {
                    AudioSource source = GetEmptyEffectSource();
                    source.clip = EffectClip[i];
                    // setting UI 만들면 default 값 없애줄 것.
                    if (PlayerPrefs.GetInt("Effect_t") == 1)
                        source.volume = 0f;
                    else
                        source.volume = PlayerPrefs.GetFloat("Effect_s", 1);
                    source.Play();

                    return;
                }
            }
        }
    }

    // 빈 오디오 소스 받아오기
    AudioSource GetEmptyEffectSource()
    {
        int MaxIdx = 0;
        float MaxProgress = 0;

        for (int i = 0; i < EffectSource.Length; ++i)
        {
            if (!EffectSource[i].isPlaying) return EffectSource[i];

            float progress = EffectSource[i].time / EffectSource[i].clip.length;

            if (progress > MaxProgress && !EffectSource[i].loop)
            {
                MaxIdx = i;
                MaxProgress = progress;
            }
        }

        return EffectSource[MaxIdx];
    }

    public void SetVolume(int num, float volume)
    {
        if (num == 1) BGMSource.volume = volume;
        else if(num == 2)
        {
            for (int i = 0; i < EffectSource.Length; ++i)
            {
                if (EffectSource[i] != null)
                    EffectSource[i].volume = volume;
            }
        }
    }
}
