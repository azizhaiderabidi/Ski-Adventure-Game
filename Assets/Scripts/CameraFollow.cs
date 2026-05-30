using UnityEngine;

public class CameraFollow : MonoBehaviour
{
    public Transform target;  // The target to follow (the snowboarder)
    public Vector3 offset;    // Offset from the target (camera position relative to the target)
    public float smoothSpeed = 0.125f;  // Speed of camera movement (smoothing effect)
    public float ySmoothSpeed = 0.02f;  // Speed for Y-axis smoothing (set lower for better effect)

    void LateUpdate()
    {
        if (target != null)
        {
            Vector3 desiredPosition = target.position + offset;

            // X & Z ko smoothly lerp karna
            float smoothX = Mathf.Lerp(transform.position.x, desiredPosition.x, smoothSpeed);
            float smoothZ = Mathf.Lerp(transform.position.z, desiredPosition.z, smoothSpeed);

            // Y position ko separately smooth karna
            float smoothY = Mathf.Lerp(transform.position.y, desiredPosition.y, ySmoothSpeed);

            // Final position set karna
            //  transform.position = new Vector3(smoothX, smoothY, smoothZ);
            transform.position = desiredPosition;

            
        }
    }
}
