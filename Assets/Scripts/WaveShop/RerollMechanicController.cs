using System.Collections.Generic;
using UnityEngine;
using System.Linq;
using System;


public class RerollMechanicController : MonoBehaviour
{
    [SerializeField] private List<WaveRatio> _waveRatioList;
    [SerializeField] private List<ItemData> _itemAllDataList;
    [SerializeField] private List<ItemTierData> tierInfo;
    [SerializeField] private Dictionary<int, List<ItemData>> _currentWaveDataList;
    [SerializeField] private int _currentWave = 1;
    [SerializeField] private List<ItemTierData> _currentTierData;
    [SerializeField] private WaveRatio _currentWaveRatio;
    [SerializeField] private WaveRatio _defaultWaveRatio;

    private void Awake()
    {
        this._itemAllDataList = WaveShopMainController.Instance.ItemDataList;
    }
    private void ResetWaveInfo()
    {
    }

    public void UpdateRerollWaveInfo(int currentWave)
    {
        ResetWaveInfo();
        this._currentWave = currentWave;
        QueryCurrentRatio();
        var tierValid = tierInfo.FindAll(
            delegate (ItemTierData data)
            {
                return data.WaveAvailable <= currentWave;
            }
        );
        _currentTierData = tierValid;
        ResetCurrentWaveDataList();
    }



    public Stack<ItemData> GetRerollData()
    {
        var data = PrepareSlot(); 
        return CreateItemStack(data);
    }

    private Stack<ItemData> CreateItemStack(List<ItemData> data)
    {
        Utils.Instance.ShuffleList<ItemData>(ref data);       
        return new Stack<ItemData>(data);
    }

    private List<ItemData> PrepareSlot()
    {
        return RandomMechanic();
    }


    private List<ItemData> RandomMechanic()
    {
        List<ItemData> randomSlotData = new List<ItemData>();
        var frameList = CreateSlotWithRare();
        foreach(var i in frameList){
            int tier = i[1];
            do{
                var tierList = _currentWaveDataList.ContainsKey(i[1]) ? _currentWaveDataList[i[1]] : null;
                var type  = i[0] == 0 ?  ITEM_TYPE.ITEM : ITEM_TYPE.WEAPON;
                var suitableData = tierList.Where(data => data.ItemStats.TYPE1 == type).ToList();
                if(suitableData.Count == 0 && tier != 1) tier --;
                else{
                    var random = Math.Abs(UnityEngine.Random.Range(0,suitableData.Count - 1));
                    randomSlotData.Add(suitableData[random]);
                    break;
                }
            }while(true);
            
        }
        return randomSlotData;
    }



    private void QueryCurrentRatio()
    {
        foreach (var data in _waveRatioList)
        {
            if (data.IsWaveValid(_currentWave))
            {
                this._currentWaveRatio = data;
                return;
            }
        }
        this._currentWaveRatio = _defaultWaveRatio;
    }

    private List<int> CreateSlotRatio()
    {
        List<int> slot = new List<int>();
        var ratio = _currentWaveRatio;

        if (ratio.Wave[0] < 0)
        {
            for (int i = 0; i < 4; i++)
            {
                float random = UnityEngine.Random.Range(0f, 100f);
                if (random <= ratio.MinWeapon) slot.Add(1);
                else slot.Add(0);
            }
        }
        else
        {
            int weaponCount = 0;
            int itemCount = 0;
            do
            {
                if (weaponCount < ratio.MinWeapon && ratio.MinWeapon != 0)
                    {
                        slot.Add(1);
                        weaponCount++;
                    }
                    else if (itemCount < ratio.MinItem && ratio.MinItem != 0)
                    {
                        slot.Add(0);
                        itemCount++;
                    }else if(slot.Count < 4){
                        float random = UnityEngine.Random.Range(0f, 100f);
                        if (random <= _defaultWaveRatio.MinWeapon) slot.Add(1);
                        else slot.Add(0);
                    }else break;
            } while (true);

        }
        return slot;
    }

    private List<List<int>> CreateSlotWithRare(){
        List<List<int>> slot = new List<List<int>>();
        var ratio = _currentWaveRatio;
        if(ratio.Wave[0] < 0){
            for(int i = 0; i < 4; i++){
                float random = UnityEngine.Random.Range(0f, 100f);
                var temp = new List<int>();
                if (random <= ratio.MinWeapon) temp.Add(1);
                else temp.Add(0);
                temp.Add(IsRate());
                slot.Add(temp);
            }
        }else{
            int weaponCount = 0;
            int itemCount = 0;
            do
            {
                var temp = new List<int>();
                if (weaponCount < ratio.MinWeapon && ratio.MinWeapon != 0)
                    {
                        temp.Add(1);
                        weaponCount++;
                    }
                    else if (itemCount < ratio.MinItem && ratio.MinItem != 0)
                    {
                        temp.Add(0);
                        itemCount++;
                    }else if(slot.Count < 4){
                        float random = UnityEngine.Random.Range(0f, 100f);
                        if (random <= _defaultWaveRatio.MinWeapon) temp.Add(1);
                        else temp.Add(0);
                    }else break;
                    temp.Add(IsRate());
                    slot.Add(temp);
            } while (true);
        }
        return slot;
    }

    private int IsRate(){
        for(int i = _currentTierData.Count - 1; i >= 0; i--){
            var data = _currentTierData[i];
            var rate = Utils.Instance.GetChanceRateTierPerWave(data.ChancePerWave, _currentWave, data.MinWave, 0, data.BaseChance) * 100;
            float random = UnityEngine.Random.Range(0f,100f);
            if(random <= rate) return data.Tier;
        }
        return 1;
    }

    private void ResetCurrentWaveDataList(){
        Dictionary<int, List<ItemData>> tempDataList =  new Dictionary<int, List<ItemData>>();
        var size = _currentTierData.Count;
        for(int i = 0; i <  size; i++){
            tempDataList.Add(i + 1, PickDataFollowByTier(i));
        }
        _currentWaveDataList = tempDataList;
    }

    private List<ItemData> PickDataFollowByTier(int tier){
        var tempTier = _currentTierData[tier];
        var result = _itemAllDataList.Where(data => data.Tier == tempTier.Tier).ToList();
        return result;
    }
}
