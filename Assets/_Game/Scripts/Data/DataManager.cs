using System.IO;
using NUnit.Framework;
using UnityEngine;
using System.Collections.Generic;
using Unity.VisualScripting;
using JetBrains.Annotations;
using NUnit.Framework.Internal;

public class DataManager : Singleton<DataManager>
{
    //private static string destinationParth = Application.persistentDataPath + "/playerData.json";
    public static string destinationParth = "Assets/playerData.json";

    public static void SaveDataToLocal(DataPlayer data)
    {
        string json = JsonUtility.ToJson(data, true);
        File.WriteAllText(destinationParth, json);
        Debug.Log("<color=green>SAVE DATA TO LOCAL</color>");
    }

    public static DataPlayer LoadDataFromLocal()
    {
        if (File.Exists(destinationParth))
        {
            string json = File.ReadAllText(destinationParth);
            DataPlayer data = JsonUtility.FromJson<DataPlayer>(json);
            Debug.Log("<color=green>LOAD DATA FROM LOCAL</color>");
            return data;
        }
        else
        {
            Debug.Log("<color=green>LOAD DEFAULT DATA</color>");
            DataPlayer data = InitDefaultData();
            SaveDataToLocal(data);
            return data;
        }
    }

    public static DataPlayer InitDefaultData()
    {
        int df_gold = 20;
        List<int> df_wpS = new List<int> {2, 0, 0, 0, 0, 0, 0, 0, 0,};
        List<int> df_hatS = new List<int> { 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, };
        List<int> df_pantS = new List<int> { 0, 0, 0, 0, 0, 0, 0, 0, 0, };
        List<int> df_skinS = new List<int> { 0, 0, 0, 0, };
        List<int> df_levelS = new List<int> { 2, 0, 0, 0, 0 };
        AudioData df_audioData = new AudioData(1f, 1f);
        DataPlayer df_data = new DataPlayer(df_gold, df_wpS, df_hatS, df_pantS, df_skinS, df_levelS, df_audioData);
        return df_data;
    }

    public static void SaveAudioSetting(float bgMusicVolume, float sfxVolulme)
    {
        DataPlayer data = LoadDataFromLocal();
        AudioData audio = new AudioData(bgMusicVolume, sfxVolulme);
        data.audioSetting = audio;
        SaveDataToLocal(data);
    }

    public static List<int> GetLevelStatus()
    {
        return LoadDataFromLocal().levelStatus;
    }

    public static int GetGoldOfPlayer()
    {
        return LoadDataFromLocal().gold;
    }

    public static void SetGold(int newGold)
    {
        DataPlayer data = LoadDataFromLocal();
        data.gold = newGold;
        SaveDataToLocal(data);
    }

    public static List<int> UpdateItemStatus(List<int> items, int itemIndex)
    {
        for(int i = 0; i < items.Count; i++)
        {
            if (i == itemIndex)
            {
                items[i] = 2;
                continue;
            }
            if (items[i] == 2) items[i] = 1;
        }
        return items;
    }

    public static void CompleteLevelStatus(int indexLevel)
    {
        DataPlayer data = LoadDataFromLocal();
        if (indexLevel + 1 < data.levelStatus.Count)
        {
            data.levelStatus[indexLevel + 1] = 1;
            SaveDataToLocal(data);
        }
    }

    public static void NextLvelStatus()
    {
        DataPlayer data = LoadDataFromLocal();
        for(int i = 0; i < data.levelStatus.Count; i++)
        {
            if (data.levelStatus[i] == 2)
            {
                if(i + 1 < data.levelStatus.Count)
                {
                    data.levelStatus[i] = 1;
                    data.levelStatus[i + 1] = 2;
                }
            }
        }
    }

    public static bool CheckWasBuy(EnumItemCategory type, int itemIndex)
    {
        DataPlayer data = LoadDataFromLocal();
        
        if(type == EnumItemCategory.PATN)
        {
            return CheckitemWasBuy(data.pantStatus, itemIndex);
        }

        if (type == EnumItemCategory.HAT)
        {
            return CheckitemWasBuy(data.hatStatus, itemIndex);
        }

        if (type == EnumItemCategory.SKIN)
        {
            return CheckitemWasBuy(data.skinStatus, itemIndex);
        }

        if(type == EnumItemCategory.WP)
        {
            return CheckitemWasBuy(data.wpStatus, itemIndex);
        }

        return false;
    }

    public static bool CheckWasEquiped(EnumItemCategory type, int itemIndex)
    {
        DataPlayer data = LoadDataFromLocal();

        if (type == EnumItemCategory.PATN)
        {
            return CheckItemWarEquiped(data.pantStatus, itemIndex);
        }

        if (type == EnumItemCategory.HAT)
        {
            return CheckItemWarEquiped(data.hatStatus, itemIndex);
        }

        if (type == EnumItemCategory.SKIN)
        {
            return CheckItemWarEquiped(data.skinStatus, itemIndex);
        }

        if (type == EnumItemCategory.WP)
        {
            return CheckItemWarEquiped(data.wpStatus, itemIndex);
        }

        return false;
    }

    private static bool CheckitemWasBuy(List<int> items ,int itemIndex)
    {
        for(int i = 0; i < items.Count; i++)
        {
            if(i == itemIndex && (items[i] == 1 /*|| items[i] == 2)*/)) return true;
        }
        return false;
    }

    private static bool CheckItemWarEquiped(List<int> items, int itemIndex)
    {
        for (int i = 0; i < items.Count; i++)
        {
            if (i == itemIndex && items[i] == 2) return true;
        }
        return false;
    }
    
    public static int GetIndexCurrentWp()
    {
        DataPlayer data = LoadDataFromLocal();
        for(int i = 0; i < data.wpStatus.Count; i++) {
            {
                if (data.wpStatus[i] == 2) return i; 
            }
        }
        return 0;
    }
}
    