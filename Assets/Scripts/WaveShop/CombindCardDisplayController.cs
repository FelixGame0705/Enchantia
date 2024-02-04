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
    [SerializeField] private GameObject _combineButton;

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
        if(CheckCanCombine()) _combineButton.SetActive(true);
        else _combineButton.SetActive(false);
    }

    public void ResetCard(){
        _itemData = null;
        _itemNameText.text = "";
        _descriptionText.text = "";
        _itemIconImage.sprite = null;
        _combineButton.SetActive(false);
    }

    public void OnClickRecycle()
    {
        WaveShopMainController.Instance.CombineRecycleMechanicController.RecycleWeapon();
        WaveShopMainController.Instance.CombindPanelController.DisableWeaponItemList();
        WaveShopMainController.Instance.CombindPanelController.InitWeaponItemList();
    }

    public void OnClickCombine()
    {
        WaveShopMainController.Instance.CombineRecycleMechanicController.CombineWeapon();
        WaveShopMainController.Instance.CombindPanelController.DisableWeaponItemList();
        WaveShopMainController.Instance.CombindPanelController.InitWeaponItemList();
    }

    private bool CheckCanCombine(){
        var oldIndex = WaveShopMainController.Instance.CombindPanelController.GetIndexWeaponSelected();
        return WaveShopMainController.Instance.CombineRecycleMechanicController.FindIndexRemove(oldIndex) >= 0;
    }
}
