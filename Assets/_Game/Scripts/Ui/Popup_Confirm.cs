using System;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class Popup_Confirm : UiCanvas
{
    [SerializeField] Button yesBtn;
    [SerializeField] Button noBtn;
    [SerializeField] TMP_Text contentText;

    public Action<bool> OnConfirm;

    public override void SetUp()
    {
        base.SetUp();
        yesBtn.onClick.AddListener(() => OnClickYesBtn());
        noBtn.onClick.AddListener(() => OnClickNoBtn());
    }

    public void OnInit(string content, Action<bool> conFirmCB)
    {
        //Debug.Log("fuck");
        contentText.text = content;
        OnConfirm = conFirmCB;
    }

    public override void BackKey()
    {
        //base.BackKey();
        CloseDirectly();
        UiManager.Ins.RemoveBackUI(this);
    }

    private void OnClickYesBtn()
    {
        SoundManager.Ins.PlaySFX(EnumSoundType.SFX_CLICK_BUTTON);
        OnConfirm?.Invoke(true);
        BackKey();
    }

    private void OnClickNoBtn()
    {
        SoundManager.Ins.PlaySFX(EnumSoundType.SFX_CLICK_BUTTON);
        BackKey();
    }
}
