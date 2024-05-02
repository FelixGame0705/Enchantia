using System;
using UnityEngine;

public class UserData : MonoBehaviour
{
    private string playerName;
    private string userId;
    private int timePlay;
    private int gem;
    private bool isAds;
    private bool isLogin;

    private void Awake()
    {
        playerName = "";
        timePlay = 0;
        gem = 0;
        isAds = true;
        isLogin = false;
    }

    public string PlayerName { get { return playerName; } set {playerName = value;} }
    public int TimePlay { get { return timePlay;} set {timePlay = value;} }
    public int Gem { get { return gem;} set {gem = value;} }
    public bool Ads { get { return isAds;} set {isAds = value;} }
    public string UserId { get { return userId;} set {userId = value;} }
    public bool IsLogin {get {return  isLogin ;} set {isLogin = value;}}
}