using System.Collections.ObjectModel;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemViewListController : MonoBehaviour
{
    [SerializeField] private List<ItemCardController> _cardControllerList;
    [SerializeField] private RerollPlayController _rerollPlayController;
    
    public bool _isFullLocked = false;
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

    private void OnEnable()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if(_isFullLocked == true){
            _rerollPlayController.ChangeRerollBtnState(false);
        }else _rerollPlayController.ChangeRerollBtnState(true);
    }

    public void ReRoll(Stack<ItemData> rerollDataList)
    {
        foreach (var cardController in _cardControllerList)
        {
            if(cardController.IsLock != true) cardController.RenderCard(rerollDataList.Pop());
        }
    }

    public ItemCardController GetItemDataOfCardUsingPosition(int pos)
    {
        return _cardControllerList[pos - 1].GetComponent<ItemCardController>();
    }

    public GameObject GetObjectCard(int pos)
    {
        return _cardControllerList[pos - 1].gameObject;
    }

    public void CheckAllLocked(){
        var count = 0;
        foreach(var item in _cardControllerList){
           if(item.IsLock == true){
                count ++;
           } 
        }
        if(count == 4) _isFullLocked = true;
        else _isFullLocked = false;
    }
    
}
