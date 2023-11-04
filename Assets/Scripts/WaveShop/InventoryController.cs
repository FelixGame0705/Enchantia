using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static UnityEditor.Progress;

public class InventoryController : MonoBehaviour
{
    [SerializeField] private List<ItemImageController> _cardControllerList;
    [SerializeField] private GameObject _cardModel;

    public List<ItemImageController> CardControllerList { get => _cardControllerList; set => _cardControllerList = value; }

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
        return _cardControllerList[index];
    }

    private void OnEnable()
    {
        foreach (var cardController in _cardControllerList)
        {
            cardController.EnableItem();
        }
    }

    public void AddCardToInventory(ItemData item)
    {
        Debug.Log("Create item");
        GameObject card = Instantiate(_cardModel, gameObject.transform);
        _cardControllerList.Add(card.GetComponent<ItemImageController>());
        card.GetComponent<ItemImageController>().SetCardData(item);
    }

    public void AddCardToWeapon(ItemData item)
    {
        GameObject card = Instantiate(_cardModel, gameObject.transform);
        card.GetComponent<ItemImageController>().SetCardData(item);
        card.GetComponent<ItemImageController>().SetID(_cardControllerList.Count);
        _cardControllerList.Add(card.GetComponent<ItemImageController>());
    }

    public int GetCountWeapon()
    {
        return _cardControllerList.Count;
    }

    public void UpgradeCard(int id)
    {
        _cardControllerList[id].SetCardData(_cardControllerList[id].GetCardData().NextItemWeapon);
    }

    public void RemoveCard(int id)
    {
        Destroy(_cardControllerList.Find(x => x.GetComponent<ItemImageController>().GetID() == id).gameObject);
        _cardControllerList.Remove(_cardControllerList.Find(x => x.GetComponent<ItemImageController>().GetID() == id));
        InitSetIDWeaponDefault();
    }

    private void InitSetIDWeaponDefault()
    {
        for(int i = 0; i < _cardControllerList.Count; i++)
        {
            _cardControllerList[i].SetID(i);
        }
    }

}
