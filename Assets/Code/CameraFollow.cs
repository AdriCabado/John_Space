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

    [Header("Clamp Settings")]
    [Tooltip("Minimum X position for the camera.")]
    public float minX = -701f;
    [Tooltip("Maximum X position for the camera.")]
    public float maxX = 705f;
    [Tooltip("Minimum Y position for the camera.")]
    public float minY = -398.4f;
    [Tooltip("Maximum Y position for the camera.")]
    public float maxY = 403;

    void LateUpdate()
    {
        if (target == null)
            return;

        // Calculate the desired position based on John's position and the offset.
        Vector3 desiredPosition = target.position + offset;
        // Smoothly interpolate between the current camera position and the desired position.
        Vector3 smoothedPosition = Vector3.Lerp(transform.position, desiredPosition, smoothSpeed);

        // Clamp the smoothed position within the defined boundaries.
        float clampedX = Mathf.Clamp(smoothedPosition.x, minX, maxX);
        float clampedY = Mathf.Clamp(smoothedPosition.y, minY, maxY);
        
        transform.position = new Vector3(clampedX, clampedY, smoothedPosition.z);
    }
}

