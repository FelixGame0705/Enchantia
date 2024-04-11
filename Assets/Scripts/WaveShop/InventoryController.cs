using System.Collections.Generic;
using UnityEngine;
public class InventoryController : MonoBehaviour
{
    // [SerializeField] private List<ItemImageController> _weaponControllerList;
    // [SerializeField] private List<ItemImageController> _itemControllerList; 
    [SerializeField] private GameObject _weaponCardModel;
    [SerializeField] private GameObject _itemCardModel;
    [SerializeField] private GameObject _weaponContainer;
    [SerializeField] private GameObject _itemContainer;

    // public List<ItemImageController> ItemControllerList { get => _itemControllerList; set => _itemControllerList = value; }
    // public List<ItemImageController> WeaponControllerList { get =>  _weaponControllerList; set => _weaponControllerList = value;}
    public List<ItemImageController> WeaponControllerList { get => GameDataController.Instance.CurrentGamePlayData.WeaponControllerList; set => GameDataController.Instance.CurrentGamePlayData.WeaponControllerList = value; }
    public List<ItemImageController> ItemControllerList { get =>  GameDataController.Instance.CurrentGamePlayData.ItemControllerList; set => GameDataController.Instance.CurrentGamePlayData.ItemControllerList = value;}
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public ItemImageController GetWeaponCard(int index)
    {
        return WeaponControllerList[index];
    }

    private void OnEnable()
    {
        foreach (var cardController in WeaponControllerList)
        {
            cardController.EnableItem();
        }
        foreach (var cardController in ItemControllerList){
            cardController.EnableItem();
        }
    }

    public void AddCardToInventory(ItemData item)
    {
        Debug.Log("Create item");
        GameObject card = Instantiate(_itemCardModel, _itemContainer.transform);
        ItemControllerList.Add(card.GetComponent<ItemImageController>());
        card.GetComponent<ItemImageController>().SetCardData(item);
    }

    public void AddCardToWeapon(ItemData item)
    {
        GameObject card = Instantiate(_weaponCardModel, _weaponContainer.transform);
        card.GetComponent<ItemImageController>().SetCardData(item);
        card.GetComponent<ItemImageController>().SetID(WeaponControllerList.Count);
        card.GetComponent<ItemImageController>().CombineRecycleInfo.BuyWave = WaveShopMainController.Instance.CurrentWave;
        WeaponControllerList.Add(card.GetComponent<ItemImageController>());
    }

    public int GetCountWeapon()
    {
        return WeaponControllerList.Count;
    }

    public void UpgradeCard(int id)
    {
        var card = WeaponControllerList[id];
        var nextWeapon = card.GetCardData().NextItemWeapon;
        if(nextWeapon != null){
            card.SetCardData(nextWeapon);
            card.CombineRecycleInfo.CombineWave = WaveShopMainController.Instance.CurrentWave;
            card.CombineRecycleInfo.BuyWave = -1;
        }
        WeaponControllerList[id] = card;
        // _cardControllerList[id].SetCardData(_cardControllerList[id].GetCardData().NextItemWeapon);
    }

    public void RemoveCard(int id)
    {
        var card = WeaponControllerList.Find(x => x.GetComponent<ItemImageController>().GetID() == id);
        HandleGoldReturn(card);
        Destroy(card.gameObject);
        WeaponControllerList.Remove(card);
        InitSetIDWeaponDefault();
    }

    private void InitSetIDWeaponDefault()
    {
        for(int i = 0; i < WeaponControllerList.Count; i++)
        {
            WeaponControllerList[i].SetID(i);
        }
    }

    private void HandleGoldReturn(ItemImageController itemData){
        var waveLast = itemData.CombineRecycleInfo.BuyWave == -1 ? itemData.CombineRecycleInfo.CombineWave: itemData.CombineRecycleInfo.BuyWave;
        var finalPrice = Utils.Instance.GetFinalPrice(itemData.GetCardData().ItemPrice,waveLast);
        int priceReturn = (int)((int) finalPrice * 0.25);
        WaveShopMainController.Instance.CurrentMoney += priceReturn;
        WaveShopMainController.Instance.UpdateMoney();
    }

}
