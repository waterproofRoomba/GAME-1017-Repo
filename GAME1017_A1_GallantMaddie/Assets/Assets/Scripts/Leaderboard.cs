using TMPro;
using UnityEngine;
using System.Collections.Generic;

public class Leaderboard : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI leaderboardText;

    public void Initialize(List<float> bestTime)
    {
        string leaderboardString = "Leaderboard";

        for (int i = 0; i < bestTime.Count; i++)
        {
            leaderboardString += "\nPosition # " + (i + 1) + " " + bestTime[i].ToString("0.00");
        }

        leaderboardText.text = leaderboardString;
    }

    //call this from game manager
    public void Clear()
    {
        leaderboardText.text = "";
    }
}