// _Scripts/Events/GameFlowEvents.cs

public static class GameEvents
{
    public struct GameStart { }

    public struct MainMenuRequested { }

    public struct LevelStart
    {
        public int LevelIndex;
        public LevelStart(int levelIndex) => LevelIndex = levelIndex;
    }

    public struct LevelComplete
    {
        public int LevelIndex;
        public LevelComplete(int levelIndex) => LevelIndex = levelIndex;
    }

    public struct GamePaused { }
    public struct StartHazard { }
    public struct HazardState
    {
        public bool IsClose;
        public HazardState(bool IsClose) => this.IsClose = IsClose;

    }

    public struct StopHazard { }
    public struct GameResumed { }
    public struct FlipPerformed
    {
        public bool isBackFlip;
        public FlipPerformed(bool isBackFlip) => this.isBackFlip = isBackFlip;
    }

    public struct GameOver
    {
        public bool IsWin;
        public GameOver(bool isWin) => IsWin = isWin;
    }

    public struct RestartLevel{}
    public struct XPChanged
    {
        public int CurrentXP;
        public XPChanged(int xp) => CurrentXP = xp;
    }

    public struct SceneLoadRequested
    {
        public string SceneName;
        public SceneLoadRequested(string name) => SceneName = name;
    }

    public struct ProfileLoaded
    {
        public PlayerData Data;
        public ProfileLoaded(PlayerData data) => Data = data;
    }

    public struct ScoreAdded
    {
        public int Amount;
        public ScoreAdded(int amount) => Amount = amount;
    }

}
