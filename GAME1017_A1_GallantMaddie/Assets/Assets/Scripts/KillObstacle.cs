using UnityEngine;
using UnityEngine.SceneManagement;

public class KillObstacle : MonoBehaviour
{
    [SerializeField] private string GameOverScene;
    private void OnTriggerEnter2D(Collider2D other)
    {
        PlayerController player = other.GetComponent<PlayerController>();

        if (other.CompareTag("Player"))
        {
            GameManager.Instance.GameOver();
        }
    }
}