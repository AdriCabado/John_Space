using UnityEngine;

[RequireComponent(typeof(Rigidbody2D))]
public class JohnMovement : MonoBehaviour
{
    [Header("Movement Settings")]
    [Tooltip("Force applied when thrusting forward.")]
    public float thrustForce = 5f;
    [Tooltip("Speed at which John rotates.")]
    public float rotationSpeed = 200f;

    private Rigidbody2D rb;
    private float rotationInput;
    private float thrustInput;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    void Update()
    {
        // Get input from the player.
        // Horizontal input: A/D or Left/Right arrows to rotate.
        rotationInput = Input.GetAxis("Horizontal");

        // Vertical input: W/S or Up/Down arrows to thrust.
        thrustInput = Input.GetAxis("Vertical");
    }

    void FixedUpdate()
    {
        // Rotate John's ship. 
        // Negative sign ensures correct rotational direction.
        rb.MoveRotation(rb.rotation - rotationInput * rotationSpeed * Time.fixedDeltaTime);

        // Apply forward thrust in the direction the ship is facing.
        Vector2 force = transform.up * thrustInput * thrustForce;
        rb.AddForce(force);
    }
}
