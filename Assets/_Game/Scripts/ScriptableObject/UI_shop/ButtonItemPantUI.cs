using UnityEngine;
using System.Collections.Generic;
using System;

public class ButtonItemPantUI : ButtonItemShopUI
{
    [SerializeField] EnumPantType pantType;

    public Action<EnumPantType> OnClick;
    private void Start()
    {
        itemBtn.onClick.AddListener(() => OnClickPantItemBtn());
    }

    public void OnInit(Sprite sp, string price, EnumPantType type)
    {
        base.OnInit(sp, price);
        this.pantType = type;
        this.itemType = EnumItemCategory.PATN;
    }

    private void OnClickPantItemBtn()
    {
        Debug.Log("<color=red>click pant item btn</color>");
        OnClick?.Invoke(pantType);
    }
}
