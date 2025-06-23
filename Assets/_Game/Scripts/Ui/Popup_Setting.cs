using System.Runtime.InteropServices.WindowsRuntime;
using UnityEngine;
using UnityEngine.UI;

public class Popup_Setting : UiCanvas
{
    [SerializeField] Slider bgMusisSlider;
    [SerializeField] Slider sfxSiler;
    [SerializeField] Button exitButton;
    [SerializeField] Button homeButton;
    [SerializeField] Button playAgainButton;

    private float bgMusicValue;
    private float sfxMusicValue;

    private bool isMaineMenu;

    private void OnEnable()
    {
        DataPlayer data = DataManager.LoadDataFromLocal();
        bgMusicValue = data.audioSetting.bgMusicValue;
        sfxMusicValue = data.audioSetting.sfxValue;
        bgMusisSlider.value = bgMusicValue;
        sfxSiler.value = sfxMusicValue;
    }

    public override void SetUp()
    {
        base.SetUp();
    }

    public override void BackKey()
    {
        //base.BackKey();
        CloseDirectly();
        DataManager.SaveAudioSetting(bgMusicValue, sfxMusicValue);
        SoundManager.Ins.SetVolume(bgMusicValue, sfxMusicValue);
        //GameManager.Ins.ContinueGame();
        UiManager.Ins.RemoveBackUI(this);

    }

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        exitButton.onClick.AddListener(() => OnClickExitButton());
        homeButton.onClick.AddListener(() => OnClickHomeButton());
        playAgainButton.onClick.AddListener(() => OnClickPlayAgainButton());

        bgMusisSlider.onValueChanged.AddListener(OnBgMusicSliderChange);
        sfxSiler.onValueChanged.AddListener(OnSfxMusicSliderChange);
    }
    
    private void OnClickExitButton()
    {
        SoundManager.Ins.PlaySFX(EnumSoundType.SFX_CLICK_BUTTON);
        if (isMaineMenu)
        {
            GameManager.Ins.ExitGame();
        }
        else
        {
            GameManager.Ins.ContinueGame();
            
            BackKey();
        }
    }

    private void OnClickHomeButton()
    {
        SoundManager.Ins.PlaySFX(EnumSoundType.SFX_CLICK_BUTTON);
        BackKey();
        LevelManager.Ins.DeSpawnCurrentLevel();
        GameManager.Ins.ContinueGame();
        UiManager.Ins.OpenUI<CanvasMainMenu>();
    }

    private void OnClickPlayAgainButton()
    {
        SoundManager.Ins.PlaySFX(EnumSoundType.SFX_CLICK_BUTTON);
        GameManager.Ins.ContinueGame();
        LevelManager.Ins.ResetCurrentLevel();
    }

    private void OnBgMusicSliderChange(float value)
    {
        bgMusicValue = value;
    }

    private  void OnSfxMusicSliderChange(float value)
    {
        sfxMusicValue = value;
    }

    public void OpenAtMainMenu()
    {
        homeButton.gameObject.SetActive(false);
        playAgainButton.gameObject.SetActive(false);
        isMaineMenu = true;
    }

    public void OpenAtGamePlay()
    {
        playAgainButton.gameObject.SetActive(true);
        homeButton.gameObject.SetActive(true);
        isMaineMenu = false;
    }
}
