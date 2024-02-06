using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Runtime.CompilerServices;
using JetBrains.Annotations;
using UnityEngine;
using UnityEngine.PlayerLoop;

public class CombindPanelController : MonoBehaviour
{
    [Header("Info Data")]
    [SerializeField] private List<TierCardSpriteInfo> tierListSpriteInfo;
    [SerializeField] private ItemData _cardData;
    [Header("Controller")]
    [SerializeField] private CombindCardDisplayController _combindCardDisplayController;

    [SerializeField] private List<CombineImageController> _combineImageControllerList;
    [SerializeField] private List<ItemImageController> weaponImageController;


    [SerializeField] private GameObject _sampleImage;
    [SerializeField] private GameObject _content;

    [SerializeField] private int _indexWeaponSelected;

    public List<CombineImageController> CombineImageControllerList {get => this._combineImageControllerList;}

    public void CombindPanelClicked(){
        HidePanel();
    }

    public int GetIndexWeaponSelected(){
        return _indexWeaponSelected;
    }
    public void SetIndexWeaponSelected(int index){
        this._indexWeaponSelected = index;
    }

    public void HidePanel(){
        gameObject.SetActive(false);
    }

    public void RenderPanel(){
        gameObject.SetActive(true);
    }

    private void OnDisable()
    {
        _cardData = null;
        _combindCardDisplayController.ResetCard();
    }

    private void Awake()
    {
        _combineImageControllerList = new List<CombineImageController>();
    }

    private void OnEnable()
    {
        this.weaponImageController = WaveShopMainController.Instance.GetWeaponInventory().WeaponControllerList;
        InitWeaponItemList();
        _combindCardDisplayController.CardRender(_cardData);
    }

    public void SetCardData(ItemData itemData){
        _cardData = itemData;
    }

    public TierCardSpriteInfo GetTierCardSpriteInfoByTier(int tier){
        return tierListSpriteInfo.First<TierCardSpriteInfo>(x => x.Tier == tier);
    }

    public void InitWeaponItemList(){
        var i  = 0;
        foreach(var item in weaponImageController){
            var enableImageList = _combineImageControllerList.Where(x => x.Index < 0).ToList();
            CombineImageController cloneCombineImage = null;
            if(enableImageList.Count == 0){
                cloneCombineImage =  Instantiate(_sampleImage,_content.transform)?.GetComponent<CombineImageController>();
                _combineImageControllerList.Add(cloneCombineImage);
            }else{
                cloneCombineImage = enableImageList[0];
            }
            cloneCombineImage?.LoadData(i, item.GetCardData().ItemImg, item.GetCardData().Tier);
            i++;
        }
    }

    public void DisableWeaponItemList(){
        foreach(var item in _combineImageControllerList){
            item.DisableItem();
        }
    }


    public void OnDoneClicked(){
        HidePanel();
    }

    public void OnUndoClicked(){

    }
    public void HandleItemClicked(int index){
        try{
            this._indexWeaponSelected = index;
            var item = weaponImageController[index];
            var a = item.GetCardData();
            SetCardData(a);
            _indexWeaponSelected = index;
            _combindCardDisplayController.CardRender(_cardData);
            WaveShopMainController.Instance.SetIndexWeaponSelected(item.GetID());
            
        }catch(Exception){
            Debug.LogError("Index Out Of Bounce");
        }
    }
}
