using UnityEngine;

public class CameraFollow : MonoBehaviour
{
    [Header("Target Settings")]
    [Tooltip("Assign John's transform here.")]
    public Transform target;
    
    [Header("Camera Settings")]
    [Tooltip("Offset from the target's position (default is -10 on Z to keep the camera back).")]
    public Vector3 offset = new Vector3(0, 0, -10);
    [Tooltip("Smoothing factor for the camera movement.")]
    public float smoothSpeed = 0.125f;

    void LateUpdate()
    {
        // Ensure we have a target assigned
        if (target == null)
            return;

        // Calculate the desired position based on John's position and the offset.
        Vector3 desiredPosition = target.position + offset;
        // Smoothly interpolate between the current camera position and the desired position.
        Vector3 smoothedPosition = Vector3.Lerp(transform.position, desiredPosition, smoothSpeed);
        transform.position = smoothedPosition;
    }
}
