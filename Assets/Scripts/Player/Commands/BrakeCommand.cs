public class BrakeCommand : ICommand
{
    private SnowboarderController controller;

    public BrakeCommand(SnowboarderController controller)
    {
        this.controller = controller;
    }

    public void Execute() => controller.StartBrake();
    public void Undo() => controller.StopBrake();
}
