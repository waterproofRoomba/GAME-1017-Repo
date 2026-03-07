using Unity.VisualScripting;
using UnityEngine;


[RequireComponent(typeof(Rigidbody2D))]
public class PlayerController : MonoBehaviour
{
    [SerializeField] private float speed;
    [SerializeField] private float jumpForce = 8.0f;
    [SerializeField ] private LayerMask groundLayer;
    [SerializeField] private float groundCheckDistance = 40f;

    private Vector3 startPosition;
    private Rigidbody2D rb;
    private bool jumpPressed = false;
    private bool isGrounded = false;

    private void Awake()
    {
        Initialize();
    }
    private void Update()
    {
        CheckGrounded();

        if (Input.GetKeyDown(KeyCode.Space))
        {
            Debug.Log("Space pressed");
            jumpPressed = true;
        }
    }
    public void Initialize()
    {
        startPosition = transform.position;
       
        rb = GetComponent<Rigidbody2D>();
        rb.simulated = true;
    }



    private void FixedUpdate()
    {
        Vector2 velocity = rb.linearVelocity;
        velocity.x = speed;
        rb.linearVelocity = velocity;

        if (jumpPressed && isGrounded)
        {
            Jump();
            jumpPressed = false;
        }
    }

    public void ResetPlayer()
    {
        rb.linearVelocity = Vector2.zero;
        transform.position = startPosition;
        transform.rotation = Quaternion.identity;

        rb.simulated = false;
        jumpPressed = false;
    }

    private void Jump()
    {
        Vector2 velocity = rb.linearVelocity;
        velocity.y = 0;
        rb.linearVelocity = velocity;
        rb.AddForce(Vector2.up * jumpForce, ForceMode2D.Impulse);
        
    }

    public void OnJump()
    {
        Debug.Log("Jump");
        jumpPressed = true;
    }

    private void CheckGrounded()
    {
       
        RaycastHit2D hit = Physics2D.Raycast(transform.position, Vector2.down, groundCheckDistance, groundLayer);
        if (hit.collider != null)
        {
            isGrounded = true;
            Debug.Log("Ground hit: " + hit.collider.name);
        }
        else
        {
            isGrounded = false;
            Debug.Log("No ground hit");
        }
    }
}
