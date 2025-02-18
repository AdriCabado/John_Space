using UnityEngine;

public class AsteroidMovement : MonoBehaviour
{
    [Tooltip("Speed at which the asteroid moves.")]
    public float speed = 2f;

    private Vector3 direction;

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
    }
}
