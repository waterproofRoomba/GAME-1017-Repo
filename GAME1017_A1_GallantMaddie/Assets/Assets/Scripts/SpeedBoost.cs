using UnityEngine;

public class SpeedBooster : MonoBehaviour
{
    private void OnTriggerEnter2D(Collider2D other)
    {
        PlayerController player = other.GetComponent<PlayerController>();

        if (player != null)
        {
            player.SpeedBoost();
        }
    }
}