using System.Net.Mime;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class ItemCardController : MonoBehaviour
{
    [Header("Setup Card")]
    [SerializeField] private ItemData _cardItemInfo;
    [SerializeField] private Image _itemIcon;
    [SerializeField] private TextMeshProUGUI  _itemName;
    [SerializeField] private TextMeshProUGUI _itemType;
    [SerializeField] private TextMeshProUGUI _itemDescription;
    [SerializeField] private TextMeshProUGUI _lockBtn;
    [SerializeField] private TextMeshProUGUI _itemPriceBtn;
    [SerializeField] private Image _backgroundCard;
    [SerializeField] private Image _backgroundIcon;
    [SerializeField] private Button _buyBtn;

    [Header("Card Infomation")]
    [SerializeField] private int _itemPrice;
    
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

    private void OnDisable()
    {
        _buyBtn.enabled = true;
        _lockBtn.text = "Lock";
        _isLocked = false;
    }

    public void Lock()
    {
        if(_isLocked == true) _lockBtn.text = "Lock";
        else _lockBtn.text = "Unlock";
        _isLocked = !_isLocked;
        WaveShopMainController.Instance.UpdateFullLockItemStatus();
    }
    public void Buy()
    {
        _isLocked = false;
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
        var tierSprite = WaveShopMainController.Instance.ViewListController.TierSpriteDictionary[data.Tier];
        DisableItem();
        _cardItemInfo = data;
        _itemIcon.sprite = _cardItemInfo.ItemImg;
        _itemName.text = _cardItemInfo.ItemName;
        _itemPrice = _cardItemInfo.ItemPrice;
        _itemDescription.text = _cardItemInfo.ItemDescription;
        _itemPriceBtn.text = Utils.Instance.GetFinalPrice(_itemPrice, WaveShopMainController.Instance.CurrentWave).ToString();
        _backgroundCard.sprite = tierSprite.Background;
        _backgroundIcon.sprite = tierSprite.IconBackground;
        _itemName.color = tierSprite.NameColor;
        if (data.ItemStats.TYPE1 == ITEM_TYPE.ITEM) _itemType.text = "Item";
        else _itemType.text = "Weapon";
        EnableItem();
    }

    public int GetItemFinalPrice(){
        return Utils.Instance.GetFinalPrice(_itemPrice,WaveShopMainController.Instance.CurrentWave);
    }

    public void ChangeBuyButtonState(bool state){
        _buyBtn.interactable = state;
    }

    public void CanItemBuy(){
        if(_cardItemInfo.ItemStats.TYPE1 == ITEM_TYPE.WEAPON){
            if(GetItemFinalPrice() > WaveShopMainController.Instance.CurrentMoney || WaveShopMainController.Instance.GetCountWeapon() >= 6) ChangeBuyButtonState(false);
            else ChangeBuyButtonState(true);
        }else{
            if(GetItemFinalPrice() > WaveShopMainController.Instance.CurrentMoney ) ChangeBuyButtonState(false);
            else ChangeBuyButtonState(true);
        }
    }
}
