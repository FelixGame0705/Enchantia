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
        rank.SetText(data.rank.ToString());
        Name.SetText(data.name);
        time.SetText(data.time.ToString("mm:ss"));
        wave.SetText(data.wave.ToString());
        score.SetText(data.score.ToString());
    }
}
