using UnityEngine;
using TMPro;

public class GameManager : MonoBehaviour
{

  
    private static GameManager instance;
    public static GameManager Instance



    {
        get
        {
            if (instance == null)
            {
                // Check if an instance exists in the scene.
                instance = FindFirstObjectByType<GameManager>();

                // Create a GameObject with Singleton.
                if (instance == null)
                {
                    GameObject singletonObject = new GameObject(nameof(GameManager));
                    singletonObject.AddComponent<GameManager>();
                }
            }

            return instance;
        }
    }

    // Get the instance => check if its null => if null set the instance


    private void Awake()
    {
        // If an instance already exists and it's not this one, destroy this
        if (Instance != null && Instance != this)
        {
            Destroy(gameObject);
            return;
        }

        // Set the instance
        instance = this;

        // Keep this object when loading new scenes
        DontDestroyOnLoad(gameObject);



        player.SetActive(false);
    }




    private bool isDEAD = true;
    [SerializeField] private GameObject player;
    [SerializeField] private PlayerController playerController;
    [SerializeField] private TMP_Text targetText;

    

    private void Initialize()
    {
        if (player == null)
        {
            player.SetActive(true);
            transform.position = new Vector3(0f, 0f, 0f);
        }
        else if (player != null)
        {
            player.GetComponent<PlayerController>().enabled = true;      
        }

    }


    public void PlayUnpauseState() => ButtonChangeState(GameState.Start);
    public void PauseState() => ButtonChangeState(GameState.Pause);
    public void DIEState() => ButtonChangeState(GameState.DIE);

    [SerializeField] private GameState gameState;
    public void ButtonChangeState(GameState currentState)
   {
        gameState = currentState;

        switch (gameState)
        {
    case GameState.Start:
        
                if (isDEAD == true)
                {
                    player.SetActive(true);
                    Initialize();
                    Time.timeScale = 1;
                    isDEAD = false;
                    targetText.text = "Game gameing. Press Pause to pause. Press DIE to lose.";
                }
                else if (isDEAD == false)
                {
                    Time.timeScale = 1;
                    player.GetComponent<PlayerController>().enabled = true;
                    targetText.text = "Game gameing. Press Pause to pause. Press DIE to lose.";
                }
                break;

    case GameState.Pause:

                player.GetComponent<PlayerController>().enabled = false;

                Time.timeScale = 0;
                targetText.text = "Game paused. Press Play to resume.";
                break;

    case GameState.DIE:

                player.SetActive(false);
                isDEAD = true;
                Time.timeScale = 0;
                targetText.text = "Game Over. Press Play to restart.";
                break;
        }
}

public enum GameState
    {
        Start,
        Pause,
        DIE
    }

}
