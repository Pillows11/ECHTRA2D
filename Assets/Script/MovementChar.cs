using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    public float moveSpeed = 5f;
    public float jumpForce = 7f;
    private bool isGrounded;
    private Rigidbody2D rb;
    public LayerMask groundLayer;
    public float groundCheckRadius = 0.2f;
    public Transform groundCheck;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();

        // Pengecekan apakah Rigidbody2D ada di GameObject
        if (rb == null)
        {
            Debug.LogError("No Rigidbody2D found on " + gameObject.name);
        }
    }

    void Update()
    {
        if (rb != null)
        {
            float moveInput = Input.GetAxis("Horizontal");
            rb.velocity = new Vector2(moveInput * moveSpeed, rb.velocity.y);

            isGrounded = Physics2D.OverlapCircle(groundCheck.position, groundCheckRadius, groundLayer);

            if (Input.GetKeyDown(KeyCode.W) && isGrounded)
            {
                rb.velocity = new Vector2(rb.velocity.x, jumpForce);
            }
        }
    }
}
