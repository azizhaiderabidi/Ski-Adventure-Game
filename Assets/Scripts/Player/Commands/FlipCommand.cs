public class FlipCommand : ICommand
{
    private SnowboarderController controller;

    public FlipCommand(SnowboarderController controller)
    {
        this.controller = controller;
    }

    public void Execute() => controller.StartFlip();
    public void Undo() => controller.StopFlip();
}
