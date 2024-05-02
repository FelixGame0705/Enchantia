using System.IO;
using Firebase.Auth;
using Firebase.Database;
using Firebase.Extensions;
using UnityEngine;

public class PlayerService : MonoBehaviour
{
    [SerializeField] private DatabaseReference firebaseDBRef;
    [SerializeField] private FirebaseAuth auth;
    private void Awake()
    {
        auth = FirebaseAuth.DefaultInstance;
        firebaseDBRef = FirebaseDatabase.DefaultInstance.RootReference;
    }
    private bool IsPlayerLogin(){
        return auth != null && auth.CurrentUser != null;
    }

    public UserData GetUserDataWithAuthentication(){
        if(!IsPlayerLogin()) return null;
        string userId = auth.CurrentUser.UserId;
        var userRef = firebaseDBRef.Child(Path.Combine("users", userId));
        userRef.GetValueAsync().ContinueWithOnMainThread(task => {
            if(task.IsFaulted || task.IsCanceled) Debug.Log("Error");
            else if(task.IsCompletedSuccessfully) {
                var snapshot = task.Result;
                var value = JsonUtility.FromJson<GoogleUserData>(snapshot.GetRawJsonValue());
                MapGGUserDataToUserData(value);
            }
        });
        return GameDataController.Instance.GoogleUserData;
    }


    private void MapGGUserDataToUserData(GoogleUserData userData){
        UserData user = GameDataController.Instance.GoogleUserData;
        user.Ads = userData.isAds;
        user.TimePlay = userData.timePlay;
        user.Gem = userData.gem;
        user.PlayerName = userData.playerName;
    }

    public void RegisterUser(){
        var userRef = firebaseDBRef.Child(Path.Combine("users",auth.CurrentUser.UserId));
        var userData = new GoogleUserData();
        userData.playerName = auth.CurrentUser.DisplayName;
        userData.userId = auth.CurrentUser.UserId;
        userData.timePlay = 0;
        userData.gem = 0;
        userData.isAds = true;
        var json = JsonUtility.ToJson(userData);
        userRef.SetRawJsonValueAsync(json).GetAwaiter();
    }
}

public class GoogleUserData { 
    public string playerName;
    public string userId;
    public int timePlay;
    public int gem;
    public bool isAds;
}