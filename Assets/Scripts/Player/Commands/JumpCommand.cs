public class JumpCommand : ICommand
{
    private SnowboarderController controller;

    public JumpCommand(SnowboarderController controller)
    {
        this.controller = controller;
    }

    public void Execute() => controller.TryJump();
    public void Undo() { }
}
