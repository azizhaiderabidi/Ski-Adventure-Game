using UnityEngine;

public class TutorialListener : MonoBehaviour
{
    public bool hasMoved, hasBraked, hasJumped, hasFlipped;

    private void OnEnable()
    {
        SnowboarderController.OnMoved += HandleMoved;
        SnowboarderController.OnBraked += HandleBraked;
        SnowboarderController.OnJumped += HandleJumped;
        FlipDetector.OnFlipped += HandleFlipped;
    }

    private void OnDisable()
    {
        SnowboarderController.OnMoved -= HandleMoved;
        SnowboarderController.OnBraked -= HandleBraked;
        SnowboarderController.OnJumped -= HandleJumped;
        FlipDetector.OnFlipped -= HandleFlipped;
    }

    private void HandleMoved()
    {
        if (!hasMoved)
        {
            hasMoved = true;
            TutorialManager.instance.NextStep();
        }
    }

    private void HandleBraked()
    {
        if (!hasBraked)
        {
            hasBraked = true;
            TutorialManager.instance.NextStep();
        }
    }

    private void HandleJumped()
    {
        if (!hasJumped)
        {
            hasJumped = true;
            TutorialManager.instance.NextStep();
        }
    }

    private void HandleFlipped()
    {
        if (!hasFlipped)
        {
            hasFlipped = true;
            TutorialManager.instance.NextStep();
        }
    }
}
