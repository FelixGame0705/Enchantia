using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ResultItemListController : MonoBehaviour
{
    [SerializeField] private GameObject _objectModel;
    [SerializeField] private InventoryController _weaponInventory;
    [SerializeField] private InventoryController _itemInventory;
    [SerializeField] private GameObject _weaponLists;
    [SerializeField] private GameObject _itemLists;


    public void Render()
    {
        foreach (var card in _weaponInventory.CardControllerList)
        {
            var cloneCard = Instantiate(_objectModel, _weaponLists.transform);
            cloneCard.GetComponent<Image>().sprite = card.GetCardData().ItemImg;
        }
        foreach (var card in _itemInventory.CardControllerList)
        {
            var cloneCard = Instantiate(_objectModel, _itemLists.transform);
            cloneCard.GetComponent<Image>().sprite = card.GetCardData().ItemImg;
        }
    }

}