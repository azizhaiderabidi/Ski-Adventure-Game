using UnityEngine;

public class UIButtonCommandInstaller : MonoBehaviour
{
    public PlayerUIButton accelerateButton;
    public PlayerUIButton brakeButton;
    public PlayerUIButton jumpButton;
    public PlayerUIButton flipButton;
    public PlayerUIButton backFlipButton;

    void Start()
    {
      //  SnowboarderController controller = FindObjectOfType<SnowboarderController>();

      //  accelerateButton.SetCommand(new AccelerateCommand(controller));
       // brakeButton.SetCommand(new BrakeCommand(controller));
       // jumpButton.SetCommand(new JumpCommand(controller));
       //flipButton.SetCommand(new FlipCommand(controller));
       // backFlipButton.SetCommand(new BackFlipCommand(controller));
    }

    void OnEnable()
    {
        // Undo all previously set commands
       // accelerateButton.OnRelease();
       // brakeButton.OnRelease();
       // jumpButton.SetCommand(new JumpCommand(controller));
        //flipButton.SetCommand(new FlipCommand(controller));
        //backFlipButton.SetCommand(new BackFlipCommand(controller));
    }

}
