using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ItemImageController : MonoBehaviour
{
    [SerializeField] private ItemData _cardItemInfo;
    [SerializeField] private Image _itemImg;
    // Start is called before the first frame update

    public void EnableItem()
    {
        this.gameObject.SetActive(true);
    }

    public void RenderCard()
    {
        _itemImg.sprite = _cardItemInfo.ItemImg;
    }

    public void SetCardData(ItemData data)
    {
        _cardItemInfo = data;
        RenderCard();
    }

    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
