using System.Collections.Generic;
using UnityEngine;

public class GamePlaySceneDataController : Singleton<GamePlaySceneDataController>
{
    [SerializeField] private List<WaveRatio> _waveRatioList;
    [SerializeField] private List<ItemData> _itemAllDataList;
    [SerializeField] private List<ItemTierData> tierInfo;
}