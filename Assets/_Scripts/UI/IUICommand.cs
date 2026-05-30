public interface IUICommand
{
    void Execute();
}

public class OpenSettingsCommand : IUICommand
{
    private UIManager _uiManager;
    private UIScreenSO setting;
    public OpenSettingsCommand(UIManager manager) => _uiManager = manager;

    public void Execute()
    {
        throw new System.NotImplementedException();
    }

    // public void Execute() => _uiManager.ShowScreen(setting);
}
