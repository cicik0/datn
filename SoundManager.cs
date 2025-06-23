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
        
    }

    // Update is called once per frame
    void Update()
    {
        
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
                case EnumSoundCategory.SFX:
                    if (!bgMusics.ContainsKey(s.soundType))
                    {
                        bgMusics.Add(s.soundType, s.clip);
                    }
                    break;
                case EnumSoundCategory.BG_MUSIC:
                    if (!sfxs.ContainsKey(s.soundType))
                    {
                        sfxs.Add(s.soundType, s.clip);
                    }
                    break;
            }
        }
    }
}
