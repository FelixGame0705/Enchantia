using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InventoryController : MonoBehaviour
{
    [SerializeField] private List<ItemImageController> _cardControllerList;
    [SerializeField] private GameObject _cardModel;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnEnable()
    {
        foreach (var cardController in _cardControllerList)
        {
            cardController.EnableItem();
        }
    }

    public void AddCardToInventory(ItemData item)
    {
        GameObject card = Instantiate(_cardModel, gameObject.transform);
        _cardControllerList.Add(card.GetComponent<ItemImageController>());
        card.GetComponent<ItemImageController>().SetCardData(item);
    }
}
