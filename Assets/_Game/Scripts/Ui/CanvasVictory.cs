using UnityEngine;
using UnityEngine.UI;

public class CanvasVictory : UiCanvas
{
    [SerializeField] Button homeBtn;
    [SerializeField] Button playAgainBtn;
    [SerializeField] Button nextLevelBtn;

    private void OnEnable()
    {
        SoundManager.Ins.PlaySFX(EnumSoundType.SFX_VICTORY);
    }

    public override void BackKey()
    {
        base.BackKey();
        CloseDirectly();
        GameManager.ChangeState(GameState.MAINMENU);
        UiManager.Ins.RemoveBackUI(this);
    }

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        homeBtn.onClick.AddListener(() => OnClickHomeBtn());
        playAgainBtn.onClick.AddListener(() => OnClickPlayAgainBtn());
        nextLevelBtn.onClick.AddListener(() => OnClickNextLevelBtn());
    }

    private void OnClickHomeBtn()
    {
        SoundManager.Ins.PlaySFX(EnumSoundType.SFX_CLICK_BUTTON);
        BackKey();
    }

    private void OnClickPlayAgainBtn()
    {
        SoundManager.Ins.PlaySFX(EnumSoundType.SFX_CLICK_BUTTON);
        LevelManager.Ins.ResetCurrentLevel();
    }

    private void OnClickNextLevelBtn()
    {
        SoundManager.Ins.PlaySFX(EnumSoundType.SFX_CLICK_BUTTON);
        DataManager.NextLvelStatus();
        LevelManager.Ins.SetLevelStatus();
        LevelManager.Ins.SpawnLevel(LevelManager.Ins.GetLevelType());
    }
}
