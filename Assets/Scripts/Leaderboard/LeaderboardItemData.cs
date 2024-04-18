using System.Data.Common;
using UnityEngine;

public class LeaderboardItemData
{
    public int Rank {get => _leaderBoardJsonData.rank; set => _leaderBoardJsonData.rank = value;}
    public string PlayerName {get => _leaderBoardJsonData.playerName; set => _leaderBoardJsonData.playerName = value;}
    public int Time {get => _leaderBoardJsonData.time; set => _leaderBoardJsonData.time = value;}
    public int Wave {get => _leaderBoardJsonData.wave; set => _leaderBoardJsonData.wave = value;}
    public int Score {get => _leaderBoardJsonData.score; set => _leaderBoardJsonData.score = value;}
    private readonly LeaderBoardJsonUserData _leaderBoardJsonData;
    public LeaderboardItemData(int rank, string name, int time, int wave, int score)
    {
        _leaderBoardJsonData = new()
        {
            rank = rank,
            playerName = name,
            time = time,
            wave = wave,
            score = score
        };
    }
    public LeaderboardItemData(LeaderBoardJsonUserData leaderBoardJsonUserData){
        _leaderBoardJsonData = leaderBoardJsonUserData;
    }

    public LeaderBoardJsonUserData LeaderBoardJsonData {get => _leaderBoardJsonData;}
    public string ToJsonData(){
        return JsonUtility.ToJson(_leaderBoardJsonData);
    }
}