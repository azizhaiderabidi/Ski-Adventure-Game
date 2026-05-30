using System;
using UnityEngine;

public class FlipDetector : MonoBehaviour
{
    private float accumulatedRotation = 0f;
    private Vector3 lastForward;
    private SnowboarderController controller;
    public static event Action OnFlipped;
    void Start()
    {
        controller = GetComponent<SnowboarderController>();
        lastForward = transform.forward;
    }

    void Update()
    {
        // Optional: Skip detecting flips while grounded
        if (controller.IsGrounded) return;

        Vector3 currentForward = transform.forward;
        float angle = Vector3.SignedAngle(lastForward, currentForward, transform.right); // flip around local X

        accumulatedRotation += angle;
        lastForward = currentForward;

        // Flip Detected
        if (accumulatedRotation >= 180f)
        {
            Debug.Log("Backflip completed!");
            EventManager.Raise(new GameEvents.FlipPerformed(true));
            accumulatedRotation = 0f;
        }
        else if (accumulatedRotation <= -180f)
        {
            Debug.Log("Frontflip completed!");
            EventManager.Raise(new GameEvents.FlipPerformed(false));
            if(controller.isTutorialRunning)
            {
                OnFlipped?.Invoke();
            }
            
            accumulatedRotation = 0f;
        }

        // Optional: Reset if landed
        if (controller.IsGrounded && Mathf.Abs(accumulatedRotation) > 0f)
        {
            accumulatedRotation = 0f;
            lastForward = transform.forward;
        }
    }
}
