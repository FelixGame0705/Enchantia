using System;
using UnityEngine;

//using GoogleMobileAds.Api;
public class AdsService : MonoBehaviour
{
    [SerializeField]
    private RewardAdsConfig _rewardAdsConfig;

    private void Awake()
    {
        GoogleMobileAdsInit();
    }

    private void GoogleMobileAdsInit()
    {
        //MobileAds.Initialize(initStatus => {
        //    AdsConfigLoad();
        //});
    }

    private void AdsConfigLoad()
    {
        _rewardAdsConfig.LoadAdsUnit();
    }
}
