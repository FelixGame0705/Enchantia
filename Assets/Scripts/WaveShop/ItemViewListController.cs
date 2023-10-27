using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemViewListController : MonoBehaviour
{
    [SerializeField] private List<ItemCardController> _cardControllerList;
    private void Awake()
    {
       
    }
    // Start is called before the first frame update
    void Start()
    {
        foreach (var cardController in _cardControllerList)
        {
            cardController.EnableItem();
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void ReRoll(Stack<ItemData> rerollDataList)
    {
        foreach (var cardController in _cardControllerList)
        {
            if(cardController.IsLock != true) cardController.RenderCard(rerollDataList.Pop());
        }
    }

    public ItemData GetItemDataOfCardUsingPosition(int pos)
    {
        return _cardControllerList[pos - 1].CardItemInfo;
    }
    
}
