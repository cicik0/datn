using UnityEngine;
using UnityEngine.UI;

public class CanvasLose : UiCanvas
{
    [SerializeField] Button homeButton;
    [SerializeField] Button playAgainButton;

    private void OnEnable()
    {
        SoundManager.Ins.PlaySFX(EnumSoundType.SFX_LOSE);
    }

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        homeButton.onClick.AddListener(() => OnClickHomeButton());
        playAgainButton.onClick.AddListener(() => OnClickPlayAgainButton());
    }

    public override void BackKey()
    {
        //base.BackKey();
        //CloseDirectly();
        //UiManager.Ins.OpenUI<CanvasMainMenu>();
        GameManager.ChangeState(GameState.MAINMENU);
        UiManager.Ins.RemoveBackUI(this);
    }

    private void OnClickHomeButton()
    {
        SoundManager.Ins.PlaySFX(EnumSoundType.SFX_CLICK_BUTTON);
        LevelManager.Ins.DeSpawnCurrentLevel();
        BackKey();
    }

    private void OnClickPlayAgainButton()
    {
        SoundManager.Ins.PlaySFX(EnumSoundType.SFX_CLICK_BUTTON);
        LevelManager.Ins.ResetCurrentLevel();
        GameManager.ChangeState(GameState.GAMEPLAY);
    }
}
