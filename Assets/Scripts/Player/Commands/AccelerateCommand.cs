public class AccelerateCommand : ICommand
{
    private SnowboarderController controller;

    public AccelerateCommand(SnowboarderController controller)
    {
        this.controller = controller;
    }

    public void Execute() => controller.StartAccelerate();
    public void Undo() => controller.StopAccelerate();
}
