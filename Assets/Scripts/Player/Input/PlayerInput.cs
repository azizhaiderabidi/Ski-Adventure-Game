using UnityEngine;

public class PlayerInput : MonoBehaviour
{
    private ICommand accelerateCommand;
    private ICommand brakeCommand;
    private ICommand jumpCommand;
    private ICommand flipCommand;
    private ICommand backFlipCommand;

    public void SetCommands(ICommand accelerate, ICommand brake, ICommand jump, ICommand flip ,ICommand backFlip)
    {
        accelerateCommand = accelerate;
        brakeCommand = brake;
        jumpCommand = jump;
        flipCommand = flip;
        backFlipCommand = backFlip;
    }

    public void OnAccelerateDown() => accelerateCommand?.Execute();
    public void OnAccelerateUp() => accelerateCommand?.Undo();

    public void OnBrakeDown() => brakeCommand?.Execute();
    public void OnBrakeUp() => brakeCommand?.Undo();

    public void OnJump() => jumpCommand?.Execute();

    public void OnFlipDown() => flipCommand?.Execute();
    public void OnBackFlipDown() => backFlipCommand?.Execute();
    public void OnFlipUp() => flipCommand?.Undo();
}
