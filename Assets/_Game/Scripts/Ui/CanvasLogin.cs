using System.IO;
using UnityEngine;
using UnityEngine.UI;
//using static System.Net.Mime.MediaTypeNames;

public class CanvasLogin : UiCanvas
{
    [SerializeField] Button loginButton;
    [SerializeField] Button registerButton;
    [SerializeField] Button guestButton;
    [SerializeField] Button continueButton;

    //public override void SetUp()
    //{
    //    base.SetUp();
        
    //}

    //Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        loginButton.onClick.AddListener(() => ClickLoginButton());
        registerButton.onClick.AddListener(() => ClickRegisterButton());
        guestButton.onClick.AddListener(() => ClickGuestButton());
        continueButton.onClick.AddListener(() => ClickContinueButton());
    }

    public void ClickLoginButton()
    {
        //login logic
    }

    public void ClickRegisterButton()
    {
        //register logic
    }

    private bool CheckHaveData()
    {
        if (File.Exists(DataManager.destinationParth)) return true;
        return false;
    }

    public void ClickGuestButton()
    {
        SoundManager.Ins.PlaySFX(EnumSoundType.SFX_CLICK_BUTTON);
        //Debug.Log("Click button guest");
        string text = CheckHaveData()? text = Constant.CONFIRM_GUEST_HAVEDATA: Constant.CONFIRM_GUEST_NODATA;

        OpenPopup<Popup_Confirm>().OnInit(text, (bool confirm) =>
        {
            if (confirm)
            {
                DataPlayer data = DataManager.InitDefaultData();
                DataManager.SaveDataToLocal(data);
                CloseDirectly();
                UiManager.Ins.OpenUI<CanvasMainMenu>();
            }
        });
    }

    public void ClickContinueButton()
    {
        SoundManager.Ins.PlaySFX(EnumSoundType.SFX_CLICK_BUTTON);
        CloseDirectly();
        UiManager.Ins.OpenUI<CanvasMainMenu>();
    }
}
