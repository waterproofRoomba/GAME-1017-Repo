using Unity.VisualScripting;
using UnityEngine;


[RequireComponent(typeof(Rigidbody2D))]
public class PlayerController : MonoBehaviour
{
    [SerializeField] public float speed;
    [SerializeField] public float maxSpeed;
    [SerializeField] private float jumpForce = 8.0f;
    [SerializeField] private LayerMask groundLayer;
    [SerializeField] private float groundCheckDistance = 40f;

    [SerializeField] private float targetSpeed = 300f;
    [SerializeField] private float acceleration = 100f;


    private Vector3 startPosition;
    private Rigidbody2D rb;
    private bool jumpPressed = false;
    private bool isGrounded = false;

    [SerializeField] private Animator animator;

    //added a double jump since i added obstacles that can kill the player, and they're kind of hard to avoid
    [SerializeField] private int maxJumps = 2;

    private int jumpsRemaining;
    private bool wasGrounded;
    private void Awake()
    {
        Initialize();
    }
    private void Update()
    {
        CheckGrounded();

        // Reset jumps on landing
        if (isGrounded && !wasGrounded)
        {
            jumpsRemaining = maxJumps;
        }

        wasGrounded = isGrounded;

        if (Input.GetKeyDown(KeyCode.Space))
        {
            jumpPressed = true;
        }

        if (boosting && Time.time > boostEndTime)
        {
            boosting = false;
            targetSpeed = originalTargetSpeed;
            nextAllowedBoostTime = Time.time + boostDelayTime;
            //animation for boost
            if (animator != null)
            {
                animator.SetBool("isBoosting", false);
            }
        }
    }
    public void Initialize()
    {
        startPosition = transform.position;
        originalTargetSpeed = targetSpeed;
        rb = GetComponent<Rigidbody2D>();
        rb.simulated = true;
        jumpsRemaining = maxJumps;

        if (animator == null)
        {
            animator = GetComponent<Animator>();
        }

        if (animator != null)
        {
            animator.SetBool("isBoosting", false);
        }
    }



    //i changed the speed from force because the bumpers would ruin momentum. so i had to add acceleration + max speed, which will work for difficulty.

    private void FixedUpdate()
    {
        Vector2 velocity = rb.linearVelocity;

        velocity.x = Mathf.MoveTowards(velocity.x, targetSpeed, acceleration * Time.fixedDeltaTime);
        rb.linearVelocity = velocity;

        //now checks number of jumps left in "bank" (kind of works like charges lol)
        if (jumpPressed && jumpsRemaining > 0)
        {
            Jump();
            jumpsRemaining--;
        }

        jumpPressed = false;
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

    //to do current lives of player. update UI. if lives after collision is 0, end game
    private void OnTriggerEnter2D(Collider2D collision)
    {
        // if (collision.CompareTag("Obstacle"))
        // {
        //GameManager.Instance.GameOver();
        //}
     // if (collision.CompareTag("Booster"))
       // {



           // SpeedBoost();
       // }
        if (collision.GetComponent<Obstacle>())
        {
            GameManager.Instance.GameOver();
        }
        //check if the colliding trigger is
        //when collision with obstacles occurs end the game
       
    }

   


    public float boostTime = 2f;
    public float boostDelayTime = 0f;
    public float boostedSpeed = 10f;

    private float boostEndTime;
    private float nextAllowedBoostTime;
    private bool boosting;


    private float originalTargetSpeed;





    //it adjusts the speed in the player controller script temporarily
    public void SpeedBoost()
    {
        if (Time.time < nextAllowedBoostTime) return;

        boosting = true;
        boostEndTime = Time.time + boostTime;
        targetSpeed = boostedSpeed;
        //added animation for boost
        if (animator != null)
        {
            animator.SetBool("isBoosting", true);
        }

        Vector2 velocity = rb.linearVelocity;
        velocity.x = boostedSpeed;
        rb.linearVelocity = velocity;
    }
}
