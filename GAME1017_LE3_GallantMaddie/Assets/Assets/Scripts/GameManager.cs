using UnityEngine;
using UnityEngine.SceneManagement;



public enum GameState
{
    InMenu,
    InGame,
    GameOver
}




public class GameManager : MonoBehaviour
{
    public static GameManager Instance;
    
    private SoundManager soundManager;
    public SoundManager SoundManager

    {
        get
        {
            if (soundManager == null)

                soundManager = FindFirstObjectByType<SoundManager>();
            return soundManager;
        }
        private set
        {
            soundManager = value;
        }
    }

   
    private SegmentSpawner segmentSpawner;
    public SegmentSpawner SegmentSpawner
    {
        get
        {
            if (segmentSpawner == null)
            {
                segmentSpawner = FindFirstObjectByType<SegmentSpawner>();
            }
            return segmentSpawner;
        }
        private set
        {
            segmentSpawner = value;
        }
    }

    private UIManager uiManager;
    public UIManager UIManager
    {
        get
        {
            if (uiManager == null)
            {
                uiManager = FindFirstObjectByType<UIManager>();
            }
            return uiManager;
        }
        private set
        {
            uiManager = value;
        }
    }

    private BackgroundManager backgroundManager;
    public BackgroundManager BackgroundManager
    {
        get
        {
            if (backgroundManager == null)
            {
                backgroundManager = FindFirstObjectByType<BackgroundManager>();
            }
            return backgroundManager;
        }
        private set
        {
            backgroundManager = value;
        }
    }

    private PlayerController player;
    public PlayerController Player
    {
        get
        {
            if (player == null)
            {
                player = FindFirstObjectByType<PlayerController>();
            }
            return player;
        }
        private set
        {
            player = value;
        }
    }

    public GameState CurrentGameState { get; private set; }



    private void Awake()
    {
        if (Instance != null && Instance != this)
        {
            Destroy(gameObject);
            return;
        }

        Instance = this;
    }


    private void Start()
    {
        
        CurrentGameState = GameState.InMenu;
        UIManager.Initialize();
    }
 

    public void GameOver()
    {
        SetGameState(GameState.GameOver);
        SceneManager.LoadScene("GameOverScene");
    }
   
    public void PlayGame()
    {
        //SceneManager.LoadScene("GameScene");
        //SetGameState(GameState.InMenu);


    }

    public void StartGame()
    {
        SetGameState(GameState.InGame);

        Player.Initialize();
        SegmentSpawner.Initialize();

        UIManager.OnPlayPressed();
        //Start Timer
    }

    [ContextMenu("Restart Game")]
    public void RestartGame()
    {
        Player.ResetPlayer();
        segmentSpawner.Reset();
        SetGameState(GameState.InMenu);
    }
private void SetGameState(GameState state)
    { 
        CurrentGameState = state;
    }
}
