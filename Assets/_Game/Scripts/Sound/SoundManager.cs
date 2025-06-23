using System.Collections.Generic;
using UnityEngine;

public class SoundManager : Singleton<SoundManager>
{
    [SerializeField] AudioSource bgMusicAudioSource;
    [SerializeField] AudioSource sfxAudioSource;
    [SerializeField] SoundSO soundSO;

    //dic cache sfx
    Dictionary<EnumSoundType, AudioClip> sfxs = new Dictionary<EnumSoundType, AudioClip>();

    //dic cache bg_music 
    Dictionary<EnumSoundType, AudioClip> bgMusics = new Dictionary<EnumSoundType, AudioClip>();

    private float bgMusicVolume;
    private float sfxMusicVolume;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        InitDictionary();
        DataPlayer data = DataManager.LoadDataFromLocal();
        SetVolume(data.audioSetting.bgMusicValue, data.audioSetting.sfxValue);
    }

    public void SetVolume(float bgMusic, float sfx)
    {
        this.bgMusicVolume = bgMusic;
        this.sfxMusicVolume = sfx;
    }

    private void InitDictionary()
    {
        foreach(var s in soundSO.soundLisSO)
        {
            switch (s.soundCatory)
            {
                case EnumSoundCategory.BG_MUSIC:
                    if (!bgMusics.ContainsKey(s.soundType))
                    {
                        bgMusics.Add(s.soundType, s.clip);
                    }
                    break;
                case EnumSoundCategory.SFX:
                    if (!sfxs.ContainsKey(s.soundType))
                    {
                        sfxs.Add(s.soundType, s.clip);
                    }
                    break;
            }
        }
    }

    public void PlayBGMusic(EnumSoundType type, bool loop = true)
    {
        if (bgMusics.TryGetValue(type, out var clip))
        {
            if (bgMusicAudioSource.clip == clip && bgMusicAudioSource.isPlaying)
                return;

            bgMusicAudioSource.Stop();
            bgMusicAudioSource.clip = clip;
            bgMusicAudioSource.loop = loop;
            bgMusicAudioSource.volume = bgMusicVolume;
            bgMusicAudioSource.Play();
        }
    }

    public void PlaySFX(EnumSoundType type)
    {
        if (sfxs.TryGetValue(type, out var clip))
        {
            sfxAudioSource.PlayOneShot(clip, sfxMusicVolume);
        }
    }

    public void StopBGMusic()
    {
        bgMusicAudioSource.Stop();
    }

}
