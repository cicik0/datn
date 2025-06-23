using System.Collections.Generic;
using System.Linq;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class CanvasWeaponShop : UiCanvas
{
    [SerializeField] WeaponSO wpSO;
    [SerializeField] Button nextBtn;
    [SerializeField] Button prevBtn;
    [SerializeField] Button buyBtn;
    [SerializeField] Button exitBtn;
    [SerializeField] TMP_Text goldText;
    [SerializeField] TMP_Text buyText;
    [SerializeField] TMP_Text wpPriceText;
    [SerializeField] Transform wpViewTF;

    [SerializeField] private int wpIndex = -1;
    private int wpPrice = -1;
    private EnumWpType wpType = EnumWpType.NONE;
    private List<GameObject> wpList = new List<GameObject>();

    public override void SetUp()
    {
        base.SetUp();

    }

    private void OnEnable()
    {
        OnInitWpShop();
    }

    public override void BackKey()
    {
        base.BackKey();
        CloseDirectly();
        UiManager.Ins.OpenUI<CanvasMainMenu>();
        UiManager.Ins.RemoveBackUI(this);
    }

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        //OnInitWpShop();
        nextBtn.onClick.AddListener(() => OnClickNextBtn());
        prevBtn.onClick.AddListener(() => OnClickPrevBtn());
        buyBtn.onClick.AddListener(() => OnClickBuyBtn());
        exitBtn.onClick.AddListener(() => OnClickExitBtn());
    }

    private void OnClickNextBtn()
    {
        SoundManager.Ins.PlaySFX(EnumSoundType.SFX_CLICK_BUTTON);
        int index = wpIndex;
        index++;
        if(index >=0 && index < wpSO.wpListSO.Length)
        {
            wpList[wpIndex].SetActive(false);
            wpIndex++;
            wpList[wpIndex].SetActive(true);

            SetTextForBuyBtn();     
        }
    }
    
    private void OnClickPrevBtn()
    {
        SoundManager.Ins.PlaySFX(EnumSoundType.SFX_CLICK_BUTTON);
        int index = wpIndex;
        index--;
        if (index >= 0 && index < wpSO.wpListSO.Length)
        {
            wpList[wpIndex].SetActive(false);
            wpIndex--;
            wpList[wpIndex].SetActive(true);

            SetTextForBuyBtn();
        }
    }

    private void OnClickBuyBtn()
    {
        //Debug.Log($"<color=red>{wpIndex}, {wpPrice}, {DataManager.CheckWasBuy(EnumItemCategory.WP, wpIndex)}</color>");
        if(wpPrice > DataManager.GetGoldOfPlayer() && !DataManager.CheckWasBuy(EnumItemCategory.WP, wpIndex))
        {
            OpenPopup<Popup_Notify>().OnInit("Not enougt gold");
            return;
        }
        else
        {
            SoundManager.Ins.PlaySFX(EnumSoundType.SFX_BUY);
            string text = $"Are you sure you want to buy end equip this weapon with {wpPrice} gold?";
            OpenPopup<Popup_Confirm>().OnInit(text, (bool confirm) =>
            {
                if (confirm)
                {
                    DataPlayer data = DataManager.LoadDataFromLocal();
                    data.gold -= wpPrice;
                    DataManager.UpdateItemStatus(data.wpStatus, wpIndex);
                    DataManager.SaveDataToLocal(data);
                    SetText(buyText, "EQUIPED");
                    SetText(goldText, DataManager.GetGoldOfPlayer().ToString());
                    OpenPopup<Popup_Notify>().OnInit("Buy and equip complete");
                }
            });
        }
    }

    private void OnClickExitBtn()
    {
        SoundManager.Ins.PlaySFX(EnumSoundType.SFX_CLICK_BUTTON);
        BackKey();
    }

    private void SetCurerntWpIndex()
    {
        wpIndex = DataManager.GetIndexCurrentWp();
    }

    private void OnInitWpShop()
    {
        SetText(goldText, DataManager.GetGoldOfPlayer().ToString());
        SetCurerntWpIndex();
        for(int i = 0; i < wpSO.wpListSO.Length; i++)
        {
            GameObject w = Instantiate(wpSO.wpListSO[i].wpPrefab, wpViewTF);
            wpList.Add(w);
            if (i != wpIndex) w.SetActive(false); 
        }
        SetTextForBuyBtn();
    }

    private void SetTextForBuyBtn()
    {
        SetText(wpPriceText, wpSO.wpListSO[wpIndex].wpPrice.ToString());
        wpPrice = wpSO.wpListSO[wpIndex].wpPrice;

        if (DataManager.CheckWasBuy(EnumItemCategory.WP, wpIndex))
        {
            SetText(buyText, "EQUIP");
        }
        else if (DataManager.CheckWasEquiped(EnumItemCategory.WP, wpIndex))
        {
            SetText(buyText, "EQUIPED");
        }
        else
        {
            SetText(buyText, "BUY");
        }
    }
}
