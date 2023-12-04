using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ItemCardController : MonoBehaviour
{
    [SerializeField] private ItemData _cardItemInfo;
    [SerializeField] private Image _itemIcon;
    [SerializeField] private Text _itemName;
    [SerializeField] private Text _itemType;
    [SerializeField] private Text _itemDescription;
    [SerializeField] private int _itemPrice;
    [SerializeField] private Text _itemPriceBtn;
    [SerializeField] private bool _isLocked;
    [SerializeField] private int _cardIndex;

    public ItemData CardItemInfo { get { return _cardItemInfo; } set { _cardItemInfo = value; } }
    public bool IsLock { get { return _isLocked; } set { _isLocked = value; } }
    private void Awake()
    {
        _isLocked = false;
        if(_cardItemInfo != null)
        {
            _itemIcon.sprite = _cardItemInfo.ItemImg;
            //_itemName.text = _cardItemInfo.ItemName;
            _itemPrice = _cardItemInfo.ItemPrice;
            _itemDescription.text = _cardItemInfo.ItemDescription;
            _itemPriceBtn.text = _itemPrice.ToString();
            this.gameObject.SetActive(false);
        }
    }

    public void Lock()
    {
        _isLocked = !_isLocked;
    }
    public void Buy()
    {
        WaveShopMainController.Instance.BuyItem(_cardIndex);
    }

    public void DisableItem()
    {
        this.gameObject.SetActive(false);
    }
    public void EnableItem()
    {
        this.gameObject.SetActive(true);
    }

    public void RenderCard(ItemData data)
    {
        DisableItem();
        _cardItemInfo = data;
        _itemIcon.sprite = _cardItemInfo.ItemImg;
        //_itemName.text = _cardItemInfo.ItemName;
        _itemPrice = _cardItemInfo.ItemPrice;
        _itemDescription.text = _cardItemInfo.ItemDescription;
        _itemPriceBtn.text = _itemPrice.ToString();
        EnableItem();
    }
}
