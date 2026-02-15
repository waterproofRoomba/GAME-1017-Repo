using UnityEngine;

public class PlayerController : MonoBehaviour
{

    public float moveSpeed = 3f;
    float speedX, speedY;
    Rigidbody2D rb;
     

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    void Update()
    {
        speedX = Input.GetAxisRaw("Horizontal") * moveSpeed;
        speedY = Input.GetAxisRaw("Vertical") * moveSpeed;
        rb.linearVelocity = new Vector2(speedX, speedY);
    }
}
