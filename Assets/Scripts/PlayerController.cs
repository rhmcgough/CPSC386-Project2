using UnityEngine;
using UnityEngine.InputSystem;

// Goes on Player. Moves and jumps with the Input System (PlayerInput, Send Messages).
[RequireComponent(typeof(Rigidbody2D))]
public class PlayerController : MonoBehaviour
{
    [SerializeField] float moveSpeed = 5f;
    [SerializeField] float jumpForce = 9f;
    [SerializeField] Transform groundCheck;
    [SerializeField] float groundCheckRadius = 0.2f;
    [SerializeField] LayerMask groundLayer;
    [SerializeField] Animator animator;
    [SerializeField] ParticleSystem collectBurst;
    [SerializeField] float fallLimit = -8f;

    Rigidbody2D rb;
    Vector2 moveInput;
    Vector3 spawnPosition;
    bool isGrounded;
    bool isFalling;

    void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
        spawnPosition = transform.position;
    }

    public void OnMove(InputValue value)
    {
        moveInput = value.Get<Vector2>();
    }

    public void OnJump(InputValue value)
    {
        if (value.isPressed && isGrounded)
            rb.linearVelocity = new Vector2(rb.linearVelocity.x, jumpForce);
    }

    void Update()
    {
        isGrounded = Physics2D.OverlapCircle(groundCheck.position, groundCheckRadius, groundLayer);
        
        if (animator != null)
        {
            animator.SetFloat("Speed", Mathf.Abs(moveInput.x));
            animator.SetBool("IsGrounded", isGrounded);
            animator.SetFloat("YVelocity", rb.linearVelocity.y);
        }
        if (transform.position.y < fallLimit) Respawn();
    }

    void FixedUpdate()
    {
        rb.linearVelocity = new Vector2(moveInput.x * moveSpeed, rb.linearVelocity.y);
    }

    public void PlayCollectEffect()
    {
        if (collectBurst != null) collectBurst.Play();
    }

    public void Respawn()
    {
        transform.position = spawnPosition;
        rb.linearVelocity = Vector2.zero;
    }
}
