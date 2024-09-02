using UnityEngine;

public class TerrariaStyleMovement : MonoBehaviour
{
    public float moveSpeed = 5f;          // Kecepatan gerakan horizontal
    public float jumpForce = 20f;         // Kekuatan lompat
    public float gravityScale = 3f;       // Skala gravitasi
    public LayerMask groundLayer;         // Layer tanah
    public Transform groundCheck;         // Transform untuk pengecekan tanah
    public float groundCheckRadius = 0.2f;// Radius pengecekan tanah

    private Rigidbody2D rb;
    private bool isGrounded;
    private float moveInput;
    private bool canDoubleJump;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        rb.gravityScale = gravityScale;

        rb.freezeRotation = true;
    }

    void Update()
    {
        moveInput = Input.GetAxisRaw("Horizontal");  // Input horizontal

        // Cek apakah karakter di tanah
        isGrounded = Physics2D.OverlapCircle(groundCheck.position, groundCheckRadius, groundLayer);

        if (isGrounded)
        {
            canDoubleJump = true;  // Reset kemampuan double jump saat karakter menyentuh tanah
        }

        // Lompat
        if (Input.GetButtonDown("Jump"))
        {
            if (isGrounded)
            {
                rb.velocity = new Vector2(rb.velocity.x, jumpForce);
            }
            else if (canDoubleJump)
            {
                rb.velocity = new Vector2(rb.velocity.x, jumpForce);
                canDoubleJump = false;
            }
        }
    }

    void FixedUpdate()
    {
        // Gerakan horizontal dengan smooth
        rb.velocity = new Vector2(moveInput * moveSpeed, rb.velocity.y);
    }

    void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(groundCheck.position, groundCheckRadius);
    }
}
