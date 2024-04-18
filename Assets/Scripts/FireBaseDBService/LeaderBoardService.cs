using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Firebase.Database;
using Firebase.Extensions;
using UnityEngine;

public class LeaderBoardService : MonoBehaviour
{
    [SerializeField] private DatabaseReference firebaseDBRef;
    [SerializeField] private string pathName;

    private void Awake()
    {
        firebaseDBRef = FirebaseDatabase.DefaultInstance.RootReference;
        pathName = "leader_board";
    }

    public void GetLeaderBoardWithPagination(int page, int pageSize, Action<List<LeaderboardItemData>> callBack)
    {
        int startIndex = page * pageSize;
        var leaderboardRef = firebaseDBRef.Child("leader_board");
        leaderboardRef.GetValueAsync().ContinueWithOnMainThread(task =>
        {
            if (task.IsFaulted)
            {
                Debug.LogError("Failed to fetch users: " + task.Exception);
            }
            else if (task.IsCompleted)
            {
                var snapshot = task.Result;
                List<LeaderboardItemData> itemDatas = new List<LeaderboardItemData>();
                var list = snapshot.Children.Skip(startIndex).Take(pageSize).ToList();
                foreach (var item in list)
                {
                    var json = item.GetRawJsonValue();
                    var value = JsonUtility.FromJson<LeaderBoardJsonUserData>(json);
                    itemDatas.Add(new LeaderboardItemData(value));
                }
                callBack?.Invoke(itemDatas);
            }
        });
    }

    public async Task AddNewLeaderBoard(LeaderboardItemData leaderboardItemData)
    {
        var leaderBoardRef = firebaseDBRef.Child("leader_board").Push();
        var leaderBoardJsonData = leaderboardItemData.ToJsonData();

        try
        {
            await firebaseDBRef.Child("counter_info").Child("leaderboard_counter")
                .RunTransaction(mutableData =>
                {
                    var value = Convert.ToInt64(mutableData.Value ?? 0);
                    value += 1;
                    mutableData.Value = value;

                    leaderBoardRef.SetRawJsonValueAsync(leaderBoardJsonData).GetAwaiter();

                    return TransactionResult.Success(mutableData);
                });
        }
        catch (Exception ex)
        {
            Debug.LogException(ex);
        }
    }

    public async Task<long> GetMaxPage(long pageSize){
        long maxPage = 0;
        var result =await firebaseDBRef.Child("counter_info").Child("leaderboard_counter").GetValueAsync();
        if (result.Exists)
        {
            return (long)Convert.ToInt64(result.Value)/pageSize;
        }
        else
        {
            Debug.LogWarning("Leaderboard counter does not exist in the database.");
            return (long)0; // Or handle the case as needed
        }
        return maxPage;
    }
}

