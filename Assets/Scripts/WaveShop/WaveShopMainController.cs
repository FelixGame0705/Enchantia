using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class WaveShopMainController : Singleton<WaveShopMainController>
{
    [SerializeField] private List<ItemData> _itemDataList;
    [SerializeField] private int _currentMoney = 100;
    [SerializeField] private int _currentWave = 1;
    [SerializeField] private ItemViewListController _viewListController;
    [SerializeField] private InventoryController inventoryController;
    [SerializeField] private InventoryController weaponInventoryController;
    [SerializeField] private Text _moneyText;
    [SerializeField] private CurrencyController _currentController;
    [SerializeField] private GameObject _detailWeapon;

    private int _indexWeaponSelected;
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
        CurrentMoney = _currentController.GetGold();
        Reroll();
        UpdateMoney();
        if(weaponInventoryController.GetCountWeapon() == 0)
        {
            for (int i = 0; i < GamePlayController.Instance.GetCharacterController().GetCharacterData().CharacterStats.FirstItems.Count; i ++)
                EquipItemWeapon(GamePlayController.Instance.GetCharacterController().GetCharacterData().CharacterStats.FirstItems[i], GamePlayController.Instance.GetCharacterController().GetCharacterData().CharacterStats.FirstItems[i].ItemStats.WeaponBaseModel);
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
        if(CurrentMoney> itemCard.CardItemInfo.ItemPrice)
        {
            CurrentMoney -= itemCard.CardItemInfo.ItemPrice;
            if (itemCard.CardItemInfo.ItemStats.TYPE1 == ITEM_TYPE.ITEM)
                inventoryController.AddCardToInventory(itemCard.CardItemInfo);
            else if(weaponInventoryController.GetCountWeapon() < 6)
            {
                EquipItemWeapon(itemCard.CardItemInfo, itemCard.CardItemInfo.ItemStats.WeaponBaseModel);
            }
            itemCard.DisableItem();
        }
        UpdateMoney();
    }

    private void UpdateMoney()
    {
        _moneyText.text = CurrentMoney.ToString();
    }

    public void AddGoldValue(int amount)
    {
        CurrentMoney += amount;
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
        //_detailWeapon.transform.position = anchorTransform.position + new Vector3(130, 0, 0);
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
}
