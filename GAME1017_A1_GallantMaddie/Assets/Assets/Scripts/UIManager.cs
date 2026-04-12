using UnityEngine;
using TMPro;
using UnityEngine.SceneManagement;

public class UIManager : Singleton<UIManager>
{
    [SerializeField] private GameObject playButton, resetButton, gameOverButton;
    [SerializeField] private TextMeshProUGUI timerText;

    private float time;

    private void Start()
    {
        Initialize();
    }

    public void Initialize()
    {
        time = 0f;
        playButton.SetActive(true);
        resetButton.SetActive(false);
        gameOverButton.SetActive(false);
        UpdateTimerText();
    }

    public void OnPlayPressed()
    {
        playButton.SetActive(false);
        resetButton.SetActive(true);
        gameOverButton.SetActive(true);
    }

    public void GameOver()
    {
        playButton.SetActive(false);
        resetButton.SetActive(true);
        gameOverButton.SetActive(false);

        GameManager.Instance.GameOver();
    }

    public void OnResetPressed()
    {
        resetButton.SetActive(false);
        gameOverButton.SetActive(false);
        playButton.SetActive(true);
        Initialize();
    }

    private void Update()
    {
        if (SceneManager.GetActiveScene().name != "GameScene")
            return;

        if (GameManager.Instance.CurrentGameState == GameState.InGame)
        {
            time += Time.deltaTime;
            UpdateTimerText();
        }
    }

    private void UpdateTimerText()
    {
        timerText.text = "Time: " + Mathf.Floor(time).ToString();
    }

    public float GetCurrentTime()
    {
        return time;
    }
}