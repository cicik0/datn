using DG.Tweening;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class CanvasGameplay : UiCanvas
{
    [SerializeField] FloatingJoystick floatJT;
    [SerializeField] Button settingBtn;
    [SerializeField] public TMP_Text pointText;

    public override void SetUp()
    {
        base.SetUp();
        LevelManager.Ins.SetCanvasGamePlay(this);
    }

    public override void BackKey()
    {
        base.BackKey();
    }

    private void OnEnable()
    {
        SoundManager.Ins.PlayBGMusic(EnumSoundType.BG_MUSIC_GAMEPLAY);
        LevelManager.Ins.SetFloatJoystick(floatJT);
   }

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        settingBtn.onClick.AddListener(() => OnClickSettingBtn());
    }

    private void OnClickSettingBtn()
    {
        SoundManager.Ins.PlaySFX(EnumSoundType.SFX_CLICK_BUTTON);
        GameManager.Ins.StopGame();
        OpenPopup<Popup_Setting>();
    }
}
