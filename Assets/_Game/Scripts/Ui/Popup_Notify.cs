using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class Popup_Notify : UiCanvas
{
    [SerializeField] Button backButton;
    [SerializeField] TMP_Text content;

    public override void SetUp()
    {
        base.SetUp();
        backButton.onClick.AddListener(() => OnClickBackButton());
    }


    public override void BackKey()
    {
        //base.BackKey();
        CloseDirectly();
        UiManager.Ins.RemoveBackUI(this);
    }

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    //void Start()
    //{

    //}

    private void OnClickBackButton()
    {
        SoundManager.Ins.PlaySFX(EnumSoundType.SFX_CLICK_BUTTON);
        BackKey();
    }

    public void OnInit(string notify)
    {
        content .text = notify;
    }
}
