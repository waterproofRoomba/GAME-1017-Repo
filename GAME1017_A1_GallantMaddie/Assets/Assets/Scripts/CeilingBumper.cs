using UnityEngine;

public class CeilingBumper : MonoBehaviour
{
    [SerializeField] private float bumpForce = 10f;

    private void OnCollisionEnter2D(Collision2D collision)
    {
        Rigidbody2D playerRigidbody2D = collision.gameObject.GetComponent<Rigidbody2D>();

        if (playerRigidbody2D == null)
        {
            return;
        }

        Vector2 forceDirection = new Vector2(0f, -bumpForce);
        playerRigidbody2D.AddForce(forceDirection, ForceMode2D.Impulse);
    }
}