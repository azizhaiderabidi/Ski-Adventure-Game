public interface ICommand
{
    void Execute();
    void Undo(); // Optional if needed
}
