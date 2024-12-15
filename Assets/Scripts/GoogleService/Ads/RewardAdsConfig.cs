//using GoogleMobileAds.Api;
using UnityEngine;

public class RewardAdsConfig : MonoBehaviour
{
    [SerializeField]
    private string _adsUnitId;

    [SerializeField]
    private string _androidAdsUnitId;

    [SerializeField]
    private string _iosAdsUnitId;

    //private RewardedAd _rewardedAd;

    public void LoadAdsUnit()
    {
#if UNITY_ANDROID
        _adsUnitId = _androidAdsUnitId;
#elif UNITY_IPHONE
        _adsUnitId = _iosAdsUnitId;
#else
        _adsUnitId = _androidAdsUnitId;
#endif
    }

    public void LoadRewardedAd()
    {
        //if (_rewardedAd != null)
        //{
        //    _rewardedAd.Destroy();
        //    _rewardedAd = null;
        //}

        Debug.Log("Loading the rewarded ad.");

        // Correct way to create an ad request.
        //var adRequest = new AdRequest.Builder().Build();

        // Create and load a new RewardedAd instance.
        //_rewardedAd = new RewardedAd(_adsUnitId);
        //_rewardedAd.LoadAd(adRequest);
    }

    public void ShowRewardedAd()
    {
        const string rewardMsg = "Rewarded ad rewarded the user. Type: {0}, amount: {1}.";

        //if (_rewardedAd != null && _rewardedAd.IsLoaded())
        //{
        //    _rewardedAd.Show();
        //}
        //else
        //{
        //    Debug.Log("Rewarded ad is not ready to show.");
        //}
    }
}
