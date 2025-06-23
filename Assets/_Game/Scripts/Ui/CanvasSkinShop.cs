using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;
using System.Collections.Generic;
using static System.Net.Mime.MediaTypeNames;
using TMPro;

public class CanvasSkinShop : UiCanvas
{
    [SerializeField] Button pantBtn;
    [SerializeField] Button hatBtn;
    [SerializeField] Button skinBtn;
    [SerializeField] Button exitBtn;
    [SerializeField] Button buyBtn;
    [SerializeField] TMP_Text buyText;
    [SerializeField] TMP_Text goldText;

    [SerializeField] ButtonOnShopControl pantBtnControl;
    [SerializeField] ButtonOnShopControl hatBtnControl;
    [SerializeField] ButtonOnShopControl skinBtnControl;

    [SerializeField] ScrollRect pantScroll;
    [SerializeField] ScrollRect hatScroll;
    [SerializeField] ScrollRect skinScroll;

    [SerializeField] PantBtnSO pantBtnSO;
    [SerializeField] HatBtnSO hatBtnSO;

    [SerializeField] List<ButtonItemPantUI> pantBtnUIs;
    [SerializeField] List<ButtonItemHatUI> hatBtnUIs;
    [SerializeField] List<ButtonItemSkinUI> skinBtnUIs;

    [SerializeField] Player model_player;

    private ButtonOnShopControl currentBtnClick = null;

    private int itemPrice = 0;
    private EnumItemCategory itemType;
    private int itemIndex = -1;

    private void OnEnable()
    {
        OnClickPantBtn();
        SetText(goldText, DataManager.GetGoldOfPlayer().ToString());
        model_player.OnInit();
    }

    private void OnDestroy()
    {
        foreach(ButtonItemPantUI item in pantBtnUIs)
        {
            item.OnClick -= HandleClickPantItemBtn;
        }

        foreach (ButtonItemHatUI item in hatBtnUIs)
        {
            item.OnClick -= HandleClickHatItemBtn;
        }
    }

    public override void SetUp()
    {
        base.SetUp();
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
        OnIntitShop();
        OnClickPantBtn();
        pantBtn.onClick.AddListener(() => OnClickPantBtn());
        hatBtn.onClick.AddListener(() => OnClickHatBtn());
        skinBtn.onClick.AddListener(() => OnClickSkinBtn());
        exitBtn.onClick.AddListener(() => OnClickExitBtn());
        buyBtn.onClick.AddListener(() => OnClickBuyBtn());
    }

    public void OnIntitShop()
    {
        SpawnButtonPantUI(pantScroll.content.transform);
        SpawnButtonHatUI(hatScroll.content.transform);
    }

    private void SpawnButtonPantUI(Transform parent)
    {
        foreach(PantBtnData p in pantBtnSO.pantBtnListSO)
        {
            ButtonItemPantUI p_ui = Instantiate(pantBtnSO.pf_pantBtnUI, parent);
            p_ui.OnClick -= HandleClickPantItemBtn;
            p_ui.OnClick += HandleClickPantItemBtn;
            p_ui.OnInit(p.pantSprite, p.pantCost.ToString(), p.pantType);
            pantBtnUIs.Add(p_ui);
        }
    }

    private void SpawnButtonHatUI(Transform parent)
    {
        foreach(HatBtnData h in hatBtnSO.hatBtnListSO)
        {
            ButtonItemHatUI h_ui = Instantiate(hatBtnSO.pf_hatBtnUI, parent);
            h_ui.OnClick -= HandleClickHatItemBtn;
            h_ui.OnClick += HandleClickHatItemBtn;
            h_ui.OnInit(h.hatSprite, h.hatCost.ToString(), h.hatType);
            hatBtnUIs.Add(h_ui);
        }
    }

    private void SpawnButtonSkinUI(Transform parent)
    {
        //foreach(PantBtnData p in pantBtnSO.pantBtnListSO)
        //{
        //    ButtonItemPantUI p_ui = Instantiate(pf_pantBtnUI, parent);
        //    p_ui.OnInit(p.pantSprite, p.pantCost.ToString(), p.pantType);
        //}
    }

    private void OnClickPantBtn()
    {
        SoundManager.Ins.PlaySFX(EnumSoundType.SFX_CLICK_BUTTON);
        if (currentBtnClick == pantBtnControl) return;
        currentBtnClick = pantBtnControl;
        pantBtnControl.ActiveButton();
        hatBtnControl.DeactiveButton();
        skinBtnControl.DeactiveButton();

        pantScroll.gameObject.SetActive(true);
        hatScroll.gameObject.SetActive(false);
        skinScroll.gameObject.SetActive(false);
    }

    private void OnClickHatBtn()
    {
        SoundManager.Ins.PlaySFX(EnumSoundType.SFX_CLICK_BUTTON);
        if (currentBtnClick == hatBtnControl) return;
        currentBtnClick = hatBtnControl;
        pantBtnControl.DeactiveButton();
        hatBtnControl.ActiveButton();
        skinBtnControl.DeactiveButton();

        pantScroll.gameObject.SetActive(false);
        hatScroll.gameObject.SetActive(true);
        skinScroll.gameObject.SetActive(false);
    }

    private void OnClickSkinBtn()
    {
        SoundManager.Ins.PlaySFX(EnumSoundType.SFX_CLICK_BUTTON);
        if (currentBtnClick == skinBtnControl) return;
        currentBtnClick = skinBtnControl;
        pantBtnControl.DeactiveButton();
        hatBtnControl.DeactiveButton();
        skinBtnControl.ActiveButton();

        pantScroll.gameObject.SetActive(false);
        hatScroll.gameObject.SetActive(false);
        skinScroll.gameObject.SetActive(true);
    }

    private void OnClickExitBtn()
    {
        SoundManager.Ins.PlaySFX(EnumSoundType.SFX_CLICK_BUTTON);
        BackKey();
    }

    private void HandleClickPantItemBtn(EnumPantType type)
    {
        for(int i = 0; i < pantBtnSO.pantBtnListSO.Count; i++)
        {
            if (pantBtnSO.pantBtnListSO[i].pantType == type)
            {
                itemPrice = pantBtnSO.pantBtnListSO[i].pantCost;
                itemType = pantBtnSO.itemType;
                itemIndex = i;
                break;
            }
        }
        TestItemFoePlayer(itemType, itemIndex);
        if (DataManager.CheckWasBuy(itemType, itemIndex)) SetText(buyText, "EQUIP");
        else
        {
            SetText(buyText, "BUY");
        }
    }

    private void HandleClickHatItemBtn(EnumHatType type)
    {
        for (int i = 0; i < hatBtnSO.hatBtnListSO.Count; i++)
        {
            if (hatBtnSO.hatBtnListSO[i].hatType == type)
            {
                itemPrice = hatBtnSO.hatBtnListSO[i].hatCost;
                itemType = hatBtnSO.itemType;
                itemIndex = i;
                break;
            }
        }
        TestItemFoePlayer(itemType, itemIndex);
        if (DataManager.CheckWasBuy(itemType, itemIndex)) SetText(buyText, "EQUIP");
        else
        {
            SetText(buyText, "BUY");
        }
    }

    private void OnClickBuyBtn()
    {
        int own_gold = DataManager.GetGoldOfPlayer();
        Debug.Log($"<color=orange>{itemPrice}, {itemType}, {itemIndex}</color>");

        if(DataManager.CheckWasBuy(itemType, itemIndex))
        {
            OpenPopup<Popup_Notify>().OnInit("You have already purchased this item!!!");
            return;
        }

        if (itemPrice > own_gold)
        {
            Debug.Log("<color=red>Can not buy</color>");
            OpenPopup<Popup_Notify>().OnInit("Not enough gold");
        }
        else
        {
            string text = $"Are you sure you want to buy end equip this item for {itemPrice} gold?";
            SoundManager.Ins.PlaySFX(EnumSoundType.SFX_BUY);
            OpenPopup<Popup_Confirm>().OnInit(text, (bool confirm) =>
            {
                if (confirm)
                {
                    DataPlayer data = DataManager.LoadDataFromLocal();
                    data.gold -= itemPrice;
                    switch (itemType)
                    {
                        case EnumItemCategory.PATN:
                            data.pantStatus = DataManager.UpdateItemStatus(data.pantStatus, itemIndex);
                            break;
                        case EnumItemCategory.HAT:
                            data.hatStatus = DataManager.UpdateItemStatus(data.hatStatus, itemIndex);
                            break;
                    }
                    DataManager.SaveDataToLocal(data);
                    SetText(goldText, DataManager.GetGoldOfPlayer().ToString());
                    SetText(buyText, "EQUIPE");
                    OpenPopup<Popup_Notify>().OnInit("Buy and equip complete");
                }
            });
        }
        itemIndex = -1;
        itemPrice = -1;
        itemType = EnumItemCategory.NONE;
    }

    private void TestItemFoePlayer(EnumItemCategory type, int itemIndex)
    {
        model_player.SetItemForModel(type, itemIndex);
    }
}
