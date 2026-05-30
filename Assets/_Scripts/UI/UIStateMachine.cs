public class UIStateMachine
{
    private UIScreen _current;

    public void SwitchTo(UIScreen newScreen)
    {
        _current?.Hide();
        _current = newScreen;
        _current.Show();
    }

    public void Clear()
    {
        _current?.Hide();
        _current = null;
    }

    public UIScreen CurrentScreen() => _current;
}
