using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SaveSystem : MonoBehaviour
{
    public static SaveSystem Instance { get; private set; }

    private const string LeaderboardKey = "LeaderboardScore";
    private const string LeaderboardCountKey = "LeaderboardCount";
    private const int MaxHighScores = 5;

    private List<float> leaderboard = new List<float>();

    private void Awake()
    {
        if (Instance != null && Instance != this)
        {
            Destroy(gameObject);
            return;
        }

        Instance = this;
        DontDestroyOnLoad(gameObject);

        LoadScores();
    }

    public void SaveTimer(float timer)
    {
        Debug.Log("Saving timer value: " + timer);

        leaderboard.Add(timer);
        leaderboard.Sort();
        leaderboard.Reverse();

        if (leaderboard.Count > MaxHighScores)
        {
            leaderboard.RemoveAt(leaderboard.Count - 1);
        }

        for (int i = 0; i < leaderboard.Count; i++)
        {
            PlayerPrefs.SetFloat(LeaderboardKey + i, leaderboard[i]);
            Debug.Log("Stored score " + i + ": " + leaderboard[i]);
        }

        PlayerPrefs.SetInt(LeaderboardCountKey, leaderboard.Count);
        PlayerPrefs.Save();

        Debug.Log("Saved timer complete.");
    }

    public void LoadScores()
    {
        leaderboard.Clear();

        int count = PlayerPrefs.GetInt(LeaderboardCountKey, 0);

        for (int i = 0; i < count; i++)
        {
            float score = PlayerPrefs.GetFloat(LeaderboardKey + i, 0f);
            leaderboard.Add(score);
        }

        leaderboard.Sort();
        leaderboard.Reverse();
    }

    public List<float> GetScores()
    {
        return new List<float>(leaderboard);
    }
}