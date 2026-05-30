using UnityEngine;

public class PlayerInputHandler : MonoBehaviour
{
    private ICommand accelerateCommand;
    private ICommand brakeCommand;
    private ICommand jumpCommand;
    private ICommand flipCommand;

    void Start()
    {
        var controller = FindObjectOfType<SnowboarderController>();

        accelerateCommand = new AccelerateCommand(controller);
        brakeCommand = new BrakeCommand(controller);
        jumpCommand = new JumpCommand(controller);
        flipCommand = new FlipCommand(controller);
    }

    void Update()
    {
        // Accelerate
        if (Input.GetKeyDown(KeyCode.W))
            accelerateCommand.Execute();
        if (Input.GetKeyUp(KeyCode.W))
            accelerateCommand.Undo();

        // Brake
        if (Input.GetKeyDown(KeyCode.S))
            brakeCommand.Execute();
        if (Input.GetKeyUp(KeyCode.S))
            brakeCommand.Undo();

        // Jump
        if (Input.GetKeyDown(KeyCode.Space))
            jumpCommand.Execute();

        // Flip
        if (Input.GetKeyDown(KeyCode.F))
            flipCommand.Execute();
        if (Input.GetKeyUp(KeyCode.F))
            flipCommand.Undo();
    }
}
