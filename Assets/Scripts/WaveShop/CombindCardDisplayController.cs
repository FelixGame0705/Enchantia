using System;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class CombindCardDisplayController : MonoBehaviour
{
    [Header("Card Component")]

    [SerializeField] private Image _itemIconImage;
    [SerializeField] private Image _itemBackgroundImage;
    [SerializeField] private Image _background;
    [SerializeField] private TMP_Text _itemNameText;
    [SerializeField] private TMP_Text _itemTypeText;
    [SerializeField] private TMP_Text _descriptionText;
    [SerializeField] private GameObject _combindButton;

    [SerializeField] private ItemData _itemData;


    public void CardRender(ItemData dataIn){
        
        _itemData = dataIn;
        _itemNameText.text = _itemData.ItemName;
        _descriptionText.text = _itemData.ItemDescription;
        _itemIconImage.sprite = _itemData.ItemImg;
        switch(_itemData.ItemStats.TYPE1){
            case ITEM_TYPE.ITEM:
                _itemTypeText.text = "ITEM";
                break;
            case ITEM_TYPE.WEAPON:
                _itemTypeText.text = "WEAPON";
                break;
        }
        try{
            var itemBackground = WaveShopMainController.Instance.CombindPanelController.GetTierCardSpriteInfoByTier(_itemData.Tier);
            _itemBackgroundImage.sprite = itemBackground.IconBackground;
            _background.sprite = itemBackground.Background;

        }catch(NullReferenceException){
            Debug.LogError("Error when searching tier card sprite");
        }
        if(CheckCanCombind()) _combindButton.SetActive(true);
        else _combindButton.SetActive(false);
    }

    public void ResetCard(){
        _itemData = null;
        _itemNameText.text = "";
        _descriptionText.text = "";
        _itemIconImage.sprite = null;
        _combindButton.SetActive(false);
    }

    public void OnClickRecycle()
    {
        WaveShopMainController.Instance.GetWeaponInventory().RemoveCard(WaveShopMainController.Instance.GetIndexWeaponSelected());
        GamePlayController.Instance.GetWeaponSystem().SellWeapon(WaveShopMainController.Instance.GetIndexWeaponSelected());
        WaveShopMainController.Instance.CombindPanelController.HidePanel();
    }

    public void OnClickCombine()
    {
        int oldIndex = WaveShopMainController.Instance.GetIndexWeaponSelected();
        int oldID = WaveShopMainController.Instance.GetWeaponInventory().WeaponControllerList[oldIndex].GetCardData().Id;
        var indexRemove = FindIndexRemove(oldIndex);
        if (FindIndexRemove(oldIndex) < 0) return;
        WaveShopMainController.Instance.GetWeaponInventory().UpgradeCard(WaveShopMainController.Instance.GetIndexWeaponSelected());
        GamePlayController.Instance.GetWeaponSystem().UpgradeWeapon(WaveShopMainController.Instance.GetIndexWeaponSelected());
        GamePlayController.Instance.GetWeaponSystem().SellWeapon(indexRemove);
        WaveShopMainController.Instance.GetWeaponInventory().RemoveCard(indexRemove);
        _combindButton.SetActive(false);
    }

    private int FindIndexRemove(int oldIndex)
    {
        int id_combine = -1;
        var cardControllerList = WaveShopMainController.Instance.GetWeaponInventory().WeaponControllerList;
        var cardImage = WaveShopMainController.Instance.GetWeaponInventory().WeaponControllerList[oldIndex];
        if(cardImage == null) return -1;
        if(cardImage.GetCardData().NextItemWeapon == null) return -1;
        var list = cardControllerList.FindAll(item => {
            return item.GetCardData().name.Equals(cardImage.GetCardData().name) 
            && item.GetCardData().Tier == cardImage.GetCardData().Tier
            && item.GetID() != cardImage.GetID()
            ;
        });
        if(list.Count > 0) id_combine = list[0].GetID();
        return id_combine;
    }

    private bool CheckCanCombind(){
        int oldIndex = WaveShopMainController.Instance.GetIndexWeaponSelected();
        return FindIndexRemove(oldIndex) >= 0;
    }
}
