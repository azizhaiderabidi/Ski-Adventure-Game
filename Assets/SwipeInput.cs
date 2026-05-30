using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class SwipeInput : MonoBehaviour
{
    public float maxDeltaX = 5f;
    public float maxDeltaY = 5f;
    public float smoothTime = 0.2f;

    private float swipeDeltaX = 0f;
    private float targetDeltaX = 0f;
    private float currentDeltaX = 0f;

    private float swipeDeltaY = 0f;
    private float targetDeltaY = 0f;
    private float currentDeltaY = 0f;
    public bool IsSwiping { get => isSwiping;  }

    private Vector2 touchStartPos;

    private float velocity = 0f;
    private bool isSwiping = false;
    private int swipeFingerId = -1;

    void Update()
    {
        swipeDeltaX = 0f;

        for (int i = 0; i < Input.touchCount; i++)
        {
            Touch touch = Input.GetTouch(i);

            // Touch began
            if (touch.phase == TouchPhase.Began)
            {
                if (EventSystem.current.IsPointerOverGameObject(touch.fingerId))
                    continue;

                if (!isSwiping)
                {
                    isSwiping = true;
                    swipeFingerId = touch.fingerId;
                    touchStartPos = touch.position;
                }
            }

            if (isSwiping && touch.fingerId == swipeFingerId)
            {
                switch (touch.phase)
                {
                    case TouchPhase.Moved:
                        Vector2 delta = touch.position - touchStartPos;

                        float rawDeltaX = delta.x / Screen.width * maxDeltaX;
                        targetDeltaX = Mathf.Clamp(rawDeltaX, -maxDeltaX, maxDeltaX);

                        touchStartPos = touch.position;

                        float rawDeltaY = delta.y / Screen.height * maxDeltaY;
                        targetDeltaY = Mathf.Clamp(rawDeltaY, -maxDeltaY, maxDeltaY);

                        touchStartPos = touch.position;

                        break;

                    case TouchPhase.Ended:
                    case TouchPhase.Canceled:
                        isSwiping = false;
                        swipeFingerId = -1;

                        targetDeltaX = 0f;
                        targetDeltaY = 0f;
                        break;
                }
            }
        }

        // Smooth interpolation X
        currentDeltaX = Mathf.SmoothDamp(currentDeltaX, targetDeltaX, ref velocity, smoothTime);
        
        // Smooth interpolation X
        currentDeltaY = Mathf.SmoothDamp(currentDeltaY, targetDeltaY, ref velocity, smoothTime);

        // Final value you can use
        swipeDeltaX = currentDeltaX;
        swipeDeltaY = currentDeltaY;
    }



    public float GetSwipeX() => swipeDeltaX;
    public float GetSwipeY() => swipeDeltaY;

}
