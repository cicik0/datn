using NUnit.Framework;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using System.Collections.Generic;
using Unity.VisualScripting;

public class CanvasMainMenu : UiCanvas
{
    [SerializeField] Button playBtn;
    [SerializeField] Button nextBtn;
    [SerializeField] Button prevBtn;
    [SerializeField] Button skinShopBtn;
    [SerializeField] Button weaponShopBtn;
    [SerializeField] Button shopBtn;
    [SerializeField] Button settingBtn;
    [SerializeField] TMP_Text goldText;
    [SerializeField] TMP_Text levelNameText;
    [SerializeField] LevelUI_SO levelUI_SO;

    [SerializeField] RectTransform wpShopRT;
    [SerializeField] RectTransform skinShopRT;
    [SerializeField] RectTransform shopRT;

    private Vector2 openWpShopOffset = new Vector2(200, 100);
    private Vector2 openSkinShopOffset = new Vector2(200, -100);

    private bool isClickShopBtn = false;

    int levelIndex = 0;
    EnumLevelType currentLevelType;

    private void OnEnable()
    {
        SoundManager.Ins.PlayBGMusic(EnumSoundType.BG_MUSIC_MAIN);
        SetText(goldText, DataManager.LoadDataFromLocal().gold.ToString());
        SetCurrentLevelName();
    }

    public override void SetUp()
    {
        base.SetUp();
    }

    public override void BackKey()
    {
        base.BackKey();
    }

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        playBtn.onClick.AddListener(() => OnClickPlayBtn());
        nextBtn.onClick.AddListener(() => OnClickNextBtn());
        prevBtn.onClick.AddListener(() => OnClickPrevBtn());
        skinShopBtn.onClick.AddListener(() => OnClickSkinShopBtn());
        weaponShopBtn.onClick.AddListener(() => OnClickWeaponShopBtn());
        shopBtn.onClick.AddListener(() => OnClickShopBtn());
        settingBtn.onClick.AddListener(() => OnClickSettingBtn());
    }

    public void OnClickPlayBtn()
    {
        SoundManager.Ins.PlaySFX(EnumSoundType.SFX_CLICK_BUTTON);
        //Debug.Log("Button play on click");
        CloseDirectly();
        UiManager.Ins.OpenUI<CanvasGameplay>();
        LevelManager.Ins.SpawnLevel(currentLevelType);
    }

    public void OnClickNextBtn()
    {
        SoundManager.Ins.PlaySFX(EnumSoundType.SFX_CLICK_BUTTON);
        ChangeCurrentLevelName(levelIndex + 1);
    }

    public void OnClickPrevBtn()
    {
        SoundManager.Ins.PlaySFX(EnumSoundType.SFX_CLICK_BUTTON);
        ChangeCurrentLevelName(levelIndex - 1);
    }

    public void OnClickSkinShopBtn()
    {
        SoundManager.Ins.PlaySFX(EnumSoundType.SFX_CLICK_BUTTON);
        CloseDirectly();
        UiManager.Ins.OpenUI<CanvasSkinShop>();
    }

    public void OnClickWeaponShopBtn()
    {
        SoundManager.Ins.PlaySFX(EnumSoundType.SFX_CLICK_BUTTON);
        CloseDirectly();
        UiManager.Ins.OpenUI<CanvasWeaponShop>();
    }

    public void OnClickShopBtn()
    {
        SoundManager.Ins.PlaySFX(EnumSoundType.SFX_CLICK_BUTTON);
        if (!isClickShopBtn)
        {
            wpShopRT.anchoredPosition = shopRT.anchoredPosition + openWpShopOffset;
            skinShopRT.anchoredPosition = shopRT.anchoredPosition + openSkinShopOffset;
        }
        else
        {
            wpShopRT.anchoredPosition -= openWpShopOffset;
            skinShopRT.anchoredPosition -= openSkinShopOffset;
        }
        isClickShopBtn = !isClickShopBtn;
    }

    public void OnClickSettingBtn()
    {
        SoundManager.Ins.PlaySFX(EnumSoundType.SFX_CLICK_BUTTON);
        OpenPopup<Popup_Setting>();
    }

    private void SetCurrentLevelName()
    {
        List<int> lvS = LevelManager.Ins.levelStatus;
        if(lvS == null || lvS.Count == 0) return;

        for(int i = 0; i < lvS.Count; i++)
        {
            if (lvS[i] == 2)
            {
                SetText(levelNameText, levelUI_SO.leveUIDataListSO[i].levelName);
                levelIndex = i;
                currentLevelType = levelUI_SO.leveUIDataListSO[i].levelType;
                return;
            }
        }
    }

    private void ChangeCurrentLevelName(int index)
    {
        if(index >= 0 && index < levelUI_SO.leveUIDataListSO.Count)
        {
            SetText(levelNameText, levelUI_SO.leveUIDataListSO[index].levelName);
            currentLevelType = levelUI_SO.leveUIDataListSO[index].levelType;
            levelIndex = index;
            //set status button play, not now.
        }
    }
}
