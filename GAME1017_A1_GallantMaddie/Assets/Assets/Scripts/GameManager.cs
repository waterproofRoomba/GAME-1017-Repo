using System.Collections.Generic;
using System.Threading;
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

    private SaveSystem saveSystem;
    public SaveSystem SaveSystem
    {
        get
        {
            if (saveSystem == null)
            {
                saveSystem = FindFirstObjectByType<SaveSystem>();
            }
            return saveSystem;
        }
        private set
        {
            saveSystem = value;
        }
    }



    public GameState CurrentGameState { get; private set; }

    
    public Leaderboard Leaderboard => FindFirstObjectByType<Leaderboard>();
    private void Awake()
    {
        if (Instance != null && Instance != this)
        {
            Destroy(gameObject);
            return;
        }

        Instance = this;

        
    }

    public void OnDisable()
    {
        SceneManager.sceneLoaded -= OnSceneLoaded;
    }

    private void OnEnable()
    {
        SceneManager.sceneLoaded += OnSceneLoaded;
    }

    private void OnSceneLoaded( Scene scene, LoadSceneMode mod)
    {
        if (scene == SceneManager.GetSceneByBuildIndex(2))
        {
            GameOverSceneStart();
        }
        Debug.Log(scene.name);
    }

    public void GameOver()
    {
        if (UIManager.Instance == null)
        {
            Debug.LogError("UIManager instance is null.");
            return;
        }

        if (SaveSystem.Instance == null)
        {
            Debug.LogError("SaveSystem instance is null.");
            return;
        }

        float finalTime = UIManager.Instance.GetCurrentTime();
        Debug.Log("Final time captured: " + finalTime);

        SaveSystem.Instance.SaveTimer(finalTime);

        Debug.Log("Scores after save: " + string.Join(", ", SaveSystem.Instance.GetScores()));

        SceneManager.LoadScene("GameOverScene");
    }

    public void GameOverSceneStart()
    {
        if (SaveSystem.Instance == null)
        {
            Debug.LogError("SaveSystem instance is null.");
            return;
        }

        if (Leaderboard == null)
        {
            Debug.LogError("Leaderboard instance is null.");
            return;
        }

        SaveSystem.Instance.LoadScores();
        Leaderboard.Initialize(SaveSystem.Instance.GetScores());
    }

    public void PlayGame()
    {  SetGameState(GameState.InGame);
        SceneManager.LoadScene("GameScene");
        
        

    }

    public void StartGame()
    {
        SetGameState(GameState.InGame);
       
        SceneManager.LoadScene("GameScene");
        UIManager.OnPlayPressed();
        //Player.Initialize();
        //SegmentSpawner.Initialize();


        //Start Timer
    }

    

    [ContextMenu("Restart Game")]
    public void RestartGame()
    {
        SetGameState(GameState.InMenu);

        if (Leaderboard != null)
        {
            Leaderboard.Initialize(new List<float>());
        }

        SceneManager.LoadScene("TitleScene");
        UIManager.OnResetPressed();
        Leaderboard.GetComponent<Leaderboard>().Clear();
    }
    private void SetGameState(GameState state)
    { 
        CurrentGameState = state;
    }

   // public void IncreasedDifficulty()
  //  {
     //   Player.IncreasedDifficulty();
      //  SegmentSpawner.IncreasedDifficulty();
   // }
}
