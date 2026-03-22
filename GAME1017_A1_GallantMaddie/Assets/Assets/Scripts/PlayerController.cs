using Unity.VisualScripting;
using UnityEngine;


[RequireComponent(typeof(Rigidbody2D))]
public class PlayerController : MonoBehaviour
{
    [SerializeField] public float speed;
    [SerializeField] private float jumpForce = 8.0f;
    [SerializeField] private LayerMask groundLayer;
    [SerializeField] private float groundCheckDistance = 40f;

    private Vector3 startPosition;
    private Rigidbody2D rb;
    private bool jumpPressed = false;
    private bool isGrounded = false;
    [SerializeField] float lowerYLimit = 50f;
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
        CheckLowerYLimit();





        if (boosting && Time.time > boostEndTime)
        {
            boosting = false;
            speed = originalSpeed;
            nextAllowedBoostTime = Time.time + boostDelayTime;
        }
    }
    public void Initialize()
    {
        startPosition = transform.position;
        originalSpeed = speed;
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

    private void CheckLowerYLimit()
    {
        if (transform.position.y < lowerYLimit)

        {
            GameManager.Instance.GameOver();
        }
    }


    public float boostTime = 2f;
    public float boostDelayTime = 5f;
    public float boostedSpeed = 10f;

    private float boostEndTime;
    private float nextAllowedBoostTime;
    private bool boosting;

    
    private float originalSpeed;

    
    
    
   
    //it adjusts the speed in the player controller script temporarily
    public void SpeedBoost()
    {
        if (Time.time < nextAllowedBoostTime) return;

        boosting = true;
        boostEndTime = Time.time + boostTime;
        speed = boostedSpeed;
    }
}
