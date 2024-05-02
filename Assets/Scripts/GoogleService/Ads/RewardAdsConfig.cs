using GoogleMobileAds.Api;
using UnityEngine;

public class RewardAdsConfig : MonoBehaviour
{
    [SerializeField] private string _adsUnitId;
    [SerializeField] private string _androidAdsUnitId;
    [SerializeField] private string _iosAdsUnitId;
    private RewardedAd _rewardedAd;

    public void LoadAdsUnit(){
        #if UNITY_ANDROID
            this._adsUnitId = _androidAdsUnitId;
        #elif UNITY_IPHONE
            this._adsUnitId = _iosAdsUnitId;
        #else
            this._adsUnitId = _androidAdsUnitId;
        #endif
    }

    public void LoadRewardedAd()
    {
      if (_rewardedAd != null)
      {
            _rewardedAd.Destroy();
            _rewardedAd = null;
      }

      Debug.Log("Loading the rewarded ad.");

      // create our request used to load the ad.
      var adRequest = new AdRequest();

      // send the request to load the ad.
      RewardedAd.Load(_adsUnitId, adRequest,
          (RewardedAd ad, LoadAdError error) =>
          {
              // if error is not null, the load request failed.
              if (error != null || ad == null)
              {
                  Debug.LogError("Rewarded ad failed to load an ad " +
                                 "with error : " + error);
                  return;
              }

              Debug.Log("Rewarded ad loaded with response : "
                        + ad.GetResponseInfo());

              _rewardedAd = ad;
        });
    }

    public void ShowRewardedAd()
    {
        const string rewardMsg =
            "Rewarded ad rewarded the user. Type: {0}, amount: {1}.";

        if (_rewardedAd != null && _rewardedAd.CanShowAd())
        {
            _rewardedAd.Show((Reward reward) =>
            {
                Debug.Log(string.Format(rewardMsg, reward.Type, reward.Amount));
            });
        }
    }


}