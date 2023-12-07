using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class WaveShopMainController : Singleton<WaveShopMainController>
{
    [SerializeField] private List<ItemData> _itemDataList;
    [SerializeField] private int _currentMoney = 0;
    [SerializeField] private int _currentWave = 1;
    [SerializeField] private ItemViewListController _viewListController;
    [SerializeField] private InventoryController inventoryController;
    [SerializeField] private InventoryController weaponInventoryController;
    [SerializeField] private Text _moneyText;
    [SerializeField] private DroppedItemController _droppedItemController;
    [SerializeField] private GameObject _detailWeapon;
    [SerializeField] private StatsPanelController _statsPanel;
    [SerializeField] private CharacterController _characterController;

    private int _indexWeaponSelected;
    private Character_Mod _characterMod;
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
        //_moneyText.text = _currentMoney.ToString();
    }

    private void OnEnable()
    {
        
        if (GamePlayController.Instance.GetCharacterController() != null)
        {
            _characterController = GamePlayController.Instance.GetCharacterController();
            _characterMod = _characterController.CharacterModStats;
            _statsPanel.SetStats(_characterMod.MaxHP,_characterMod.HPRegeneration, _characterMod.LifeSteal, 
                _characterMod.Damage, _characterMod.MeleeDamage, _characterMod.RangedDamage,
                _characterMod.ElementalDamage
               ) ;
            _statsPanel.UpdateStatValues();
        }
        Reroll();
        UpdateMoney();
        if(weaponInventoryController.GetCountWeapon() == 0)
        {
            for (int i = 0; i < _characterController.GetCharacterData().FirstItems.Count; i ++)
                EquipItemWeapon(_characterController.GetCharacterData().FirstItems[i], _characterController.GetCharacterData().FirstItems[i].ItemStats.WeaponBaseModel);
        }
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
            Debug.Log("Index card " + _itemDataList.Count);
            int index = UnityEngine.Random.Range(0, _itemDataList.Count);
            Debug.Log("Index card " +index);
            stack.Push(_itemDataList[index]);
        }
        return stack;
    }
    public void BuyItem(int cardIndex)
    {
        ItemCardController itemCard = _viewListController.GetItemDataOfCardUsingPosition(cardIndex);
        if(CurrentMoney >= itemCard.CardItemInfo.ItemPrice)
        {
            CurrentMoney -= itemCard.CardItemInfo.ItemPrice;
            if (itemCard.CardItemInfo.ItemStats.TYPE1 == ITEM_TYPE.ITEM)
            {
                inventoryController.AddCardToInventory(itemCard.CardItemInfo);
                itemCard.CardItemInfo.ItemStats.Equip(_characterController.CharacterModStats);
                _statsPanel.UpdateStatValues();
            }
            else if (weaponInventoryController.GetCountWeapon() < 6)
            {
                EquipItemWeapon(itemCard.CardItemInfo, itemCard.CardItemInfo.ItemStats.WeaponBaseModel);
            }
            itemCard.DisableItem();
        }
        UpdateMoney();
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

    bool isPanel = false;
    public void WatchStats()
    {
        isPanel = !isPanel;
        _statsPanel.gameObject.SetActive(isPanel);
    }

    public void UpdateFullLockItemStatus(){
        _viewListController.CheckAllLocked();
    }
}
