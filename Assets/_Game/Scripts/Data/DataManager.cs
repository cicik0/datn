using System.IO;
using NUnit.Framework;
using UnityEngine;
using System.Collections.Generic;

public class DataManager : Singleton<DataManager>
{
    //private static string destinationParth = Application.persistentDataPath + "/playerData.json";
    private static string destinationParth = "Assets/playerData.json";

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
        DataPlayer df_data = new DataPlayer(df_gold, df_wpS, df_hatS, df_pantS, df_skinS);
        return df_data;
    }
}
