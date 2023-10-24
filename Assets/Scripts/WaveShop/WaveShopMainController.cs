using System;
using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using Unity.Mathematics;
using UnityEditor;
using UnityEngine;
using UnityEngine.UI;
using static UnityEditor.PlayerSettings;

public class WaveShopMainController : Singleton<WaveShopMainController>
{
    [SerializeField] private List<ItemData> _itemDataList;
    [SerializeField] private int _currentMoney = 100;
    [SerializeField] private int _currentWave = 1;
    [SerializeField] private ItemViewListController _viewListController;
    [SerializeField] private Text _moneyText;

    public int CurrentMoney { get { return _currentMoney; } set { _currentMoney = value; } }
    public int CurrentWave { get { return _currentWave;} set { _currentWave = value; } }
    
    private void Awake()
    {
           
    }
    private void Update()
    {
       
    }

    private void FixedUpdate()
    {
        _moneyText.text = _currentMoney.ToString();
    }

    public void Reroll()
    {
        _viewListController.ReRoll(Random(4));
        Debug.Log("Clicked");
    }
    private Stack<ItemData> Random(int amount)
    { 
        Stack<ItemData> stack = new Stack<ItemData>();
        for(int i = 0; i < amount; i++)
        {
            int index = UnityEngine.Random.Range(0, _itemDataList.Count);
            stack.Push(_itemDataList[index]);
        }
        return stack;
    }
    public void BuyItem(int cardIndex)
    {
        var itemData = _viewListController.GetItemDataOfCardUsingPosition(cardIndex);
        if(CurrentMoney> itemData.ItemPrice)
        {
            CurrentMoney -= itemData.ItemPrice;
        }
    }

}
