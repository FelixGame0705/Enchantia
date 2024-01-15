using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class CombindPanelController : MonoBehaviour
{
    [Header("Info Data")]
    [SerializeField] private List<TierCardSpriteInfo> tierListSpriteInfo;
    [SerializeField] private ItemData _cardData;
    [Header("Controller")]
    [SerializeField] private CombindCardDisplayController _combindCardDisplayController;

    public void CombindPanelClicked(){
        HidePanel();
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

    private void OnEnable()
    {
        _combindCardDisplayController.CardRender(_cardData);
    }

    public void SetCardData(ItemData itemData){
        _cardData = itemData;
    }

    public TierCardSpriteInfo GetTierCardSpriteInfoByTier(int tier){
        return tierListSpriteInfo.First<TierCardSpriteInfo>(x => x.Tier == tier);
    }
}
