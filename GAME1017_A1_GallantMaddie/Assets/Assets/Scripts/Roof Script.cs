using UnityEngine;

public class FollowPlayerRoof : MonoBehaviour
{
    public Transform playerTransform;
    //this adds a roof that follows the player
    void LateUpdate()
    {
        if (playerTransform == null)
        {
            return;
        }

        Vector3 currentPosition = transform.position;

        currentPosition.x = playerTransform.position.x;

        transform.position = currentPosition;
    }
}