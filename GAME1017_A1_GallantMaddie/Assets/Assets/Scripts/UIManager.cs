using UnityEngine;
using TMPro;

public class UIManager : Singleton<UIManager>
{
    [SerializeField] private GameObject playButton, resetButton, gameOverButton;
   
    
    
    private void Start()
    {
        Initialize();
    }

    public void Initialize()
    {
        time = 0;
        playButton.SetActive(true);
        resetButton.SetActive(false);
        gameOverButton.SetActive(false);
        
    }

    public void OnPlayPressed()
    {
        playButton.SetActive(false);
        resetButton.SetActive(true);
        gameOverButton.SetActive(true);
        
    }

    public void GameOver()
    {
        time = 0;
        playButton.SetActive(false);
        resetButton.SetActive(true);
        gameOverButton.SetActive(false);
    }
    public void OnResetPressed()
    {
        resetButton.SetActive(false);
        gameOverButton.SetActive(false);
        Initialize();
    }


    public float time;
    public TextMeshProUGUI timerText;
    private void Update()
    {
        if (GameManager.Instance.CurrentGameState == GameState.InGame)
        time += Time.deltaTime;
        timerText.text = "Time: " + Mathf.Floor(time).ToString();
    }

    public float GerCurrentBestTime()
    {
        return time;
    }
}
