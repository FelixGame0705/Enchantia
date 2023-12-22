using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ItemImageController : MonoBehaviour
{
    [SerializeField] private ItemData _cardItemInfo;
    [SerializeField] private Image _itemImg;
    [SerializeField] private Image _bg;
    [SerializeField] private int _id;
    [SerializeField] private CombineRecycleInfo combineRecycleInfo;
    
    private void Awake()
    {
        combineRecycleInfo = gameObject.AddComponent<CombineRecycleInfo>();
    }

    private string _content;
    public void EnableItem()
    {
        this.gameObject.SetActive(true);
    }

    public void RenderCard()
    {
        _itemImg.sprite = _cardItemInfo.ItemImg;
        _bg.color = _cardItemInfo.RarityColor;
    }

    public void SetCardData(ItemData data)
    {
        _cardItemInfo = data;
        _content += data.name+"\n";
        _content += "Damage: " + data.ItemStats.RangedDamage;
        RenderCard();
    }

    public ItemData GetCardData()
    {
        return _cardItemInfo;
    }

    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void OnClickItemWeaponCard()
    {
        WaveShopMainController.Instance.SetDetailWeapon(transform, true);
        WaveShopMainController.Instance.GetDetailWeapon().SetDetailTxt(_content);
    }

    public int GetID()
    {
        return _id;
    }

    public void SetID(int id)
    {
        _id = id;
    }

    public CombineRecycleInfo CombineRecycleInfo {get => combineRecycleInfo; set => combineRecycleInfo = value;}
}
