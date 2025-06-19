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

    public DataPlayer () { }

    public DataPlayer(int g, List<int> wpS, List<int> hatS, List<int> pantS, List<int> skinS)
    {
        this.gold = g;
        this.wpStatus = wpS;
        this.hatStatus = hatS;
        this.pantStatus = pantS;
        this.skinStatus = skinS;
    }
}
