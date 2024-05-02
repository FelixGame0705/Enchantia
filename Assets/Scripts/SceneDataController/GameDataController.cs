using System.Collections.Generic;
using UnityEngine;

public class GameDataController : Singleton<GameDataController>
{
    [Header("Hard Data")]
    [SerializeField] private List<WaveRatio> _waveRatioList;
    [SerializeField] private List<ItemData> _itemAllDataList;
    [SerializeField] private List<ItemData> _itemDataPlayerOwnedList;
    [SerializeField] private List<ItemTierData> _tierInfo;
    [SerializeField] private WaveRatio _defaultWaveRatio;
    [Header("Flexible Data")]
    [SerializeField] private GamePlayCurrentData _gamePlayCurrentData;

    [Header("Google Play Data")]
    [SerializeField] private UserData _userData;

    public GamePlayCurrentData CurrentGamePlayData{
        get {
            if(_gamePlayCurrentData == null){
                _gamePlayCurrentData = FindObjectOfType<GamePlayCurrentData>();
            }
            return _gamePlayCurrentData;
        }
    }
    public void DiscardGamePlayCurrentData () {
        this._gamePlayCurrentData = null;
    }
    public List<WaveRatio> WaveRatios{get => _waveRatioList;}
    public List<ItemData> ItemAllDataList{get => _itemAllDataList;}
    public List<ItemTierData> TierInfo {get => _tierInfo;}
    public List<ItemData> ItemDataPlayerOwnedList{ get => _itemDataPlayerOwnedList;}
    public WaveRatio WaveRatioDefault{get => _defaultWaveRatio;}
    public UserData GoogleUserData {get => _userData;}
}