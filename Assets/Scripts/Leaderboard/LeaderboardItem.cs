using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class LeaderboardItem : MonoBehaviour
{
    public LeaderboardItemData leaderboardItemData;

    public TextMeshProUGUI rank;
    public TextMeshProUGUI Name;
    public TextMeshProUGUI time;
    public TextMeshProUGUI wave;
    public TextMeshProUGUI score;

    public void SetData(LeaderboardItemData data)
    {
        rank.SetText(data.Rank.ToString());
        Name.SetText(data.PlayerName);
        time.SetText(data.Time.ToString("mm:ss"));
        wave.SetText(data.Wave.ToString());
        score.SetText(data.Score.ToString());
    }
}
