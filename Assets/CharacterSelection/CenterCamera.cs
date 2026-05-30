using UnityEngine;

public class CenterCamera : MonoBehaviour
{
    public Transform target; // drag your character here
    public Vector3 offset = new Vector3(0, 5, -10); // adjust as needed

    void LateUpdate()
    {
        if (target == null) return;

        // Always center the camera on the character
        transform.position = target.position + offset;
        transform.LookAt(target); // optional
    }
}
