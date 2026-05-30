public class BackFlipCommand : ICommand
{
    private SnowboarderController controller;

    public BackFlipCommand(SnowboarderController controller)
    {
        this.controller = controller;
    }

    public void Execute() => controller.StartBackFlip();
    public void Undo() => controller.StopBackFlip();
}
