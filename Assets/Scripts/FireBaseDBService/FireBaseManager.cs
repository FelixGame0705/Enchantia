using UnityEngine;

public class FireBaseManager : Singleton<FireBaseManager>
{
    [SerializeField] private LeaderBoardService _leaderboardService;

    public LeaderBoardService LeaderboardService {get {return _leaderboardService;}}
}