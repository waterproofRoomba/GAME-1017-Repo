using UnityEngine;
using UnityEngine.SceneManagement;

public class KillFloorLoadScene : MonoBehaviour
{
    [SerializeField] private string GameOverScene;

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            SceneManager.LoadScene(GameOverScene);
        }
    }
}