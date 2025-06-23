using NUnit.Framework;
using UnityEngine;
using System.Collections.Generic;
using System;

[Serializable]

public class DataPlayer
{
    public int gold;

    public List<int> wpStatus;

    public List<int> hatStatus;

    public List<int> pantStatus;

    public List<int> skinStatus;

    public List<int> levelStatus;

    public AudioData audioSetting;
    public DataPlayer () { }

    public DataPlayer(int g, List<int> wpS, List<int> hatS, List<int> pantS, List<int> skinS, List<int> levelS, AudioData audioSetting)
    {
        this.gold = g;
        this.wpStatus = wpS;
        this.hatStatus = hatS;
        this.pantStatus = pantS;
        this.skinStatus = skinS;
        this.levelStatus = levelS;
        this.audioSetting = audioSetting;
    }
}
