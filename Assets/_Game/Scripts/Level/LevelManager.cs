using System;
using System.Collections.Generic;
using JetBrains.Annotations;
using UnityEngine;

public class LevelManager : Singleton<LevelManager>
{
    [SerializeField] CameraFollower cameraFollower;
    [SerializeField] LevelControl currentLevel;
    [SerializeField] FloatingJoystick floatJT;
    [SerializeField] CanvasGameplay cvGamePlay;
    [SerializeField] LevelUI_SO levelSO;

    //cache levels
    Dictionary<EnumLevelType, LevelControl> levels = new Dictionary<EnumLevelType, LevelControl>();

    private EnumLevelType currentLevelType;

    public List<int> levelStatus = new List<int>();
    private void Awake()
    {
        LoadAllLevel();
        levelStatus = DataManager.GetLevelStatus();
    }

    private void LoadAllLevel()
    {
        GameObject[] loadLevels = Resources.LoadAll<GameObject>("Level/");

        foreach (GameObject go in loadLevels)
        {
            LevelControl lv = go.GetComponent<LevelControl>();
            if (!levels.ContainsKey(lv.lvType))
            {
                levels.Add(lv.lvType, lv);
            }
        }
    }

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void SetCamerafollower()
    {
        if (currentLevel == null) return;
        cameraFollower.SetTarget(currentLevel.currentPlayer.transform);
    }

    public void SetFloatJoystick(FloatingJoystick jt)
    {
        floatJT = jt;
    }

    public FloatingJoystick GetFloatJoystick()
    {
        return floatJT;
    }

    public void ResetCurrentLevel()
    {
        if(currentLevel == null) return;

        currentLevel.ResetLevel();  
    }

    public void DeSpawnCurrentLevel()
    {
        if(currentLevel != null)
        {
            Destroy(currentLevel.gameObject);
        }
    }

    public Vector3 GetPlayerPosition()
    {
        if (currentLevel == null) return Vector3.zero;

        return currentLevel.currentPlayer.transform.position;
    }

    public void SpawnLevel(EnumLevelType lvType)
    {
        if (!levels.ContainsKey(lvType)) return;

        if(currentLevel != null) DeSpawnCurrentLevel();

        currentLevel = Instantiate(levels[lvType], this.transform.parent);

        SetPointForCurrentLevel(currentLevel.currentLevelPoint);
    }

    public void SetCanvasGamePlay(CanvasGameplay cvgp)
    {
        this.cvGamePlay = cvgp;
    }

    public void SetPointForCurrentLevel(int point)
    {
        if(cvGamePlay != null)
        {
            cvGamePlay.SetText(cvGamePlay.pointText, $"{point}/{currentLevel.levelPoint}");
        }
    }

    public void SetLevelStatus()
    {
        levelStatus = DataManager.GetLevelStatus();
    }

    public EnumLevelType GetLevelType()
    {
        for (int i = 0; i < levels.Count; i++)
        {
            if (levelStatus[i] == 2)
            {
                return levelSO.leveUIDataListSO[i].levelType;
            }
        }
        return EnumLevelType.LELVEL_1;
    }
}
