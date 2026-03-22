using TMPro;
using UnityEngine;

public class Leaderboard : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI leaderBoardText;

    public void Initialize(float bestTime)
    {
        leaderBoardText.text = "BestTime: " + bestTime.ToString("0.00");
    }

    public void LoadLeaderboard()
    {

    }
}
