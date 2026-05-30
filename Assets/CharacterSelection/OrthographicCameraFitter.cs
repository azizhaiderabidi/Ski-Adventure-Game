using UnityEngine;

[ExecuteAlways]
public class OrthographicCameraFitter : MonoBehaviour
{
    public Transform target; // Assign your character or central point here
    public Vector3 defaultOffset = Vector3.zero; // Optional offset from center
    public float designAspect = 16f / 9f; // Reference aspect ratio (e.g. 1920x1080)

    private Vector3 initialPosition;
    private float lastAspect;

    private void Start()
    {
        if (target == null) return;

        initialPosition = transform.position;
        lastAspect = GetCurrentAspect();

        AdjustCamera();
    }

    private void Update()
    {
        float currentAspect = GetCurrentAspect();

        // Automatically adjust if aspect ratio has changed (orientation or resolution)
        if (!Mathf.Approximately(currentAspect, lastAspect))
        {
            AdjustCamera();
            lastAspect = currentAspect;
        }
    }

    private void AdjustCamera()
    {
        if (target == null) return;

        float currentAspect = GetCurrentAspect();
        float aspectDifference = currentAspect / designAspect;

        // Keep base camera position
        Vector3 newPos = initialPosition + defaultOffset;

        // Horizontal adjustment (screen wider or narrower than design aspect)
        float horizontalOffset = (1f - aspectDifference) * 2f; // Adjust 2f multiplier as needed
        newPos += transform.right * horizontalOffset;

        transform.position = newPos;
    }

    private float GetCurrentAspect()
    {
        return (float)Screen.width / Screen.height;
    }

#if UNITY_EDITOR
    private void OnValidate()
    {
        if (!Application.isPlaying)
        {
            initialPosition = transform.position;
            AdjustCamera(); // Update immediately in editor when values change
        }
    }
#endif
}
