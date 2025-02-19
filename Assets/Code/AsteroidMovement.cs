using UnityEngine;

public class AsteroidMovement : MonoBehaviour
{
    [Tooltip("Speed at which the asteroid moves.")]
    public float speed = 2f;

    [Tooltip("Rotation speed range (degrees per second). A random value will be chosen between these limits.")]
    public float minRotationSpeed = -90f;
    public float maxRotationSpeed = 90f;
    
    private float rotationSpeed;
    private Vector3 direction;

    void Start()
    {
        // Set a random rotation speed within the defined range.
        rotationSpeed = Random.Range(minRotationSpeed, maxRotationSpeed);
    }

    /// <summary>
    /// Call this to set the target (John's position) for the asteroid.
    /// </summary>
    public void SetTarget(Vector3 target)
    {
        // Calculate a normalized direction vector from the asteroid to the target.
        direction = (target - transform.position).normalized;

        // Optionally, rotate the asteroid to face the target.
        float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;
        transform.rotation = Quaternion.Euler(new Vector3(0, 0, angle - 90f));
    }

    void Update()
    {
        // Move the asteroid toward the target every frame.
        transform.position += direction * speed * Time.deltaTime;
        
        // Rotate the asteroid around its Z-axis.
        transform.Rotate(0, 0, rotationSpeed * Time.deltaTime);
    }
}
