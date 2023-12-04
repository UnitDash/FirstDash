using UnityEngine;

public class player : MonoBehaviour
{
    public float runSpeed = 5f;         // Speed when running normally
    public float speedRunMultiplier = 2f; // Multiplier for speed run
    public float jumpForce = 10f;       // Force applied when jumping
    public float slideForce = 8f;       // Force applied when sliding

    private bool isGrounded;            // Check if player is grounded
    private Rigidbody rb;
    private bool isSpeedRunning;        // Check if player is speed running

    void Start()
    {
        rb = GetComponent<Rigidbody>();
    }

    void Update()
    {
        // Check if the player is grounded (modify this based on your ground detection logic)
        isGrounded = Physics2D.Raycast(transform.position, Vector2.down, 0.1f);

        // Running
        float move = Input.GetAxis("Horizontal");
        rb.velocity = new Vector2(move * runSpeed * (isSpeedRunning ? speedRunMultiplier : 1f), rb.velocity.y);

        // Jumping
        if (Input.GetKeyDown(KeyCode.Space) && isGrounded)
        {
            rb.velocity = new Vector2(rb.velocity.x, jumpForce);
        }

        // Speed Run (Ctrl)
        if (Input.GetKey(KeyCode.LeftControl))
        {
            isSpeedRunning = true;
        }
        else
        {
            isSpeedRunning = false;
        }

        // Sliding
        if (Input.GetKeyDown(KeyCode.LeftControl) && isGrounded)
        {
            Slide();
        }
    }

    void Slide()
    {
        Vector2 slideForceVector = new Vector2(rb.velocity.x * slideForce, rb.velocity.y);
        rb.AddForce(slideForceVector, ForceMode.Impulse);
    }
}
