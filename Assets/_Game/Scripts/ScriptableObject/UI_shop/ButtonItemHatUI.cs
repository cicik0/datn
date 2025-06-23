using UnityEngine;
using UnityEngine.UI;
using System;

public class ButtonItemHatUI : ButtonItemShopUI
{
    [SerializeField] EnumHatType hatType;

    public Action<EnumHatType> OnClick;

    private void Start()
    {
        itemBtn.onClick.AddListener(() => OnClickHatItemBtn());
    }

    public void OnInit(Sprite sp, string price, EnumHatType type)
    {
        base.OnInit(sp, price);
        this.hatType = type;
        this.itemType = EnumItemCategory.HAT;
    }

    private void OnClickHatItemBtn()
    {
        OnClick?.Invoke(hatType);
    }
}
