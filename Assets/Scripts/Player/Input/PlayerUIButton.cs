using UnityEngine;

public class PlayerUIButton : MonoBehaviour
{
    private ICommand command;

    public bool IsAccelerate;
    public void SetCommand(ICommand newCommand)
    {
        command = newCommand;
    }

    // Called by EventTrigger PointerDown
    public void OnPress()
    {
       // command?.Execute();
       if(IsAccelerate)
       {
            GameManager.Instance.player.GetComponent<SnowboarderController>().StartAccelerate();
       }
       else
        {
            GameManager.Instance.player.GetComponent<SnowboarderController>().StartBrake();

        }

    }

    // Called by EventTrigger PointerUp
    public void OnRelease()
    {
        // command?.Undo();
        if (IsAccelerate)
        {
            GameManager.Instance.player.GetComponent<SnowboarderController>()?.StopAccelerate();
        }
        else
        {
            GameManager.Instance.player.GetComponent<SnowboarderController>()?.StopBrake();

        }

    }

    // Optional for tap-style commands (e.g. Jump)
    public void OnClick()
    {
        command?.Execute();
    }
}
