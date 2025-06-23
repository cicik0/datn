using System;
using UnityEngine;

[Serializable]

public class AudioData
{
    public float bgMusicValue;

    public float sfxValue;

    public AudioData() { }

    public AudioData(float value1, float value2)
    {
        this.bgMusicValue = value1;
        this.sfxValue = value2;
    }
}
