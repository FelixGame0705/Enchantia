using System.Collections.Generic;
using UnityEngine;

public class DroppedItemController : MonoBehaviour
{
    [SerializeField] private List<DroppedItemData> _droppedItemDataList;
    [SerializeField] private ObjectPool _droppedPool;
    [SerializeField] private HashSet<GameObject> _droppedItems = new HashSet<GameObject>();
    [SerializeField] private int _gold;

    public void SetPrefabToPool(DROPPED_ITEM_TYPE index)
    {
        _droppedPool.objectPrefab = _droppedItemDataList[(int)index].modelPrefab;
    }

    public void SpawnDroppedItem(DROPPED_ITEM_TYPE indexPrefabSpawn,Vector2 position)
    {
        SetPrefabToPool(indexPrefabSpawn);
        GameObject droppedItem = _droppedPool.GetObjectFromPool();
        droppedItem.transform.position = position;
        AddUniqueDroppedItem(droppedItem);
    }

    public HashSet<GameObject> GetDroppedItems()
    {
        return _droppedItems;
    }

    public void AddDroppedItem(int amount)
    {
        _gold += amount;
    }

    public int GetDroppedItemNumber()
    {
        return _gold;
    }

    public void AddUniqueDroppedItem(GameObject consumable)
    {
        if (!_droppedItems.Contains(consumable)) _droppedItems.Add(consumable);
    }

    public void ReturnDroppedItemToPool(GameObject gold)
    {
        _droppedPool.ReturnObjectToPool(gold);
    }

    public void HarvestDroppedItem(DROPPED_ITEM_TYPE type)
    {
        switch (type)
        {
            case DROPPED_ITEM_TYPE.GOLD:
                HarvestGold();
                break;
            case DROPPED_ITEM_TYPE.FRUIT:
                HarvestFruit();
                break;
        }
    }

    private void HarvestFruit()
    {
        GamePlayController.Instance.GetCharacterController().AddCurrentHealth(3);
    }

    public void HarvestGold()
    {
        AddDroppedItem(1);
    }
}
