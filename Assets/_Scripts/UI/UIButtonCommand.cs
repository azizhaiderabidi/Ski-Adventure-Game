using UnityEngine;

public class UIButtonCommand : MonoBehaviour
{
    private IUICommand _command;

    public void SetCommand(IUICommand command) => _command = command;

    public void OnClick() => _command?.Execute();
}
