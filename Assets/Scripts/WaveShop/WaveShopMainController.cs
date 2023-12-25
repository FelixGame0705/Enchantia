using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System;
using System.ComponentModel.Design;
using System.Data.Common;
using System.Runtime.CompilerServices;

public class WaveShopMainController : Singleton<WaveShopMainController>
{
    [SerializeField] private List<ItemData> _itemDataList;
    [SerializeField] private int _currentMoney = 0;
    [SerializeField] private int _currentWave = 0;
    [SerializeField] private int _rerollTime = 0;
    [SerializeField] private ItemViewListController _viewListController;
    [SerializeField] private InventoryController inventoryController;
    [SerializeField] private InventoryController weaponInventoryController;
    [SerializeField] private Text _moneyText;
    [SerializeField] private Text _waveText;
    [SerializeField] private DroppedItemController _droppedItemController;
    [SerializeField] private GameObject _detailWeapon;
    [SerializeField] private StatsPanelController _statsPanel;
    [SerializeField] private CharacterController _characterController;
    [SerializeField] private RerollMechanicController _rerollMechanicController;

    
    bool isPanel = false;

    private int _indexWeaponSelected;
    private Character_Mod _characterMod;
    public int CurrentMoney { get { return _currentMoney; } set { _currentMoney = value; } }
    public int CurrentWave { get { return _currentWave;} set { _currentWave = value; } }

    public List<ItemData> ItemDataList {get => _itemDataList;}
    public ItemViewListController ViewListController { get => _viewListController; set => _viewListController = value; }

    private void Update()
    {
        if(CurrentMoney - Utils.Instance.GetWaveShopRerollPrice(_currentWave, _rerollTime + 1) < 0) ViewListController.RerollController.ChangeRerollBtnState(false);
    }

    private void FixedUpdate()
    {
       
    }

    private void OnEnable()
    {
        _rerollTime = 0;
        CurrentWave = GamePlayController.Instance.CurrentWave - 1;
        AddGoldValue(GamePlayController.Instance.GetCharacterController().Harvesting());
        UpdateWaveDisplay();
        if (GamePlayController.Instance.GetCharacterController() != null)
        {
            UpdateStatsPanel();
        }
        if(GamePlayController.Instance.GameState == GAME_STATES.WAVE_SHOP){
            try{
                if(CurrentWave != 0 && CurrentWave != -1){
                    _rerollMechanicController.UpdateRerollWaveInfo(CurrentWave);
                    ViewListController.ReRoll(Random(4));
                    UpdateViewListInfo();
                }
                
            }catch(Exception ex){
            }
            
        }
        if(weaponInventoryController.GetCountWeapon() == 0)
        {
            for (int i = 0; i < _characterController.GetCharacterData().FirstItems.Count; i ++)
                EquipItemWeapon(_characterController.GetCharacterData().FirstItems[i], _characterController.GetCharacterData().FirstItems[i].ItemStats.WeaponBaseModel);
        }
        ViewListController.RerollController.ChangeRerollPriceUI(Utils.Instance.GetWaveShopRerollPrice(_currentWave, _rerollTime));
        UpdateMoney();
    }

    public void UpdateStatsPanel(){
        _characterController = GamePlayController.Instance.GetCharacterController();
        _characterMod = _characterController.CharacterModStats;
            _statsPanel.SetStats(_characterMod.MaxHP,_characterMod.HPRegeneration, _characterMod.LifeSteal, 
                _characterMod.Damage, _characterMod.MeleeDamage, _characterMod.RangedDamage,
                _characterMod.ElementalDamage
               ) ;
            _statsPanel.UpdateStatValues();
    }

    public void Reroll()
    {
        var moneyPayNeed = Utils.Instance.GetWaveShopRerollPrice(_currentWave, _rerollTime);
        if(CurrentMoney - moneyPayNeed >= 0){
            ViewListController.ReRoll(Random(4));
            _rerollTime ++;
            _currentMoney -= moneyPayNeed;
            ViewListController.RerollController.ChangeRerollPriceUI(Utils.Instance.GetWaveShopRerollPrice(_currentWave, _rerollTime));
            UpdateMoney();
            UpdateViewListInfo();
        }
    }
    private Stack<ItemData> Random(int amount)
    { 
        // Stack<ItemData> stack = new Stack<ItemData>();
        // for(int i = 0; i < amount; i++)
        // {
        //     Debug.Log("Index card " + _itemDataList.Count);
        //     int index = UnityEngine.Random.Range(0, _itemDataList.Count);
        //     Debug.Log("Index card " +index);
        //     stack.Push(_itemDataList[index]);
        // }
        // return stack;
        return _rerollMechanicController.GetRerollData();
    }
    public void BuyItem(int cardIndex)
    {
        ItemCardController itemCard = ViewListController.GetItemDataOfCardUsingPosition(cardIndex);
        var finalPrice = Utils.Instance.GetFinalPrice(itemCard.CardItemInfo.ItemPrice,CurrentWave);
        if(CurrentMoney >= finalPrice)
        {
            if (itemCard.CardItemInfo.ItemStats.TYPE1 == ITEM_TYPE.ITEM)
            {
                inventoryController.AddCardToInventory(itemCard.CardItemInfo);
                itemCard.CardItemInfo.ItemStats.Equip(_characterController.CharacterModStats);
                CurrentMoney -= finalPrice;
                itemCard.DisableItem();
            }
            else if (weaponInventoryController.GetCountWeapon() < 6)
            {
                EquipItemWeapon(itemCard.CardItemInfo, itemCard.CardItemInfo.ItemStats.WeaponBaseModel);
                CurrentMoney -= finalPrice;
                itemCard.DisableItem();
            }
        }
        
        UpdateMoney();
        UpdateStatsPanel();
        UpdateViewListInfo();
    }

    public void UpdateMoney()
    {
        _moneyText.text = CurrentMoney.ToString();
    }

    public void AddGoldValue(int amount)
    {
        _currentMoney += amount;
    }

    public void EquipItemWeapon(ItemData itemInfo, GameObject weaponModel)
    {
        weaponInventoryController.AddCardToWeapon(itemInfo);
        GamePlayController.Instance.GetWeaponSystem().EquipedWeapon(weaponModel);
    }

    public InventoryController GetWeaponInventory()
    {
        return weaponInventoryController;
    }

    public void SetDetailWeapon(Transform anchorTransform, bool isActive)
    {
        _detailWeapon.SetActive(isActive);
        _indexWeaponSelected = anchorTransform.gameObject.GetComponent<ItemImageController>().GetID();
    }

    public int GetIndexWeaponSelected()
    {
        return _indexWeaponSelected;
    }

    public DetailWeapon GetDetailWeapon()
    {
        return _detailWeapon.GetComponent<DetailWeapon>();
    }

    
    public void WatchStats()
    {
        isPanel = !isPanel;
        _statsPanel.gameObject.SetActive(isPanel);
    }

    public void UpdateFullLockItemStatus(){
        ViewListController.CheckAllLocked();
    }

    public void UpdateWaveDisplay(){
        _waveText.text = string.Concat("Wave ", _currentWave.ToString());
    }

    public int GetCountWeapon(){
        return weaponInventoryController.GetCountWeapon();
    }

    private void UpdateViewListInfo(){
        ViewListController.CheckAllValid();
    }
}
