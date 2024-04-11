using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System;

public class WaveShopMainController : Singleton<WaveShopMainController>
{
    [SerializeField] private int _rerollTime = 0;

    [Header ("Controller list")]
    [SerializeField] private ItemViewListController _viewListController;
    [SerializeField] private InventoryController inventoryController;
    [SerializeField] private StatsPanelController _statsPanel;
    [SerializeField] private CharacterController _characterController;
    [SerializeField] private RerollMechanicController _rerollMechanicController;
    [SerializeField] private CombindPanelController _combindPanelController;   
    [SerializeField] private CombineRecycleMechanicController _combineRecycleMechanicController;
    [SerializeField] private Text _moneyText;
    [SerializeField] private Text _waveText;
    [SerializeField] private DroppedItemController _droppedItemController;
    [SerializeField] private GameObject _detailWeapon;


    
    bool isPanel = false;

    private int _indexWeaponSelected;
    private Character_Mod _characterMod;
    public int CurrentMoney { get { return GameDataController.Instance.CurrentGamePlayData.CurrentGold; } set { GameDataController.Instance.CurrentGamePlayData.CurrentGold = value; } }
    public int CurrentWave { get { return GameDataController.Instance.CurrentGamePlayData.CurrentWave -1;} set { GameDataController.Instance.CurrentGamePlayData.CurrentWave = value; } }
    public ItemViewListController ViewListController { get => _viewListController; set => _viewListController = value; }

    public CombindPanelController CombindPanelController {get => _combindPanelController;}
    public CombineRecycleMechanicController CombineRecycleMechanicController {get => this._combineRecycleMechanicController;}

    private void Update()
    {
        if(CurrentMoney - Utils.Instance.GetWaveShopRerollPrice(CurrentWave, _rerollTime + 1) < 0) ViewListController.RerollController.ChangeRerollBtnState(false);
    }

    private void OnEnable()
    {
        _rerollTime = 0;
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
                Debug.LogError(ex.Message);
            }
            
        }
        if(inventoryController.GetCountWeapon() == 0)
        {
            for (int i = 0; i < _characterController.GetCharacterData().FirstItems.Count; i ++)
                EquipItemWeapon(_characterController.GetCharacterData().FirstItems[i], _characterController.GetCharacterData().FirstItems[i].ItemStats.WeaponBaseModel);
        }
        ViewListController.RerollController.ChangeRerollPriceUI(Utils.Instance.GetWaveShopRerollPrice(CurrentWave, _rerollTime));
        UpdateMoney();
    }

    private void OnDisable()
    {
        _combindPanelController.HidePanel();
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
        var moneyPayNeed = Utils.Instance.GetWaveShopRerollPrice(CurrentWave, _rerollTime);
        if(CurrentMoney - moneyPayNeed >= 0){
            ViewListController.ReRoll(Random(4));
            _rerollTime ++;
            CurrentMoney -= moneyPayNeed;
            ViewListController.RerollController.ChangeRerollPriceUI(Utils.Instance.GetWaveShopRerollPrice(CurrentWave, _rerollTime));
            UpdateMoney();
            UpdateViewListInfo();
            GameDataController.Instance.CurrentGamePlayData.TotalReroll += 1;
            GameDataController.Instance.CurrentGamePlayData.TotalSpend = moneyPayNeed;
        }
    }
    private Stack<ItemData> Random(int amount)
    { 
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
            else if (inventoryController.GetCountWeapon() < 6)
            {
                EquipItemWeapon(itemCard.CardItemInfo, itemCard.CardItemInfo.ItemStats.WeaponBaseModel);
                CurrentMoney -= finalPrice;
                itemCard.DisableItem();
            }
            GameDataController.Instance.CurrentGamePlayData.TotalSpend = finalPrice;
            GameDataController.Instance.CurrentGamePlayData.TotalBuy += 1;
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
        CurrentMoney += amount;
    }

    public void EquipItemWeapon(ItemData itemInfo, GameObject weaponModel)
    {
        inventoryController.AddCardToWeapon(itemInfo);
        GamePlayController.Instance.GetWeaponSystem().EquipedWeapon(weaponModel);
    }

    public InventoryController GetWeaponInventory()
    {
        return inventoryController;
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

    public void SetIndexWeaponSelected(int index){
        this._indexWeaponSelected = index;
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
        _waveText.text = string.Concat("Wave ", CurrentWave.ToString());
    }

    public int GetCountWeapon(){
        return inventoryController.GetCountWeapon();
    }

    public void UpdateViewListInfo(){
        ViewListController.CheckAllValid();
    }
}
