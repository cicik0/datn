using UnityEngine;

public enum GameState{LOGIN, MAINMENU, WIN, LOSE, GAMEPLAY }

public class GameManager : Singleton<GameManager>
{
    private static GameState gameState;

    public static void ChangeState(GameState newState )
    {
        gameState = newState;
        UiManager.Ins.ClearBackKey();
        HandleChangeState();
    }

    public static bool IsState(GameState state) => gameState == state;

    private void Awake()
    {
        Application.targetFrameRate = 60;
    }

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        SoundManager.Ins.PlayBGMusic(EnumSoundType.BG_MUSIC_MAIN);
        ChangeState(GameState.LOGIN);
    }

    private static void HandleChangeState()
    {
        switch (gameState)
        {
            case GameState.LOGIN:
                HandleLoginState();
                break;
            case GameState.GAMEPLAY:
                HandleGameplayState();
                break;
            case GameState.MAINMENU:
                HandleMainState();
                break;
            case GameState.WIN:
                HandleWinState();
                break;
            case GameState.LOSE:
                HandleLoseState();
                break;
        }
    }
    private static void HandleMainState()
    {
        UiManager.Ins.CloseAll();
        UiManager.Ins.OpenUI<CanvasMainMenu>();
    }

    private static void HandleLoginState()
    {
        UiManager.Ins.CloseAll();
        UiManager.Ins.OpenUI<CanvasLogin>();
    }

    private static void HandleLoseState()
    {
        UiManager.Ins.CloseUI<CanvasGameplay>();
        UiManager.Ins.OpenUI<CanvasLose>();
    }

    private static void HandleWinState()
    {
        UiManager.Ins.CloseAll();
        UiManager.Ins.OpenUI<CanvasVictory>();
    }

    private static void HandleGameplayState()
    {
        UiManager.Ins.CloseAll();
        UiManager.Ins.OpenUI<CanvasGameplay>();
    }
    

    public void ExitGame()
    {
        #if UNITY_EDITOR
            UnityEditor.EditorApplication.isPlaying = false;
        #else
            Application.Quit();
        #endif
    }

    public void StopGame()
    {
        Time.timeScale = 0;
    }

    public void ContinueGame()
    {
        Time.timeScale = 1f;
    }

}
